using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using Costura;
using FFXIV_ACT_Plugin.Common;
using FFXIV_ACT_Plugin.Common.Models;
using FFXIV_ACT_Plugin.Config;
using FFXIV_ACT_Plugin.Logfile;
using FFXIV_ACT_Plugin.Memory;
using FFXIV_ACT_Plugin.Memory.MemoryProcessors;
using FFXIV_ACT_Plugin.Memory.MemoryReader;
using FFXIV_ACT_Plugin.Network;
using FFXIV_ACT_Plugin.Network.PacketHandlers;
using FFXIV_ACT_Plugin.Parse;
using FFXIV_ACT_Plugin.Parse.EffectEntryStrategy;
using FFXIV_ACT_Plugin.Parse.ParseStrategy;
using FFXIV_ACT_Plugin.Resource;
using Machina;
using Machina.FFXIV;
using Machina.FFXIV.Deucalion;
using Machina.FFXIV.Headers.Opcodes;
using Microsoft.MinIoC;

namespace FFXIV_ACT_Plugin;

public class FFXIV_ACT_Plugin : IActPluginV1, IDisposable
{
	private Container _iocContainer;

	private Label _statusLabel;

	private ACT_PluginUpdate _actUpdate;

	private ACT_UIMods _actUIMods;

	private DataCollection _dataCollection;

	private bool disposedValue;

	public IDataSubscription DataSubscription { get; private set; }

	public IDataRepository DataRepository { get; private set; }

	public bool PluginStarted { get; private set; }

	public FFXIV_ACT_Plugin()
	{
		AssemblyLoader.Attach();
	}

	public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
	{
		_statusLabel = pluginStatusText;
		if (!Environment.Is64BitProcess)
		{
			((Control)_statusLabel).Text = "FFXIV_ACT_Plugin Startup Failed. Requires 64-bit version of ACT.";
			return;
		}
		try
		{
			VerifyAssemblyVersions();
			ConfigureIOC();
			OpcodeManager.Instance.SetRegion((GameRegion)1);
			_iocContainer.Resolve<ResourceManager>().LoadResources();
			DataSubscription = (IDataSubscription)(object)_iocContainer.Resolve<DataSubscription>();
			DataRepository = _iocContainer.Resolve<IDataRepository>();
			_actUIMods = _iocContainer.Resolve<ACT_UIMods>();
			_dataCollection = _iocContainer.Resolve<DataCollection>();
			_actUpdate = _iocContainer.Resolve<ACT_PluginUpdate>();
			_actUpdate.Plugin = (IActPluginV1)(object)this;
			_actUpdate.StartUpdateCheck();
			_actUIMods.ConfigureUI(pluginScreenSpace);
			_actUIMods.LoadACTSettings();
			_actUpdate.FinalizeDeucalionDistrib();
			InitializeDeucalionPath();
			_dataCollection.StartMemory();
			((Control)_statusLabel).Text = "FFXIV_ACT_Plugin Started.";
			PluginStarted = true;
		}
		catch (Exception ex)
		{
			ActGlobals.oFormActMain.WriteExceptionLog(ex, "FFXIV_ACT_Plugin InitPlugin Failed.");
			((Control)_statusLabel).Text = $"FFXIV_ACT_Plugin Startup Failed. {ex}";
		}
	}

	public void DeInitPlugin()
	{
		PluginStarted = false;
		if (_actUpdate != null)
		{
			_actUpdate.Plugin = null;
			_actUpdate.Dispose();
			_actUpdate = null;
		}
		_actUIMods?.UnloadControls();
		_actUIMods = null;
		_dataCollection?.StopMemory();
		_dataCollection?.Dispose();
		_dataCollection = null;
		_iocContainer?.Dispose();
		_iocContainer = null;
		if (_statusLabel != null)
		{
			((Control)_statusLabel).Text = "FFXIV_ACT_Plugin Unloaded.";
		}
		_statusLabel = null;
	}

