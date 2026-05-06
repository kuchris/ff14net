#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using FFXIV_ACT_Plugin.Config;
using FFXIV_ACT_Plugin.Memory;
using FFXIV_ACT_Plugin.Memory.MemoryProcessors;
using FFXIV_ACT_Plugin.Memory.MemoryReader;
using FFXIV_ACT_Plugin.Network;
using Machina.FFXIV.Deucalion;

namespace FFXIV_ACT_Plugin;

public class ProblemDiagnosisHelper
{
	private readonly IProcessManager _processManager;

	private readonly IReadProcesses _readProcesses;

	private readonly ISignatureManager _signatureManager;

	private readonly ISettingsMediator _settingsManager;

	private readonly IScanPackets _scanPackets;

	private readonly IMobArrayProcessor _mobArrayProcessor;

	private readonly IReadAntiVirus _readAntiVirus;

	public ProblemDiagnosisHelper(ISettingsMediator settingsManager, IProcessManager processManager, IReadProcesses readProceses, ISignatureManager signatureManager, IScanPackets scanPackets, IMobArrayProcessor mobArrayProcessor, IReadAntiVirus readAntiVirus)
	{
		_settingsManager = settingsManager;
		_processManager = processManager;
		_readProcesses = readProceses;
		_signatureManager = signatureManager;
		_scanPackets = scanPackets;
		_mobArrayProcessor = mobArrayProcessor;
		_readAntiVirus = readAntiVirus;
	}

	public void Diagnose(bool isAutomatedScan)
	{
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		string text = "";
		int num = -1;
		if (_settingsManager.DataCollectionSettings == null)
		{
			return;
		}
		if (!isAutomatedScan)
		{
			string[] array = _readAntiVirus.Read();
			foreach (string text2 in array)
			{
				Trace.WriteLine("FFXIV_ACT_Plugin: Microsoft Security Center registry includes the following anti-virus: " + text2 + ".", "FFXIV_ACT_Plugin");
			}
		}
		try
		{
			string text3;
			if (!_processManager.Verify())
			{
				List<int> source = _readProcesses.Read64(false);
				Process[] processesByName = Process.GetProcessesByName("ffxiv_dx11.exe");
				text = ((_settingsManager.DataCollectionSettings == null) ? "Failure: FFXIV_ACT_Plugin has not been initialized yet." : ((_settingsManager.DataCollectionSettings.ProcessID != 0) ? "Failure: A specific FFXIV Process ID is specified, but cannot be found.  Please click 'Refresh List' and select the process ID, or choose 'Automatic'." : ((!processesByName.Any() || source.Any()) ? "Failure: Cannot find running FFXIV Game." : "Failure: ffxiv_dx11.exe process has been found, but cannot be read.  Please verify you do not have Anti-cheat software enabled for other games.")));
			}
			else if ((text3 = _signatureManager.ValidateWithMessage()) != string.Empty)
			{
				text = text + "Failure: " + text3 + ". If FFXIV was recently updated, this may require an update to FFXIV_ACT_Plugin.";
			}
			else if ((num = _scanPackets.IsNetworkDataValid()) == 0)
			{
				text = "Success: All FFXIV memory signatures detected successfully, and Network data is processing successfully.";
			}
			else if (_mobArrayProcessor.PrimaryPlayerPointer == IntPtr.Zero)
			{
				if (isAutomatedScan)
				{
					return;
				}
				text = "Failure: Please log into FFXIV before testing the game connection.";
			}
			else if (num == 1)
			{
				if (isAutomatedScan)
				{
					return;
				}
				text = "Failure: Network data is successfully received, but not processed.  If the game was recently updated, an update may be required for FFXIV_ACT_Plugin.";
			}
			else
			{
				text = "Failure:No data received from Deucalion." + Environment.NewLine + "Please review the logfile for possible erorr messages." + Environment.NewLine + "Some Anti-cheat software from other games may require running ACT as Administrator." + Environment.NewLine + "Last injection message was:" + DeucalionInjector.LastInjectionError;
			}
		}
		catch (Exception ex)
		{
			text = "Failure: An error occurred while validating access to FFXIV data: " + ex.Message;
		}
		TraySlider val = new TraySlider
		{
			ButtonLayout = (ButtonLayoutEnum)0
		};
		((Control)val.TrayText).Text = text;
		((Control)val.TrayTitle).Text = "FFXIV_ACT_Plugin Game Connection";
		val.ShowTraySlider();
	}
}
