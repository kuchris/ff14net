using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Advanced_Combat_Tracker;
using FFXIV_ACT_Plugin.Common;
using FFXIV_ACT_Plugin.Config;
using FFXIV_ACT_Plugin.Logfile;
using FFXIV_ACT_Plugin.Memory.MemoryReader;

namespace FFXIV_ACT_Plugin;

public class SettingsPropertyPage : UserControl
{
	private readonly string settingsFile = Path.Combine(ACTWrapper.AppDataFolder.FullName, "Config\\FFXIV_ACT_Plugin.config.xml");

	private SettingsSerializer xmlSettings;

	private bool m_Initialized;

	private TabPage _benchmarkTabPage;

	private readonly ISettingsMediator _settingsManager;

	private readonly IReadProcesses _readProcesses;

	private readonly ILogOutput _logOutput;

	private readonly ILogFormat _logFormat;

	private readonly ProblemDiagnosisHelper _problemDiagnosisHelper;

	private readonly BenchmarkPropertyPage _benchmarkPropertyPage;

	private AttackTypeGraphGenerator m_oldGenerator;

	private IContainer components;

	private Label lblEventRollover;

	private CheckBox chkDisableDamageShield;

	private ComboBox cboLanguage;

	private Label label2;

	private GroupBox groupBox1;

	private Label label6;

	private ComboBox cboParseFilter;

	private CheckBox chkDisableCombinePets;

	private Button cmdValidateAddress;

	private ComboBox cboProcessID;

	private Label label7;

	private Button cmdReloadProcess;

	private GroupBox groupBox3;

	private FolderBrowserDialog folderBrowserDialog1;

	private GroupBox grpDebug;

	private CheckBox chkShowRealDoTs;

	private CheckBox chkSimulateDoTCrits;

	private CheckBox chkShowDebug;

	private CheckBox chkLogAllNetwork;

	private CheckBox chkGraphPotency;

	private GroupBox groupBox2;

	private Button cmdResetLogPath;

	private Button cmdChangeFolder;

	private TextBox txtLogFileDirectory;

	private Button cmdOpenExportFolder;

	private Label label1;

	private Button cmdClearMessages;

	private Button cmdCopyProblematic;

	private ListBox lstMessages;

	private LinkLabel linkLabel1;

	private CheckBox chkDisableCombatLog;

	private CheckBox chkEnableBenchmark;

	private ComboBox cboRegion;

	private Label label3;

	public ParseSettings ParseSettings => new ParseSettings
	{
		DisableCombinePets = chkDisableCombinePets.Checked,
		DisableDamageShield = chkDisableDamageShield.Checked,
		LanguageID = (Language)(((ListControl)cboLanguage).SelectedIndex + 1),
		ParseFilter = (ParseFilterMode)((((ListControl)cboParseFilter).SelectedIndex == 1) ? 1 : ((((ListControl)cboParseFilter).SelectedIndex == 2) ? 2 : ((((ListControl)cboParseFilter).SelectedIndex == 3) ? 3 : 0))),
		SimulateIndividualDoTCrits = chkSimulateDoTCrits.Checked,
		ShowRealDoTTicks = chkShowRealDoTs.Checked,
		ShowDebug = chkShowDebug.Checked,
		EnableBenchmarks = chkEnableBenchmark.Checked
	};

	public SettingsPropertyPage(ISettingsMediator settingsManager, IReadProcesses readProcesses, ProblemDiagnosisHelper problemDiagnosisHelper, ILogOutput logOutput, ILogFormat logFormat, BenchmarkPropertyPage benchmarkPropertyPage)
	{
		InitializeComponent();
		_settingsManager = settingsManager;
		_readProcesses = readProcesses;
		_logOutput = logOutput;
		_logFormat = logFormat;
		_problemDiagnosisHelper = problemDiagnosisHelper;
		_benchmarkPropertyPage = benchmarkPropertyPage;
		_settingsManager.ProcessException = delegate(DateTime timestamp, string message)
		{
			AddParserMessage(timestamp, message);
		};
	}