	private void VerifyAssemblyVersions()
	{
		Version version = new Version("2.3.1.3");
		Version version2 = new Version("2.4.6.8");
		Version version3 = new Version("3.0.0.0");
		AssemblyName[] obj = new AssemblyName[6]
		{
			typeof(ISettingsMediator).Assembly.GetName(),
			typeof(ILogOutput).Assembly.GetName(),
			typeof(ICombatantManager).Assembly.GetName(),
			typeof(ScanPackets).Assembly.GetName(),
			typeof(IParseStrategy).Assembly.GetName(),
			typeof(IActionList).Assembly.GetName()
		};
		string text = "";
		ulong num = BitConverter.ToUInt64(GetType().Assembly.GetName().GetPublicKeyToken(), 0);
		Version version4 = typeof(TCPNetworkMonitor).Assembly.GetName().Version;
		ulong num2 = BitConverter.ToUInt64(typeof(TCPNetworkMonitor).Assembly.GetName().GetPublicKeyToken(), 0);
		if (version4 != version || num2 != num)
		{
			text += $"FFXIV_ACT_Plugin detected version {version4} of Machina library, expected {version}.  Restart ACT, and make sure FFXIV_ACT_Plugin is the first plugin listed on the plugins tab.{Environment.NewLine}";
		}
		version4 = typeof(FFXIVNetworkMonitor).Assembly.GetName().Version;
		num2 = BitConverter.ToUInt64(typeof(FFXIVNetworkMonitor).Assembly.GetName().GetPublicKeyToken(), 0);
		if (version4 != version2 || num2 != num)
		{
			text += $"FFXIV_ACT_Plugin detected version {version4} of Machina.FFXIV library, expected {version2}.  Restart ACT, and make sure FFXIV_ACT_Plugin is the first plugin listed on the plugins tab.{Environment.NewLine}";
		}
		version4 = typeof(Combatant).Assembly.GetName().Version;
		num2 = BitConverter.ToUInt64(typeof(Combatant).Assembly.GetName().GetPublicKeyToken(), 0);
		if (version4 != version3 || num2 != num)
		{
			text += $"FFXIV_ACT_Plugin detected version {version4} of FFXIV_ACT_Plugin.Common library, expected {version3}.  Restart ACT, and make sure FFXIV_ACT_Plugin is the first plugin listed on the plugins tab.{Environment.NewLine}";
		}
		AssemblyName[] array = obj;
		foreach (AssemblyName obj2 in array)
		{
			num2 = BitConverter.ToUInt64(obj2.GetPublicKeyToken(), 0);
			if (obj2.Version != typeof(FFXIV_ACT_Plugin).Assembly.GetName().Version || num2 != num)
			{
				text = text + "FFXIV_ACT_Plugin detected incorrect version of a built-in library.  Restart ACT, and make sure FFXIV_ACT_Plugin is the first plugin listed on the plugins tab." + Environment.NewLine;
				break;
			}
		}
		if (!string.IsNullOrWhiteSpace(text))
		{
			text += "If problems persist, disable all plugins other than FFXIV_ACT_Plugin and restart ACT.";
			ActGlobals.oFormActMain.RestartACT(true, text);
		}
	}

	private void InitializeDeucalionPath()
	{
		string directoryName = ACTWrapper.PluginGetSelfData((IActPluginV1)(object)this).pluginFile.DirectoryName;
		typeof(DeucalionInjector).GetProperty("DeucalionPath", BindingFlags.Static | BindingFlags.Public | BindingFlags.SetProperty)?.SetValue(null, directoryName);
	}

