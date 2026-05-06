#define TRACE
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Advanced_Combat_Tracker;

namespace FFXIV_ACT_Plugin;

public class ACT_PluginUpdate : IDisposable
{
	private readonly int m_PluginId = 73;

	private Thread _updateThread;

	private bool disposedValue;

	public IActPluginV1 Plugin { get; set; }

	public ACT_PluginUpdate()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		ACTWrapper.AddUpdateCheckClickedDelegate(new NullDelegate(UpdateCheckClicked_Version));
	}

	public void StartUpdateCheck()
	{
		if (ACTWrapper.GetAutomaticUpdatesAllowed())
		{
			_updateThread = new Thread(UpdateCheckClicked_Version);
			_updateThread.IsBackground = true;
			_updateThread.Start();
		}
	}

	public void UpdateCheckClicked_Version()
	{
		string currentFileName = "";
		try
		{
			Version? version = typeof(FFXIV_ACT_Plugin).Assembly.GetName().Version;
			Version version2 = new Version(ACTWrapper.PluginGetRemoteVersion(m_PluginId));
			if (!(version < version2))
			{
				return;
			}
			ACTWrapper.RunOnACTUIThread(delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_001c: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0056: Unknown result type (might be due to invalid IL or missing references)
				//IL_0061: Unknown result type (might be due to invalid IL or missing references)
				//IL_0085: Unknown result type (might be due to invalid IL or missing references)
				TraySlider val3 = new TraySlider
				{
					ButtonLayout = (ButtonLayoutEnum)1
				};
				((Control)val3.ButtonSW).Text = "Update";
				((Control)val3.ButtonSE).Text = "Cancel";
				((Control)val3.ButtonSW).Click += delegate
				{
					ActPluginData val4 = ACTWrapper.PluginGetSelfData(Plugin);
					currentFileName = val4?.pluginFile?.FullName;
					PluginDownloadData val5 = ActGlobals.oFormActMain.PluginDownloadMem(m_PluginId);
					val4.pluginFile.Delete();
					ActGlobals.oFormActMain.UnZip(val5.Data, val4.pluginFile.DirectoryName);
					ActGlobals.oFormActMain.RestartACT(true, "FFXIV_ACT_Plugin was updated.");
				};
				val3.ShowDurationMs = 30000;
				((Control)val3.TrayText).Text = "There is an updated version of the FFXIV Parsing Plugin.  Update it now?" + Environment.NewLine + Environment.NewLine + "(If there is an update to ACT, you should click Cancel and update ACT first.)";
				((Control)val3.TrayTitle).Text = "Plugin Update";
				val3.ShowTraySlider();
			});
		}
		catch (UnauthorizedAccessException)
		{
			ACTWrapper.RunOnACTUIThread(delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				TraySlider val2 = new TraySlider
				{
					ButtonLayout = (ButtonLayoutEnum)0
				};
				((Control)val2.TrayText).Text = "UnauthorizedAccessException occurred while updating plugin file [" + currentFileName + "].  ACT is unable to delete the old version of the file, or copy the new version.";
				((Control)val2.TrayTitle).Text = "FFXIV_ACT_Plugin Update Error";
				val2.ShowTraySlider();
			});
		}
		catch (ThreadAbortException)
		{
		}
		catch (Exception ex4)
		{
			Exception ex5 = ex4;
			Exception ex = ex5;
			ACTWrapper.RunOnACTUIThread(delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Expected O, but got Unknown
				//IL_001f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0029: Expected O, but got Unknown
				//IL_0040: Unknown result type (might be due to invalid IL or missing references)
				//IL_004a: Expected O, but got Unknown
				TraySlider val = new TraySlider();
				val.ButtonLayout = (ButtonLayoutEnum)0;
				((Control)val).Font = new Font(((Control)val).Font.FontFamily, 12f, (FontStyle)0);
				((Control)val.TrayText).Font = new Font(((Control)val).Font.FontFamily, 7f, (FontStyle)0);
				((Control)val.TrayText).Text = $"Exception occurred while updating FFXIV_ACT_Plugin: {Environment.NewLine}{ex}";
				((Control)val.TrayTitle).Text = "FFXIV_ACT_Plugin Update Error";
				val.ShowTraySlider();
			});
		}
	}

	public void FinalizeDeucalionDistrib()
	{
		string[] files = Directory.GetFiles(ACTWrapper.PluginGetSelfData(Plugin).pluginFile.DirectoryName);
		foreach (string text in files)
		{
			string name = new FileInfo(text).Name;
			if (!name.StartsWith("deucalion", StringComparison.InvariantCultureIgnoreCase) || !name.EndsWith(".distrib.dll", StringComparison.InvariantCultureIgnoreCase))
			{
				continue;
			}
			string text2 = text.ToLowerInvariant().Replace(".distrib.dll", ".dll");
			try
			{
				if (File.Exists(text2))
				{
					File.Delete(text2);
					Trace.WriteLine("Deleted original deucalion dll [" + text2 + "] so it can be replaced", "FFXIV_ACT_Plugin");
				}
			}
			catch (Exception arg)
			{
				Trace.WriteLine($"Unable to delete deucalion dll [{text2}] and replace it. Error={arg}", "FFXIV_ACT_Plugin");
			}
			try
			{
				File.Move(text, text2);
				Trace.WriteLine("Renamed [" + text + "] to [" + text2 + "]].", "FFXIV_ACT_Plugin");
			}
			catch (Exception arg2)
			{
				Trace.WriteLine($"Unable to move new deucalion dll [{text}] to [{text2}]. Error={arg2}", "FFXIV_ACT_Plugin");
			}
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		if (!disposedValue)
		{
			if (disposing)
			{
				ACTWrapper.RemoveUpdateCheckClickedDelegate(new NullDelegate(UpdateCheckClicked_Version));
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