	public SettingsSerializer InitializeSettingsSerializer()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		xmlSettings = new SettingsSerializer((object)this);
		xmlSettings.AddControlSetting(((Control)chkDisableDamageShield).Name, (Control)(object)chkDisableDamageShield);
		xmlSettings.AddControlSetting(((Control)cboLanguage).Name, (Control)(object)cboLanguage);
		xmlSettings.AddControlSetting(((Control)cboParseFilter).Name, (Control)(object)cboParseFilter);
		xmlSettings.AddControlSetting(((Control)chkDisableCombinePets).Name, (Control)(object)chkDisableCombinePets);
		xmlSettings.AddControlSetting(((Control)chkDisableCombatLog).Name, (Control)(object)chkDisableCombatLog);
		xmlSettings.AddControlSetting(((Control)txtLogFileDirectory).Name, (Control)(object)txtLogFileDirectory);
		xmlSettings.AddControlSetting(((Control)chkSimulateDoTCrits).Name, (Control)(object)chkSimulateDoTCrits);
		xmlSettings.AddControlSetting(((Control)chkShowRealDoTs).Name, (Control)(object)chkShowRealDoTs);
		xmlSettings.AddControlSetting(((Control)chkShowDebug).Name, (Control)(object)chkShowDebug);
		xmlSettings.AddControlSetting(((Control)chkEnableBenchmark).Name, (Control)(object)chkEnableBenchmark);
		xmlSettings.AddControlSetting(((Control)cboRegion).Name, (Control)(object)cboRegion);
		return xmlSettings;
	}

	public void LoadSettings(SettingsSerializer xmlSettings)
	{
		RefreshProcessList();
		cboProcessID.SelectedItem = "Automatic";
		if (File.Exists(settingsFile))
		{
			using XmlTextReader xmlTextReader = new XmlTextReader(new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName == "SettingsSerializer")
				{
					xmlSettings.ImportFromXml(xmlTextReader);
				}
			}
			xmlTextReader.Close();
		}
		xmlSettings.FinializeComboBoxes();
		if (cboRegion.SelectedItem == null)
		{
			((ListControl)cboRegion).SelectedIndex = 0;
		}
		if (cboLanguage.SelectedItem == null)
		{
			((ListControl)cboLanguage).SelectedIndex = 0;
		}
		if (cboParseFilter.SelectedItem == null)
		{
			((ListControl)cboParseFilter).SelectedIndex = 0;
		}
		if (string.IsNullOrWhiteSpace(((Control)txtLogFileDirectory).Text))
		{
			((Control)txtLogFileDirectory).Text = Path.Combine(ACTWrapper.AppDataFolder.FullName, "FFXIVLogs");
		}
		chkShowDebug_CheckedChanged(null, null);
		((Control)chkGraphPotency).Visible = true;
		m_Initialized = true;
		OnParseSettingsChanged(ParseSettings);
		OnDataCollectionSettingsChanged(GetDataCollectionSettings());
	}

	public void SaveSettings()
	{
		XmlTextWriter xmlTextWriter = new XmlTextWriter(new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
		xmlTextWriter.Formatting = Formatting.Indented;
		xmlTextWriter.Indentation = 1;
		xmlTextWriter.IndentChar = '\t';
		xmlTextWriter.WriteStartDocument(standalone: true);
		xmlTextWriter.WriteStartElement("Config");
		xmlTextWriter.WriteStartElement("SettingsSerializer");
		xmlSettings.ExportToXml(xmlTextWriter);
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteEndDocument();
		xmlTextWriter.Flush();
		xmlTextWriter.Close();
	}

	private void cmdValidateAddress_Click(object sender, EventArgs e)
	{
		_problemDiagnosisHelper.Diagnose(isAutomatedScan: false);
	}

	private void cmdOpenExportFolder_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(((Control)txtLogFileDirectory).Text))
		{
			Process.Start(((Control)txtLogFileDirectory).Text);
		}
	}

	private void cmdReloadProcess_Click(object sender, EventArgs e)
	{
		object selectedItem = cboProcessID.SelectedItem;
		RefreshProcessList();
		if (cboProcessID.Items.Contains(selectedItem))
		{
			cboProcessID.SelectedItem = selectedItem;
		}
		else
		{
			cboProcessID.SelectedItem = "Automatic";
		}
	}

	private void RefreshProcessList()
	{
		cboProcessID.Items.Clear();
		cboProcessID.Items.Add((object)"Automatic");
		foreach (int item in _readProcesses.Read64(true))
		{
			cboProcessID.Items.Add((object)item.ToString(CultureInfo.InvariantCulture));
		}
	}

	private void cboProcessID_SelectedIndexChanged(object sender, EventArgs e)
	{
		OnDataCollectionSettingsChanged(GetDataCollectionSettings());
	}

	private void chkLogAllNetwork_CheckedChanged(object sender, EventArgs e)
	{
		OnDataCollectionSettingsChanged(GetDataCollectionSettings());
	}

	private void chkShowDebug_CheckedChanged(object sender, EventArgs e)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		if (chkShowDebug.Checked && m_Initialized && (int)MessageBox.Show("Debug options should not be enabled for routine use of FFXIV_ACT_Plugin.  Are you sure you want to enable?", "", (MessageBoxButtons)4) != 6)
		{
			chkShowDebug.Checked = false;
			return;
		}
		((Control)grpDebug).Visible = chkShowDebug.Checked;
		if (!chkShowDebug.Checked)
		{
			if (chkShowDebug.Checked)
			{
				chkShowDebug.Checked = false;
			}
			if (chkShowRealDoTs.Checked)
			{
				chkShowRealDoTs.Checked = false;
			}
			if (chkLogAllNetwork.Checked)
			{
				chkLogAllNetwork.Checked = false;
			}
			if (chkSimulateDoTCrits.Checked)
			{
				chkSimulateDoTCrits.Checked = false;
			}
			if (chkGraphPotency.Checked)
			{
				chkGraphPotency.Checked = false;
			}
			if (chkEnableBenchmark.Checked)
			{
				chkEnableBenchmark.Checked = false;
			}
		}
		ACT_UIMods.UpdateACTTables(chkShowDebug.Checked);
		OnParseSettingsChanged(ParseSettings);
	}

	private void chkDisableCombatLog_CheckedChanged(object sender, EventArgs e)
	{
		OnDataCollectionSettingsChanged(GetDataCollectionSettings());
	}

	private void cmdChangeFolder_Click(object sender, EventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Invalid comparison between Unknown and I4
		folderBrowserDialog1.SelectedPath = ((Control)txtLogFileDirectory).Text;
		if ((int)((CommonDialog)folderBrowserDialog1).ShowDialog() == 1)
		{
			((Control)txtLogFileDirectory).Text = folderBrowserDialog1.SelectedPath;
			OnDataCollectionSettingsChanged(GetDataCollectionSettings());
		}
	}

	private void cmdResetLogPath_Click(object sender, EventArgs e)
	{
		((Control)txtLogFileDirectory).Text = Path.Combine(ACTWrapper.AppDataFolder.FullName, "FFXIVLogs");
		OnDataCollectionSettingsChanged(GetDataCollectionSettings());
	}

	private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
	{
		OnParseSettingsChanged(ParseSettings);
	}

	private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
	{
		OnDataCollectionSettingsChanged(GetDataCollectionSettings());
	}

	private void cboParseFilter_SelectedIndexChanged(object sender, EventArgs e)
	{
		OnParseSettingsChanged(ParseSettings);
	}

	private void chkDisableDamageShield_CheckedChanged(object sender, EventArgs e)
	{
		OnParseSettingsChanged(ParseSettings);
	}

	private void chkDisableCombinePets_CheckedChanged(object sender, EventArgs e)
	{
		OnParseSettingsChanged(ParseSettings);
	}

	private void chkSimulateDoTCrits_CheckedChanged(object sender, EventArgs e)
	{
		OnParseSettingsChanged(ParseSettings);
	}

	private void chkShowRealDoTs_CheckedChanged(object sender, EventArgs e)
	{
		OnParseSettingsChanged(ParseSettings);
	}

	private void chkEnableBenchmark_CheckedChanged(object sender, EventArgs e)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		OnParseSettingsChanged(ParseSettings);
		if (chkEnableBenchmark.Checked)
		{
			_benchmarkTabPage = new TabPage();
			((Control)_benchmarkTabPage).Controls.Add((Control)(object)_benchmarkPropertyPage);
			((Control)_benchmarkTabPage).Text = "Plugin Benchmark";
			((Control)_benchmarkPropertyPage).Dock = (DockStyle)5;
			((Control)this).Parent.Parent.Controls.Add((Control)(object)_benchmarkTabPage);
		}
		else
		{
			if (_benchmarkTabPage != null)
			{
				((Control)this).Parent.Parent.Controls.Remove((Control)(object)_benchmarkTabPage);
			}
			((Component)(object)_benchmarkTabPage)?.Dispose();
			_benchmarkTabPage = null;
		}
	}

	public DataCollectionSettingsEventArgs GetDataCollectionSettings()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		DataCollectionSettingsEventArgs val = new DataCollectionSettingsEventArgs();
		int.TryParse(((string)cboProcessID.SelectedItem) ?? "", out var result);
		val.ProcessID = result;
		val.LogFileFolder = ((Control)txtLogFileDirectory).Text;
		val.LogAllNetworkData = chkLogAllNetwork.Checked;
		val.DisableCombatLog = chkDisableCombatLog.Checked;
		val.RegionID = (Region)(((ListControl)cboRegion).SelectedIndex + 1);
		return val;
	}

	public void AddParserMessage(DateTime timestamp, string message)
	{
		ACTWrapper.RunOnACTUIThread(delegate
		{
			if (lstMessages.Items.Count < 100 || chkShowDebug.Checked)
			{
				lstMessages.Items.Add((object)$"[{timestamp:HH:mm:ss.fff}] {message}");
			}
		});
	}

	private void cmdCopyProblematic_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (object item in lstMessages.Items)
		{
			stringBuilder.AppendLine((item ?? "").ToString());
		}
		if (stringBuilder.Length > 0)
		{
			Clipboard.SetText(stringBuilder.ToString());
		}
	}

	private void cmdClearMessages_Click(object sender, EventArgs e)
	{
		lstMessages.Items.Clear();
	}

	protected virtual void OnParseSettingsChanged(ParseSettings settings)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (m_Initialized)
		{
			_settingsManager.ParseSettings = settings;
			string text = _logFormat.FormatParseSettings(settings.DisableDamageShield, settings.DisableCombinePets, settings.LanguageID, settings.ParseFilter, settings.SimulateIndividualDoTCrits, settings.ShowRealDoTTicks);
			_logOutput.WriteLine((LogMessageType)249, DateTime.MinValue, text);
		}
	}

	protected virtual void OnDataCollectionSettingsChanged(DataCollectionSettingsEventArgs settings)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (m_Initialized)
		{
			_settingsManager.DataCollectionSettings = settings;
			string text = _logFormat.FormatMemorySettings(settings.ProcessID, settings.LogFileFolder, settings.LogAllNetworkData, settings.DisableCombatLog, settings.RegionID);
			_logOutput.WriteLine((LogMessageType)249, DateTime.MinValue, text);
		}
	}

	private void chkGraphPotency_CheckedChanged(object sender, EventArgs e)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		if (chkGraphPotency.Checked)
		{
			m_oldGenerator = ACTWrapper.GenerateAttackTypeGraph;
			ACTWrapper.GenerateAttackTypeGraph = new AttackTypeGraphGenerator(ACT_UIMods.GenAttackTypeGraph);
		}
		else if (m_oldGenerator != null)
		{
			ACTWrapper.GenerateAttackTypeGraph = m_oldGenerator;
		}
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		MessageBox.Show("The ACT FFXIV Plugin is Copyright © 2025 Ravahn.\r\n\r\nThis software makes use of libraries covered under the MIT License: \r\n\r\nCostura/Fody: https://github.com/Fody/Costura\r\n\r\nFody: https://github.com/Fody/Fody\r\n\r\nMicrosoft.MinIoc: https://github.com/microsoft/MinIoC", "Copyright Notice", (MessageBoxButtons)0);
	}

	private void InitializeComponent()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b9: Expected O, but got Unknown
		lblEventRollover = new Label();
		chkDisableDamageShield = new CheckBox();
		cboLanguage = new ComboBox();
		label2 = new Label();
		groupBox1 = new GroupBox();
		chkDisableCombinePets = new CheckBox();
		label6 = new Label();
		cboParseFilter = new ComboBox();
		chkShowRealDoTs = new CheckBox();
		chkSimulateDoTCrits = new CheckBox();
		chkGraphPotency = new CheckBox();
		lstMessages = new ListBox();
		cmdClearMessages = new Button();
		cmdCopyProblematic = new Button();
		label1 = new Label();
		cmdValidateAddress = new Button();
		cboProcessID = new ComboBox();
		label7 = new Label();
		cmdReloadProcess = new Button();
		cmdOpenExportFolder = new Button();
		txtLogFileDirectory = new TextBox();
		cmdChangeFolder = new Button();
		groupBox3 = new GroupBox();
		cboRegion = new ComboBox();
		label3 = new Label();
		chkDisableCombatLog = new CheckBox();
		cmdResetLogPath = new Button();
		chkLogAllNetwork = new CheckBox();
		folderBrowserDialog1 = new FolderBrowserDialog();
		chkShowDebug = new CheckBox();
		grpDebug = new GroupBox();
		chkEnableBenchmark = new CheckBox();
		groupBox2 = new GroupBox();
		linkLabel1 = new LinkLabel();
		((Control)groupBox1).SuspendLayout();
		((Control)groupBox3).SuspendLayout();
		((Control)grpDebug).SuspendLayout();
		((Control)groupBox2).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)lblEventRollover).AutoSize = true;
		((Control)lblEventRollover).Location = new Point(34, -18);
		((Control)lblEventRollover).Name = "lblEventRollover";
		((Control)lblEventRollover).Size = new Size(0, 13);
		((Control)lblEventRollover).TabIndex = 3;
		((Control)chkDisableDamageShield).AutoSize = true;
		((Control)chkDisableDamageShield).Location = new Point(13, 82);
		((Control)chkDisableDamageShield).Name = "chkDisableDamageShield";
		((Control)chkDisableDamageShield).Size = new Size(183, 17);
		((Control)chkDisableDamageShield).TabIndex = 38;
		((Control)chkDisableDamageShield).Text = "Disable Damage Shield estimates";
		((ButtonBase)chkDisableDamageShield).UseVisualStyleBackColor = true;
		chkDisableDamageShield.CheckedChanged += chkDisableDamageShield_CheckedChanged;
		cboLanguage.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)cboLanguage).FormattingEnabled = true;
		cboLanguage.Items.AddRange(new object[7] { "English", "Français", "Deutsch", "日本語", "中文", "한국어", "繁體中文" });
		((Control)cboLanguage).Location = new Point(113, 27);
		((Control)cboLanguage).Name = "cboLanguage";
		((Control)cboLanguage).Size = new Size(121, 21);
		((Control)cboLanguage).TabIndex = 41;
		cboLanguage.SelectedIndexChanged += cboLanguage_SelectedIndexChanged;
		((Control)label2).AutoSize = true;
		((Control)label2).Location = new Point(10, 30);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(58, 13);
		((Control)label2).TabIndex = 43;
		((Control)label2).Text = "Language:";
		((Control)groupBox1).Controls.Add((Control)(object)chkDisableCombinePets);
		((Control)groupBox1).Controls.Add((Control)(object)label6);
		((Control)groupBox1).Controls.Add((Control)(object)cboParseFilter);
		((Control)groupBox1).Controls.Add((Control)(object)chkDisableDamageShield);
		((Control)groupBox1).Controls.Add((Control)(object)label2);
		((Control)groupBox1).Controls.Add((Control)(object)cboLanguage);
		((Control)groupBox1).Location = new Point(474, 11);
		((Control)groupBox1).Name = "groupBox1";
		((Control)groupBox1).Size = new Size(240, 129);
		((Control)groupBox1).TabIndex = 44;
		groupBox1.TabStop = false;
		((Control)groupBox1).Text = "Parse Options";
		((Control)chkDisableCombinePets).AutoSize = true;
		((Control)chkDisableCombinePets).Location = new Point(13, 105);
		((Control)chkDisableCombinePets).Name = "chkDisableCombinePets";
		((Control)chkDisableCombinePets).Size = new Size(185, 17);
		((Control)chkDisableCombinePets).TabIndex = 47;
		((Control)chkDisableCombinePets).Text = "Disable Combine Pets with Owner";
		((ButtonBase)chkDisableCombinePets).UseVisualStyleBackColor = true;
		chkDisableCombinePets.CheckedChanged += chkDisableCombinePets_CheckedChanged;
		((Control)label6).AutoSize = true;
		((Control)label6).Location = new Point(10, 58);
		((Control)label6).Name = "label6";
		((Control)label6).Size = new Size(62, 13);
		((Control)label6).TabIndex = 46;
		((Control)label6).Text = "Parse Filter:";
		cboParseFilter.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)cboParseFilter).FormattingEnabled = true;
		cboParseFilter.Items.AddRange(new object[4] { "None", "Self", "Party", "Alliance" });
		((Control)cboParseFilter).Location = new Point(113, 55);
		((Control)cboParseFilter).Name = "cboParseFilter";
		((Control)cboParseFilter).Size = new Size(121, 21);
		((Control)cboParseFilter).TabIndex = 45;
		cboParseFilter.SelectedIndexChanged += cboParseFilter_SelectedIndexChanged;
		((Control)chkShowRealDoTs).AutoSize = true;
		((Control)chkShowRealDoTs).Location = new Point(274, 19);
		((Control)chkShowRealDoTs).Name = "chkShowRealDoTs";
		((Control)chkShowRealDoTs).Size = new Size(205, 17);
		((Control)chkShowRealDoTs).TabIndex = 76;
		((Control)chkShowRealDoTs).Text = "(DEBUG) Also Show 'Real' DoT Ticks";
		((ButtonBase)chkShowRealDoTs).UseVisualStyleBackColor = true;
		chkShowRealDoTs.CheckedChanged += chkShowRealDoTs_CheckedChanged;
		((Control)chkSimulateDoTCrits).AutoSize = true;
		((Control)chkSimulateDoTCrits).Location = new Point(274, 42);
		((Control)chkSimulateDoTCrits).Name = "chkSimulateDoTCrits";
		((Control)chkSimulateDoTCrits).Size = new Size(208, 17);
		((Control)chkSimulateDoTCrits).TabIndex = 75;
		((Control)chkSimulateDoTCrits).Text = "(DEBUG) Simulate Individual DoT Crits";
		((ButtonBase)chkSimulateDoTCrits).UseVisualStyleBackColor = true;
		chkSimulateDoTCrits.CheckedChanged += chkSimulateDoTCrits_CheckedChanged;
		((Control)chkGraphPotency).AutoSize = true;
		((Control)chkGraphPotency).Location = new Point(521, 19);
		((Control)chkGraphPotency).Name = "chkGraphPotency";
		((Control)chkGraphPotency).Size = new Size(208, 17);
		((Control)chkGraphPotency).TabIndex = 74;
		((Control)chkGraphPotency).Text = "(DEBUG) Graph Potency, not Damage";
		((ButtonBase)chkGraphPotency).UseVisualStyleBackColor = true;
		((Control)chkGraphPotency).Visible = false;
		chkGraphPotency.CheckedChanged += chkGraphPotency_CheckedChanged;
		((ListControl)lstMessages).FormattingEnabled = true;
		((Control)lstMessages).Location = new Point(6, 81);
		((Control)lstMessages).Name = "lstMessages";
		lstMessages.ScrollAlwaysVisible = true;
		((Control)lstMessages).Size = new Size(925, 199);
		((Control)lstMessages).TabIndex = 80;
		((Control)cmdClearMessages).Location = new Point(348, 289);
		((Control)cmdClearMessages).Name = "cmdClearMessages";
		((Control)cmdClearMessages).Size = new Size(97, 26);
		((Control)cmdClearMessages).TabIndex = 82;
		((Control)cmdClearMessages).Text = "Clear";
		((ButtonBase)cmdClearMessages).UseVisualStyleBackColor = true;
		((Control)cmdClearMessages).Click += cmdClearMessages_Click;
		((Control)cmdCopyProblematic).Location = new Point(460, 289);
		((Control)cmdCopyProblematic).Name = "cmdCopyProblematic";
		((Control)cmdCopyProblematic).Size = new Size(109, 26);
		((Control)cmdCopyProblematic).TabIndex = 81;
		((Control)cmdCopyProblematic).Text = "Copy to Clipboard";
		((ButtonBase)cmdCopyProblematic).UseVisualStyleBackColor = true;
		((Control)cmdCopyProblematic).Click += cmdCopyProblematic_Click;
		((Control)label1).AutoSize = true;
		((Control)label1).Location = new Point(3, 64);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(90, 13);
		((Control)label1).TabIndex = 83;
		((Control)label1).Text = "Debug Messages";
		((Control)cmdValidateAddress).Location = new Point(293, 99);
		((Control)cmdValidateAddress).Name = "cmdValidateAddress";
		((Control)cmdValidateAddress).Size = new Size(132, 23);
		((Control)cmdValidateAddress).TabIndex = 61;
		((Control)cmdValidateAddress).Text = "Test Game Connection";
		((ButtonBase)cmdValidateAddress).UseVisualStyleBackColor = true;
		((Control)cmdValidateAddress).Click += cmdValidateAddress_Click;
		cboProcessID.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)cboProcessID).FormattingEnabled = true;
		((Control)cboProcessID).Location = new Point(141, 27);
		((Control)cboProcessID).Name = "cboProcessID";
		((Control)cboProcessID).Size = new Size(146, 21);
		((Control)cboProcessID).TabIndex = 62;
		cboProcessID.SelectedIndexChanged += cboProcessID_SelectedIndexChanged;
		((Control)label7).AutoSize = true;
		((Control)label7).Location = new Point(16, 30);
		((Control)label7).Name = "label7";
		((Control)label7).Size = new Size(94, 13);
		((Control)label7).TabIndex = 63;
		((Control)label7).Text = "FFXIV Process ID:";
		((Control)cmdReloadProcess).Location = new Point(293, 27);
		((Control)cmdReloadProcess).Name = "cmdReloadProcess";
		((Control)cmdReloadProcess).Size = new Size(132, 23);
		((Control)cmdReloadProcess).TabIndex = 64;
		((Control)cmdReloadProcess).Text = "Refresh List";
		((ButtonBase)cmdReloadProcess).UseVisualStyleBackColor = true;
		((Control)cmdReloadProcess).Click += cmdReloadProcess_Click;
		((Control)cmdOpenExportFolder).Location = new Point(18, 53);
		((Control)cmdOpenExportFolder).Name = "cmdOpenExportFolder";
		((Control)cmdOpenExportFolder).Size = new Size(140, 23);
		((Control)cmdOpenExportFolder).TabIndex = 88;
		((Control)cmdOpenExportFolder).Text = "Open Output Log Folder";
		((ButtonBase)cmdOpenExportFolder).UseVisualStyleBackColor = true;
		((Control)cmdOpenExportFolder).Click += cmdOpenExportFolder_Click;
		((Control)txtLogFileDirectory).Location = new Point(20, 24);
		((TextBoxBase)txtLogFileDirectory).Multiline = true;
		((Control)txtLogFileDirectory).Name = "txtLogFileDirectory";
		((TextBoxBase)txtLogFileDirectory).ReadOnly = true;
		((Control)txtLogFileDirectory).Size = new Size(674, 26);
		((Control)txtLogFileDirectory).TabIndex = 87;
		((Control)cmdChangeFolder).Location = new Point(562, 53);
		((Control)cmdChangeFolder).Name = "cmdChangeFolder";
		((Control)cmdChangeFolder).Size = new Size(132, 23);
		((Control)cmdChangeFolder).TabIndex = 89;
		((Control)cmdChangeFolder).Text = "Change";
		((ButtonBase)cmdChangeFolder).UseVisualStyleBackColor = true;
		((Control)cmdChangeFolder).Click += cmdChangeFolder_Click;
		((Control)groupBox3).Controls.Add((Control)(object)cboRegion);
		((Control)groupBox3).Controls.Add((Control)(object)label3);
		((Control)groupBox3).Controls.Add((Control)(object)chkDisableCombatLog);
		((Control)groupBox3).Controls.Add((Control)(object)cmdReloadProcess);
		((Control)groupBox3).Controls.Add((Control)(object)label7);
		((Control)groupBox3).Controls.Add((Control)(object)cmdValidateAddress);
		((Control)groupBox3).Controls.Add((Control)(object)cboProcessID);
		((Control)groupBox3).Location = new Point(14, 11);
		((Control)groupBox3).Name = "groupBox3";
		((Control)groupBox3).Size = new Size(445, 129);
		((Control)groupBox3).TabIndex = 60;
		groupBox3.TabStop = false;
		((Control)groupBox3).Text = "FFXIV Game Data Collection";
		cboRegion.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)cboRegion).FormattingEnabled = true;
		cboRegion.Items.AddRange(new object[4] { "Global", "中文", "한국어", "繁體中文" });
		((Control)cboRegion).Location = new Point(141, 55);
		((Control)cboRegion).Name = "cboRegion";
		((Control)cboRegion).Size = new Size(146, 21);
		((Control)cboRegion).TabIndex = 48;
		cboRegion.SelectedIndexChanged += cboRegion_SelectedIndexChanged;
		((Control)label3).AutoSize = true;
		((Control)label3).Location = new Point(17, 58);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(107, 13);
		((Control)label3).TabIndex = 93;
		((Control)label3).Text = "FFXIV Game Region:";
		((Control)chkDisableCombatLog).AutoSize = true;
		((Control)chkDisableCombatLog).Location = new Point(6, 99);
		((Control)chkDisableCombatLog).Name = "chkDisableCombatLog";
		((Control)chkDisableCombatLog).Size = new Size(152, 17);
		((Control)chkDisableCombatLog).TabIndex = 92;
		((Control)chkDisableCombatLog).Text = "Hide Chat Log (for privacy)";
		((ButtonBase)chkDisableCombatLog).UseVisualStyleBackColor = true;
		((Control)cmdResetLogPath).Location = new Point(424, 53);
		((Control)cmdResetLogPath).Name = "cmdResetLogPath";
		((Control)cmdResetLogPath).Size = new Size(132, 23);
		((Control)cmdResetLogPath).TabIndex = 90;
		((Control)cmdResetLogPath).Text = "Reset to Default";
		((ButtonBase)cmdResetLogPath).UseVisualStyleBackColor = true;
		((Control)cmdResetLogPath).Click += cmdResetLogPath_Click;
		((Control)chkLogAllNetwork).AutoSize = true;
		((Control)chkLogAllNetwork).Location = new Point(19, 19);
		((Control)chkLogAllNetwork).Name = "chkLogAllNetwork";
		((Control)chkLogAllNetwork).Size = new Size(189, 17);
		((Control)chkLogAllNetwork).TabIndex = 78;
		((Control)chkLogAllNetwork).Text = "(DEBUG) Log all Network Packets";
		((ButtonBase)chkLogAllNetwork).UseVisualStyleBackColor = true;
		chkLogAllNetwork.CheckedChanged += chkLogAllNetwork_CheckedChanged;
		((Control)chkShowDebug).AutoSize = true;
		((Control)chkShowDebug).Location = new Point(34, 234);
		((Control)chkShowDebug).Name = "chkShowDebug";
		((Control)chkShowDebug).Size = new Size(180, 17);
		((Control)chkShowDebug).TabIndex = 79;
		((Control)chkShowDebug).Text = "(DEBUG) Enable Debug Options";
		((ButtonBase)chkShowDebug).UseVisualStyleBackColor = true;
		chkShowDebug.CheckedChanged += chkShowDebug_CheckedChanged;
		((Control)grpDebug).Controls.Add((Control)(object)chkEnableBenchmark);
		((Control)grpDebug).Controls.Add((Control)(object)label1);
		((Control)grpDebug).Controls.Add((Control)(object)cmdClearMessages);
		((Control)grpDebug).Controls.Add((Control)(object)cmdCopyProblematic);
		((Control)grpDebug).Controls.Add((Control)(object)lstMessages);
		((Control)grpDebug).Controls.Add((Control)(object)chkShowRealDoTs);
		((Control)grpDebug).Controls.Add((Control)(object)chkSimulateDoTCrits);
		((Control)grpDebug).Controls.Add((Control)(object)chkLogAllNetwork);
		((Control)grpDebug).Controls.Add((Control)(object)chkGraphPotency);
		((Control)grpDebug).Location = new Point(14, 258);
		((Control)grpDebug).Name = "grpDebug";
		((Control)grpDebug).Size = new Size(937, 330);
		((Control)grpDebug).TabIndex = 75;
		grpDebug.TabStop = false;
		((Control)chkEnableBenchmark).AutoSize = true;
		((Control)chkEnableBenchmark).Location = new Point(20, 42);
		((Control)chkEnableBenchmark).Name = "chkEnableBenchmark";
		((Control)chkEnableBenchmark).Size = new Size(185, 17);
		((Control)chkEnableBenchmark).TabIndex = 84;
		((Control)chkEnableBenchmark).Text = "(DEBUG) Enable Benchmark Tab";
		((ButtonBase)chkEnableBenchmark).UseVisualStyleBackColor = true;
		chkEnableBenchmark.CheckedChanged += chkEnableBenchmark_CheckedChanged;
		((Control)groupBox2).Controls.Add((Control)(object)cmdResetLogPath);
		((Control)groupBox2).Controls.Add((Control)(object)cmdChangeFolder);
		((Control)groupBox2).Controls.Add((Control)(object)txtLogFileDirectory);
		((Control)groupBox2).Controls.Add((Control)(object)cmdOpenExportFolder);
		((Control)groupBox2).Location = new Point(14, 146);
		((Control)groupBox2).Name = "groupBox2";
		((Control)groupBox2).Size = new Size(700, 83);
		((Control)groupBox2).TabIndex = 87;
		groupBox2.TabStop = false;
		((Control)groupBox2).Text = "Output Log File Location";
		((Control)linkLabel1).AutoSize = true;
		((Control)linkLabel1).Location = new Point(532, 235);
		((Control)linkLabel1).Name = "linkLabel1";
		((Control)linkLabel1).Size = new Size(90, 13);
		((Control)linkLabel1).TabIndex = 88;
		linkLabel1.TabStop = true;
		((Control)linkLabel1).Text = "Copyright Notices";
		linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Control)this).Controls.Add((Control)(object)linkLabel1);
		((Control)this).Controls.Add((Control)(object)groupBox2);
		((Control)this).Controls.Add((Control)(object)grpDebug);
		((Control)this).Controls.Add((Control)(object)groupBox3);
		((Control)this).Controls.Add((Control)(object)groupBox1);
		((Control)this).Controls.Add((Control)(object)lblEventRollover);
		((Control)this).Controls.Add((Control)(object)chkShowDebug);
		((Control)this).Name = "SettingsPropertyPage";
		((Control)this).Size = new Size(969, 601);
		((Control)groupBox1).ResumeLayout(false);
		((Control)groupBox1).PerformLayout();
		((Control)groupBox3).ResumeLayout(false);
		((Control)groupBox3).PerformLayout();
		((Control)grpDebug).ResumeLayout(false);
		((Control)grpDebug).PerformLayout();
		((Control)groupBox2).ResumeLayout(false);
		((Control)groupBox2).PerformLayout();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