	private void ConfigureIOC()
	{
		_iocContainer = new Container();
		_iocContainer.Register((Func<IServiceProvider>)(() => _iocContainer));
		_iocContainer.Register<ACT_PluginUpdate>().AsSingleton();
		_iocContainer.Register<ISettingsMediator, SettingsMediator>().AsSingleton();
		_iocContainer.Register<ACT_UIMods>().AsSingleton();
		_iocContainer.Register<IActWrapper, ACTWrapper>().AsSingleton();
		_iocContainer.Register<ResourceManager>().AsSingleton();
		_iocContainer.Register<IStatusList, StatusList>().AsSingleton();
		_iocContainer.Register<INameResource, NameResource>().AsSingleton();
		_iocContainer.Register<IActionList, ActionList>().AsSingleton();
		_iocContainer.Register<IWorldList, WorldList>().AsSingleton();
		_iocContainer.Register<ITerritoryList, TerritoryList>().AsSingleton();
		_iocContainer.Register<IMapList, MapList>().AsSingleton();
		_iocContainer.Register<IDefinitionRepository, DefinitionRepository>().AsSingleton();
		_iocContainer.Register<ICombatantManager, CombatantManager>().AsSingleton();
		_iocContainer.Register<DataCollection>().AsSingleton();
		_iocContainer.Register<DataEvent>().AsSingleton();
		_iocContainer.Register<DataSubscription>().AsSingleton();
		_iocContainer.Register<IDataRepository, DataRepository>().AsSingleton();
		_iocContainer.Register<ILogOutput, LogOutput>().AsSingleton();
		_iocContainer.Register<ILogFormat, LogFormat>().AsSingleton();
		_iocContainer.Register<IProcessManager, ProcessManager>().AsSingleton();
		_iocContainer.Register<ISignatureManager, SignatureManager>().AsSingleton();
		_iocContainer.Register<IScanMemory, ScanMemory>().AsSingleton();
		_iocContainer.Register<IScanPackets, ScanPackets>().AsSingleton();
		_iocContainer.Register<PacketHandlerMediator>().AsSingleton();
		_iocContainer.Register<DataEventProcessor>().AsSingleton();
		_iocContainer.Register<IReadCombatant, ReadCombatant>().AsSingleton();
		_iocContainer.Register<IReadLog, ReadLog>().AsSingleton();
		_iocContainer.Register<IReadMemory, ReadMemory>().AsSingleton();
		_iocContainer.Register<IReadMobArray, ReadMobArray>().AsSingleton();
		_iocContainer.Register<IReadParty, ReadParty>().AsSingleton();
		_iocContainer.Register<IReadPlayer, ReadPlayer>().AsSingleton();
		_iocContainer.Register<IReadProcesses, ReadProcesses>().AsSingleton();
		_iocContainer.Register<IReadServerTime, ReadServerTime>().AsSingleton();
		_iocContainer.Register<IReadZoneMap, ReadZoneMap>().AsSingleton();
		_iocContainer.Register<IReadMapId, ReadMapId>().AsSingleton();
		_iocContainer.Register<IReadSignature, ReadSignature>().AsSingleton();
		_iocContainer.Register<IReadVtable, ReadVtable>().AsSingleton();
		_iocContainer.Register<IReadAntiVirus, ReadAntiVirus>().AsSingleton();
		_iocContainer.Register<ICombatantProcessor, CombatantProcessor>().AsSingleton();
		_iocContainer.Register<ILogProcessor, LogProcessor>().AsSingleton();
		_iocContainer.Register<IMobArrayProcessor, MobArrayProcessor>().AsSingleton();
		_iocContainer.Register<IPartyProcessor, PartyProcessor>().AsSingleton();
		_iocContainer.Register<IPlayerProcessor, PlayerProcessor>().AsSingleton();
		_iocContainer.Register<IServerTimeProcessor, ServerTimeProcessor>().AsSingleton();
		_iocContainer.Register<IZoneMapProcessor, ZoneMapProcessor>().AsSingleton();
		_iocContainer.Register<INetworkBuffManager, NetworkBuffManager>().AsSingleton();
		_iocContainer.Register<INetworkMarkerManager, NetworkMarkerManager>().AsSingleton();
		_iocContainer.Register<ParseMediator>().AsSingleton();
		_iocContainer.Register<ParseStrategyFactory>().AsSingleton();
		_iocContainer.Register<IReportCombatData, ReportCombatData>().AsSingleton();
		_iocContainer.Register<IEffectEntryStrategyMediator, EffectEntryStrategyMediator>().AsSingleton();
		_iocContainer.Register<IDoTSimulator, DoTSimulator>().AsSingleton();
		_iocContainer.Register<IPotencyStatusApplication, PotencyStatusApplication>().AsSingleton();
		_iocContainer.Register<CritDhStatusApplication>().AsSingleton();
		_iocContainer.Register<DamageShieldSimulator>().AsSingleton();
		_iocContainer.Register<IDeferredEventProcessor, DeferredEventProcessor>().AsSingleton();
		_iocContainer.Register<SettingsPropertyPage>().AsSingleton();
		_iocContainer.Register<BenchmarkPropertyPage>().AsSingleton();
		_iocContainer.Register<ProblemDiagnosisHelper>().AsSingleton();
		_iocContainer.Register<MonitorNetwork>().AsSingleton();
		_iocContainer.Register<LoggingTraceListener>().AsSingleton();
		_iocContainer.Register<IBenchmarkRepository, BenchmarkRepository>().AsSingleton();
		foreach (Type item in typeof(IEffectEntryStrategy).Assembly.ExportedTypes.Where((Type x) => x.GetInterfaces().Contains(typeof(IEffectEntryStrategy))))
		{
			_iocContainer.Register(item, item).AsSingleton();
		}
		foreach (Type item2 in typeof(IParseStrategy).Assembly.ExportedTypes.Where((Type x) => x.GetInterfaces().Contains(typeof(IParseStrategy))))
		{
			_iocContainer.Register(item2, item2).AsSingleton();
		}
		foreach (Type item3 in typeof(IPacketHandler).Assembly.ExportedTypes.Where((Type x) => x.GetInterfaces().Contains(typeof(IPacketHandler))))
		{
			_iocContainer.Register(item3, item3).AsSingleton();
		}
		_iocContainer.Register<CombatantState>();
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_iocContainer?.Dispose();
				_actUpdate?.Dispose();
				_dataCollection?.Dispose();
			}
			disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
