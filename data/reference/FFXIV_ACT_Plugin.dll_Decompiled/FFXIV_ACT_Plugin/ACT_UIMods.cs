using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using FFXIV_ACT_Plugin.Common;
using FFXIV_ACT_Plugin.Parse;

namespace FFXIV_ACT_Plugin;

public class ACT_UIMods
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static ExportStringDataCallback _003C_003E9__14_0;

		public static ExportStringDataCallback _003C_003E9__14_1;

		public static ExportStringDataCallback _003C_003E9__14_2;

		public static ExportStringDataCallback _003C_003E9__14_3;

		public static ExportStringDataCallback _003C_003E9__14_4;

		public static ExportStringDataCallback _003C_003E9__14_5;

		public static ExportStringDataCallback _003C_003E9__14_6;

		public static StringDataCallback _003C_003E9__14_7;

		public static StringDataCallback _003C_003E9__14_8;

		public static Comparison<MasterSwing> _003C_003E9__14_9;

		public static StringDataCallback _003C_003E9__14_10;

		public static StringDataCallback _003C_003E9__14_11;

		public static Comparison<CombatantData> _003C_003E9__14_12;

		public static ExportStringDataCallback _003C_003E9__14_13;

		public static StringDataCallback _003C_003E9__14_14;

		public static StringDataCallback _003C_003E9__14_15;

		public static Comparison<CombatantData> _003C_003E9__14_16;

		public static ExportStringDataCallback _003C_003E9__14_17;

		public static StringDataCallback _003C_003E9__14_18;

		public static StringDataCallback _003C_003E9__14_19;

		public static Comparison<CombatantData> _003C_003E9__14_20;

		public static ExportStringDataCallback _003C_003E9__14_21;

		public static StringDataCallback _003C_003E9__14_22;

		public static StringDataCallback _003C_003E9__14_23;

		public static Comparison<CombatantData> _003C_003E9__14_24;

		public static ExportStringDataCallback _003C_003E9__14_25;

		public static StringDataCallback _003C_003E9__14_26;

		public static StringDataCallback _003C_003E9__14_27;

		public static StringDataCallback _003C_003E9__14_28;

		public static StringDataCallback _003C_003E9__14_29;

		public static StringDataCallback _003C_003E9__14_30;

		public static StringDataCallback _003C_003E9__14_31;

		public static Comparison<AttackType> _003C_003E9__14_32;

		public static StringDataCallback _003C_003E9__14_33;

		public static StringDataCallback _003C_003E9__14_34;

		public static Comparison<AttackType> _003C_003E9__14_35;

		public static StringDataCallback _003C_003E9__14_36;

		public static StringDataCallback _003C_003E9__14_37;

		public static Comparison<AttackType> _003C_003E9__14_38;

		public static StringDataCallback _003C_003E9__14_39;

		public static StringDataCallback _003C_003E9__14_40;

		public static Comparison<AttackType> _003C_003E9__14_41;

		public static StringDataCallback _003C_003E9__14_42;

		public static StringDataCallback _003C_003E9__14_43;

		public static Comparison<CombatantData> _003C_003E9__14_44;

		public static ExportStringDataCallback _003C_003E9__14_45;

		public static StringDataCallback _003C_003E9__14_46;

		public static StringDataCallback _003C_003E9__14_47;

		public static StringDataCallback _003C_003E9__14_48;

		public static StringDataCallback _003C_003E9__14_49;

		public static Comparison<AttackType> _003C_003E9__14_50;

		public static StringDataCallback _003C_003E9__14_51;

		public static StringDataCallback _003C_003E9__14_52;

		public static Comparison<MasterSwing> _003C_003E9__14_53;

		public static StringDataCallback _003C_003E9__14_54;

		public static StringDataCallback _003C_003E9__14_55;

		public static Comparison<MasterSwing> _003C_003E9__14_56;

		public static StringDataCallback _003C_003E9__14_57;

		public static StringDataCallback _003C_003E9__14_58;

		public static Comparison<AttackType> _003C_003E9__14_59;

		public static StringDataCallback _003C_003E9__14_60;

		public static StringDataCallback _003C_003E9__14_61;

		public static StringDataCallback _003C_003E9__14_62;

		public static StringDataCallback _003C_003E9__14_63;

		public static Comparison<CombatantData> _003C_003E9__14_64;

		public static ExportStringDataCallback _003C_003E9__14_65;

		public static StringDataCallback _003C_003E9__14_66;

		public static StringDataCallback _003C_003E9__14_67;

		public static Comparison<AttackType> _003C_003E9__14_68;

		public static StringDataCallback _003C_003E9__14_69;

		public static StringDataCallback _003C_003E9__14_70;

		public static StringDataCallback _003C_003E9__14_71;

		public static StringDataCallback _003C_003E9__14_72;

		public static Comparison<CombatantData> _003C_003E9__14_73;

		public static ExportStringDataCallback _003C_003E9__14_74;

		public static StringDataCallback _003C_003E9__14_75;

		public static StringDataCallback _003C_003E9__14_76;

		public static Comparison<AttackType> _003C_003E9__14_77;

		public static StringDataCallback _003C_003E9__14_78;

		public static StringDataCallback _003C_003E9__14_79;

		public static StringDataCallback _003C_003E9__14_80;

		public static StringDataCallback _003C_003E9__14_81;

		public static Comparison<CombatantData> _003C_003E9__14_82;

		public static ExportStringDataCallback _003C_003E9__14_83;

		public static StringDataCallback _003C_003E9__14_84;

		public static StringDataCallback _003C_003E9__14_85;

		public static Comparison<AttackType> _003C_003E9__14_86;

		public static StringDataCallback _003C_003E9__14_87;

		public static StringDataCallback _003C_003E9__14_88;

		public static StringDataCallback _003C_003E9__14_89;

		public static StringDataCallback _003C_003E9__14_90;

		public static Comparison<CombatantData> _003C_003E9__14_91;

		public static ExportStringDataCallback _003C_003E9__14_92;

		public static StringDataCallback _003C_003E9__14_93;

		public static StringDataCallback _003C_003E9__14_94;

		public static Comparison<MasterSwing> _003C_003E9__14_95;

		public static StringDataCallback _003C_003E9__14_96;

		public static StringDataCallback _003C_003E9__14_97;

		public static Comparison<MasterSwing> _003C_003E9__14_98;

		public static StringDataCallback _003C_003E9__14_99;

		public static StringDataCallback _003C_003E9__14_100;

		public static Comparison<MasterSwing> _003C_003E9__14_101;

		public static StringDataCallback _003C_003E9__14_102;

		public static StringDataCallback _003C_003E9__14_103;

		public static Comparison<MasterSwing> _003C_003E9__14_104;

		public static StringDataCallback _003C_003E9__14_105;

		public static StringDataCallback _003C_003E9__14_106;

		public static Comparison<MasterSwing> _003C_003E9__14_107;

		public static StringDataCallback _003C_003E9__14_108;

		public static StringDataCallback _003C_003E9__14_109;

		public static Comparison<MasterSwing> _003C_003E9__14_110;

		public static StringDataCallback _003C_003E9__14_111;

		public static StringDataCallback _003C_003E9__14_112;

		public static Comparison<MasterSwing> _003C_003E9__14_113;

		public static StringDataCallback _003C_003E9__14_114;

		public static StringDataCallback _003C_003E9__14_115;

		public static Comparison<MasterSwing> _003C_003E9__14_116;

		public static StringDataCallback _003C_003E9__14_117;

		public static StringDataCallback _003C_003E9__14_118;

		public static Comparison<MasterSwing> _003C_003E9__14_119;

		public static StringDataCallback _003C_003E9__14_120;

		public static StringDataCallback _003C_003E9__14_121;

		public static Comparison<MasterSwing> _003C_003E9__14_122;

		public static Func<MasterSwing, bool> _003C_003E9__17_0;

		internal string _003CUpdateACTTables_003Eb__14_0(EncounterData data, List<CombatantData> SelectiveAllies, string extra)
		{
			return data.ZoneName;
		}

		internal string _003CUpdateACTTables_003Eb__14_1(CombatantData Data, string ExtraFormat)
		{
			return Data.LastNDPS(10).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_2(CombatantData Data, string ExtraFormat)
		{
			return Data.LastNDPS(30).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_3(CombatantData Data, string ExtraFormat)
		{
			return Data.LastNDPS(60).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_4(EncounterData Data, List<CombatantData> SelectiveAllies, string Extra)
		{
			return Data.LastNDPS(SelectiveAllies, 10).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_5(EncounterData Data, List<CombatantData> SelectiveAllies, string Extra)
		{
			return Data.LastNDPS(SelectiveAllies, 30).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_6(EncounterData Data, List<CombatantData> SelectiveAllies, string Extra)
		{
			return Data.LastNDPS(SelectiveAllies, 60).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_7(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("StatusDuration"))
			{
				return "";
			}
			return ((double)Data.Tags["StatusDuration"]).ToString("0.#", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_8(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("StatusDuration"))
			{
				return "";
			}
			return ((double)Data.Tags["StatusDuration"]).ToString("0.#", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_9(MasterSwing Left, MasterSwing Right)
		{
			return (Left.Tags.ContainsKey("StatusDuration") ? ((double)Left.Tags["StatusDuration"]) : 0.0).CompareTo(Right.Tags.ContainsKey("StatusDuration") ? ((double)Right.Tags["StatusDuration"]) : 0.0);
		}

		internal string _003CUpdateACTTables_003Eb__14_10(CombatantData Data)
		{
			return Data.Job();
		}

		internal string _003CUpdateACTTables_003Eb__14_11(CombatantData Data)
		{
			return Data.Job();
		}

		internal int _003CUpdateACTTables_003Eb__14_12(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Job(), Right.Job(), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_13(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("Job");
		}

		internal string _003CUpdateACTTables_003Eb__14_14(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_15(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct");
		}

		internal int _003CUpdateACTTables_003Eb__14_16(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct"), Right.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_17(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("ParryPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_18(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_19(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct");
		}

		internal int _003CUpdateACTTables_003Eb__14_20(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct"), Right.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_21(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("BlockPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_22(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit");
		}

		internal string _003CUpdateACTTables_003Eb__14_23(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit");
		}

		internal int _003CUpdateACTTables_003Eb__14_24(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit"), Right.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_25(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("IncToHit");
		}

		internal string _003CUpdateACTTables_003Eb__14_26(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("ParryPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_27(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("ParryPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_28(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("BlockPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_29(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("BlockPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_30(AttackType Data)
		{
			return Data.Parry().ToString(CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_31(AttackType Data)
		{
			return Data.Parry().ToString(CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_32(AttackType Left, AttackType Right)
		{
			return Left.Parry().CompareTo(Right.Parry());
		}

		internal string _003CUpdateACTTables_003Eb__14_33(AttackType Data)
		{
			return ((double)Data.Parry() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_34(AttackType Data)
		{
			return ((double)Data.Parry() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_35(AttackType Left, AttackType Right)
		{
			return Left.Parry().CompareTo(Right.Parry());
		}

		internal string _003CUpdateACTTables_003Eb__14_36(AttackType Data)
		{
			return Data.Block().ToString(CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_37(AttackType Data)
		{
			return Data.Block().ToString(CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_38(AttackType Left, AttackType Right)
		{
			return Left.Block().CompareTo(Right.Block());
		}

		internal string _003CUpdateACTTables_003Eb__14_39(AttackType Data)
		{
			return ((double)Data.Block() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_40(AttackType Data)
		{
			return ((double)Data.Block() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_41(AttackType Left, AttackType Right)
		{
			return Left.Block().CompareTo(Right.Block());
		}

		internal string _003CUpdateACTTables_003Eb__14_42(CombatantData Data)
		{
			return (long.Parse(Data.Items[CombatantData.DamageTypeDataOutgoingHealing].GetColumnByName("OverHeal"), CultureInfo.InvariantCulture) * 100 / OneOrInt((!Data.Items[CombatantData.DamageTypeDataOutgoingHealing].Items.ContainsKey("All")) ? 0 : Data.DirectHeal())).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_43(CombatantData Data)
		{
			return (long.Parse(Data.Items[CombatantData.DamageTypeDataOutgoingHealing].GetColumnByName("OverHeal"), CultureInfo.InvariantCulture) * 100 / OneOrInt((!Data.Items[CombatantData.DamageTypeDataOutgoingHealing].Items.ContainsKey("All")) ? 0 : Data.DirectHeal())).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_44(CombatantData Left, CombatantData Right)
		{
			return long.Parse(Left.GetColumnByName("OverHealPct").Replace('%', ' '), CultureInfo.InvariantCulture).CompareTo(long.Parse(Right.GetColumnByName("OverHealPct").Replace('%', ' '), CultureInfo.InvariantCulture));
		}

		internal string _003CUpdateACTTables_003Eb__14_45(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("OverHealPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_46(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return "0";
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("OverHeal");
		}

		internal string _003CUpdateACTTables_003Eb__14_47(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return "0";
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("OverHeal");
		}

		internal string _003CUpdateACTTables_003Eb__14_48(AttackType Data)
		{
			return Data.Overheal().ToString(CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_49(AttackType Data)
		{
			return Data.Overheal().ToString(CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_50(AttackType Left, AttackType Right)
		{
			return Left.Overheal().CompareTo(Right.Overheal());
		}

		internal string _003CUpdateACTTables_003Eb__14_51(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("overheal"))
			{
				return "0";
			}
			return Data.Tags["overheal"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_52(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("overheal"))
			{
				return "0";
			}
			return Data.Tags["overheal"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_53(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("overheal") ? Left.Tags["overheal"].ToString() : "0", Right.Tags.ContainsKey("overheal") ? Right.Tags["overheal"].ToString() : "0", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_54(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("DirectHit"))
			{
				return "";
			}
			return Data.Tags["DirectHit"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_55(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("DirectHit"))
			{
				return "";
			}
			return Data.Tags["DirectHit"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_56(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("DirectHit") ? Left.Tags["DirectHit"].ToString() : "", Right.Tags.ContainsKey("DirectHit") ? Right.Tags["DirectHit"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_57(AttackType Data)
		{
			return ((double)Data.DirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_58(AttackType Data)
		{
			return ((double)Data.DirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_59(AttackType Left, AttackType Right)
		{
			return Left.DirectHitCount().CompareTo(Right.DirectHitCount());
		}

		internal string _003CUpdateACTTables_003Eb__14_60(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0.0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_61(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0.0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_62(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_63(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct");
		}

		internal int _003CUpdateACTTables_003Eb__14_64(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_65(CombatantData Data, string ExtraFormat)
		{
			return ((Data.GetColumnByName("DirectHitPct") == "") ? 0.0 : Convert.ToDouble(Data.GetColumnByName("DirectHitPct").Replace("%", ""), CultureInfo.InvariantCulture)).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_66(AttackType Data)
		{
			return Data.DirectHitCount().ToString(CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_67(AttackType Data)
		{
			return Data.DirectHitCount().ToString(CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_68(AttackType Left, AttackType Right)
		{
			return Left.DirectHitCount().CompareTo(Right.DirectHitCount());
		}

		internal string _003CUpdateACTTables_003Eb__14_69(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return "0";
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_70(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return "0";
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_71(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_72(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount");
		}

		internal int _003CUpdateACTTables_003Eb__14_73(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_74(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("DirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_75(AttackType Data)
		{
			return Data.CritDirectHitCount().ToString(CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_76(AttackType Data)
		{
			return Data.CritDirectHitCount().ToString(CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_77(AttackType Left, AttackType Right)
		{
			return Left.CritDirectHitCount().CompareTo(Right.CritDirectHitCount());
		}

		internal string _003CUpdateACTTables_003Eb__14_78(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return "0";
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_79(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return "0";
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_80(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_81(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount");
		}

		internal int _003CUpdateACTTables_003Eb__14_82(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_83(CombatantData Data, string ExtraFormat)
		{
			return Data.GetColumnByName("CritDirectHitCount");
		}

		internal string _003CUpdateACTTables_003Eb__14_84(AttackType Data)
		{
			return ((double)Data.CritDirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_85(AttackType Data)
		{
			return ((double)Data.CritDirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_86(AttackType Left, AttackType Right)
		{
			return (Left.CritDirectHitCount() * 100 / OneOrInt(Left.Items.Count)).CompareTo(Right.CritDirectHitCount() * 100 / OneOrInt(Right.Items.Count));
		}

		internal string _003CUpdateACTTables_003Eb__14_87(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0.0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_88(DamageTypeData Data)
		{
			if (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
			{
				return 0.ToString("0.0'%", CultureInfo.InvariantCulture);
			}
			return Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_89(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct");
		}

		internal string _003CUpdateACTTables_003Eb__14_90(CombatantData Data)
		{
			return Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct");
		}

		internal int _003CUpdateACTTables_003Eb__14_91(CombatantData Left, CombatantData Right)
		{
			return string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct"), StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_92(CombatantData Data, string ExtraFormat)
		{
			return ((Data.GetColumnByName("CritDirectHitPct") == "") ? 0.0 : Convert.ToDouble(Data.GetColumnByName("CritDirectHitPct").Replace("%", ""), CultureInfo.InvariantCulture)).ToString("0'%", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_93(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("potency"))
			{
				return "0";
			}
			return ((double)Data.Tags["potency"]).ToString("0.00", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_94(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("potency"))
			{
				return "0";
			}
			return ((double)Data.Tags["potency"]).ToString("0.00", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_95(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("potency") ? Left.Tags["potency"].ToString() : "0", Right.Tags.ContainsKey("potency") ? Right.Tags["potency"].ToString() : "0", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_96(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("StatusEffects"))
			{
				return "";
			}
			return Data.Tags["StatusEffects"]?.ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_97(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("StatusEffects"))
			{
				return "";
			}
			return Data.Tags["StatusEffects"]?.ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_98(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("StatusEffects") ? Left.Tags["StatusEffects"].ToString() : "", Right.Tags.ContainsKey("StatusEffects") ? Right.Tags["StatusEffects"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_99(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("dotbase"))
			{
				return "";
			}
			return ((uint)Data.Tags["dotbase"]).ToString("0", CultureInfo.InvariantCulture);
		}

		internal string _003CUpdateACTTables_003Eb__14_100(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("dotbase"))
			{
				return "";
			}
			return ((uint)Data.Tags["dotbase"]).ToString("0", CultureInfo.InvariantCulture);
		}

		internal int _003CUpdateACTTables_003Eb__14_101(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("dotbase") ? Left.Tags["dotbase"].ToString() : "0", Right.Tags.ContainsKey("dotbase") ? Right.Tags["dotbase"].ToString() : "0", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_102(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("BuffByte1"))
			{
				return "";
			}
			return Data.Tags["BuffByte1"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_103(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("BuffByte1"))
			{
				return "";
			}
			return Data.Tags["BuffByte1"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_104(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("BuffByte1") ? Left.Tags["BuffByte1"].ToString() : "", Right.Tags.ContainsKey("BuffByte1") ? Right.Tags["BuffByte1"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_105(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("BuffByte2"))
			{
				return "";
			}
			return Data.Tags["BuffByte2"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_106(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("BuffByte2"))
			{
				return "";
			}
			return Data.Tags["BuffByte2"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_107(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("BuffByte2") ? Left.Tags["BuffByte2"].ToString() : "", Right.Tags.ContainsKey("BuffByte2") ? Right.Tags["BuffByte2"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_108(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("BuffByte3"))
			{
				return "";
			}
			return Data.Tags["BuffByte3"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_109(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("BuffByte3"))
			{
				return "";
			}
			return Data.Tags["BuffByte3"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_110(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("BuffByte3") ? Left.Tags["BuffByte3"].ToString() : "", Right.Tags.ContainsKey("BuffByte3") ? Right.Tags["BuffByte3"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_111(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("CritRate"))
			{
				return "";
			}
			return Data.Tags["CritRate"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_112(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("CritRate"))
			{
				return "";
			}
			return Data.Tags["CritRate"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_113(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("CritRate") ? Left.Tags["CritRate"].ToString() : "0", Right.Tags.ContainsKey("CritRate") ? Right.Tags["CritRate"].ToString() : "0", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_114(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("CritEffects"))
			{
				return "";
			}
			return Data.Tags["CritEffects"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_115(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("CritEffects"))
			{
				return "";
			}
			return Data.Tags["CritEffects"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_116(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("CritEffects") ? Left.Tags["CritEffects"].ToString() : "", Right.Tags.ContainsKey("CritEffects") ? Right.Tags["CritEffects"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_117(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("DHRate"))
			{
				return "";
			}
			return Data.Tags["DHRate"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_118(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("DHRate"))
			{
				return "";
			}
			return Data.Tags["DHRate"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_119(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("DHRate") ? Left.Tags["DHRate"].ToString() : "", Right.Tags.ContainsKey("DHRate") ? Right.Tags["DHRate"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal string _003CUpdateACTTables_003Eb__14_120(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("DHEffects"))
			{
				return "";
			}
			return Data.Tags["DHEffects"].ToString();
		}

		internal string _003CUpdateACTTables_003Eb__14_121(MasterSwing Data)
		{
			if (!Data.Tags.ContainsKey("DHEffects"))
			{
				return "";
			}
			return Data.Tags["DHEffects"].ToString();
		}

		internal int _003CUpdateACTTables_003Eb__14_122(MasterSwing Left, MasterSwing Right)
		{
			return string.Compare(Left.Tags.ContainsKey("DHEffects") ? Left.Tags["DHEffects"].ToString() : "", Right.Tags.ContainsKey("DHEffects") ? Right.Tags["DHEffects"].ToString() : "", StringComparison.OrdinalIgnoreCase);
		}

		internal bool _003CGenAttackTypeGraph_003Eb__17_0(MasterSwing x)
		{
			if (x.Tags.ContainsKey("potency") && float.TryParse(x.Tags["potency"].ToString(), out var result))
			{
				return result > 0f;
			}
			return false;
		}
	}

	private Button cmdClear;

	private TabPage _pluginScreenSpace;

	private SettingsPropertyPage _settingsPropertyPage;

	private readonly ParseMediator _parseMediator;

	private readonly IActWrapper _actWrapper;

	private readonly IBenchmarkRepository _benchmarkRepository;

	private DateTimeLogParser _oldDateTimeDelegate;

	public ACT_UIMods(ParseMediator parseMediator, SettingsPropertyPage settingsPropertyPage, IActWrapper actWrapper, IBenchmarkRepository benchmarkRepository)
	{
		_actWrapper = actWrapper;
		_parseMediator = parseMediator;
		_settingsPropertyPage = settingsPropertyPage;
		_benchmarkRepository = benchmarkRepository;
	}

	public void ConfigureUI(TabPage pluginScreenSpace)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		_pluginScreenSpace = pluginScreenSpace;
		cmdClear = ((Control)(object)ActGlobals.oFormActMain).FindControl<Button>("tc1\\tpMain\\pLeftView\\pTvBtns\\tableLayoutPanel11\\btnClear");
		if (cmdClear != null)
		{
			((Control)cmdClear).Click += _parseMediator.OnClear;
		}
		_actWrapper.TimeStampLen = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture).Length + 3;
		_actWrapper.LogPathHasCharName = false;
		_actWrapper.ZoneChangeRegex = new Regex("^01[|][^|]*[|][^|]*[|](?<zone>.+)[|].*$", RegexOptions.Compiled);
		_oldDateTimeDelegate = ActGlobals.oFormActMain.GetDateTimeFromLog;
		ActGlobals.oFormActMain.GetDateTimeFromLog = new DateTimeLogParser(_parseMediator.ParseLogDateTime);
		ActGlobals.oFormActMain.BeforeLogLineRead += new LogLineEventDelegate(OFormActMain_BeforeLogLineRead);
		ActGlobals.oFormActMain.LogFileChanged += new LogFileChangedDelegate(OFormActMain_LogFileChanged);
		LoadPluginSettingsControl();
	}

	private void OFormActMain_LogFileChanged(bool IsImport, string NewLogFileName)
	{
		_benchmarkRepository.Measure("ParseLogFileChanged", (Action)delegate
		{
			_parseMediator.LogFileChanged(IsImport);
		});
	}

	private void OFormActMain_BeforeLogLineRead(bool isImport, LogLineEventArgs logInfo)
	{
		(logInfo.logLine, logInfo.detectedType) = _benchmarkRepository.Measure<(string, int)>("ParseBeforeLogLineRead", (Func<(string, int)>)(() => ((string, int))_parseMediator.BeforeLogLineRead(isImport, logInfo.detectedTime, logInfo.logLine)));
	}

	private void LoadPluginSettingsControl()
	{
		((Control)_settingsPropertyPage).Dock = (DockStyle)5;
		((Control)_pluginScreenSpace).Controls.Add((Control)(object)_settingsPropertyPage);
		((Control)_pluginScreenSpace).Text = "FFXIV ACT Plugin";
	}

	public void LoadACTSettings()
	{
		SettingsSerializer xmlSettings = _settingsPropertyPage.InitializeSettingsSerializer();
		_settingsPropertyPage.LoadSettings(xmlSettings);
	}

	public void UnloadControls()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		ActGlobals.oFormActMain.BeforeLogLineRead -= new LogLineEventDelegate(OFormActMain_BeforeLogLineRead);
		ActGlobals.oFormActMain.GetDateTimeFromLog = _oldDateTimeDelegate;
		_settingsPropertyPage?.SaveSettings();
		_settingsPropertyPage = null;
		if (cmdClear != null)
		{
			((Control)cmdClear).Click -= _parseMediator.OnClear;
			cmdClear = null;
		}
	}

	public static void UpdateACTTables(bool showDebug)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Expected O, but got Unknown
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Expected O, but got Unknown
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Expected O, but got Unknown
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Expected O, but got Unknown
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Expected O, but got Unknown
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Expected O, but got Unknown
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Expected O, but got Unknown
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Expected O, but got Unknown
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Expected O, but got Unknown
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Expected O, but got Unknown
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Expected O, but got Unknown
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Expected O, but got Unknown
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Expected O, but got Unknown
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Expected O, but got Unknown
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Expected O, but got Unknown
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_0845: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Expected O, but got Unknown
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Expected O, but got Unknown
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Expected O, but got Unknown
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Expected O, but got Unknown
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Expected O, but got Unknown
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ff: Expected O, but got Unknown
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Expected O, but got Unknown
		//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c8: Expected O, but got Unknown
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Expected O, but got Unknown
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Expected O, but got Unknown
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Expected O, but got Unknown
		//IL_09dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Expected O, but got Unknown
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Expected O, but got Unknown
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Expected O, but got Unknown
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Expected O, but got Unknown
		//IL_0b36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b41: Expected O, but got Unknown
		//IL_0ac4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Expected O, but got Unknown
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Expected O, but got Unknown
		//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Expected O, but got Unknown
		//IL_0c29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c33: Expected O, but got Unknown
		//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c29: Expected O, but got Unknown
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Expected O, but got Unknown
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Expected O, but got Unknown
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be0: Expected O, but got Unknown
		//IL_0ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf0: Expected O, but got Unknown
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Expected O, but got Unknown
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Expected O, but got Unknown
		//IL_0d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Expected O, but got Unknown
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Expected O, but got Unknown
		//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0f: Expected O, but got Unknown
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Expected O, but got Unknown
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d85: Expected O, but got Unknown
		//IL_0e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e90: Expected O, but got Unknown
		//IL_0e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Expected O, but got Unknown
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Expected O, but got Unknown
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f25: Expected O, but got Unknown
		//IL_0ea4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaf: Expected O, but got Unknown
		//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e43: Expected O, but got Unknown
		//IL_0faf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Expected O, but got Unknown
		//IL_0f39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f44: Expected O, but got Unknown
		//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed8: Expected O, but got Unknown
		//IL_104b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1055: Expected O, but got Unknown
		//IL_1040: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_104b: Expected O, but got Unknown
		//IL_0fce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd9: Expected O, but got Unknown
		//IL_0f63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6d: Expected O, but got Unknown
		//IL_1094: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_109f: Expected O, but got Unknown
		//IL_0ff8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1002: Expected O, but got Unknown
		//IL_110a: Unknown result type (might be due to invalid IL or missing references)
		//IL_110f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Expected O, but got Unknown
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c8: Expected O, but got Unknown
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10be: Expected O, but got Unknown
		//IL_119f: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11aa: Expected O, but got Unknown
		//IL_1129: Unknown result type (might be due to invalid IL or missing references)
		//IL_112e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1134: Expected O, but got Unknown
		//IL_1234: Unknown result type (might be due to invalid IL or missing references)
		//IL_1239: Unknown result type (might be due to invalid IL or missing references)
		//IL_123f: Expected O, but got Unknown
		//IL_11be: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Expected O, but got Unknown
		//IL_1153: Unknown result type (might be due to invalid IL or missing references)
		//IL_115d: Expected O, but got Unknown
		//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Expected O, but got Unknown
		//IL_1253: Unknown result type (might be due to invalid IL or missing references)
		//IL_1258: Unknown result type (might be due to invalid IL or missing references)
		//IL_125e: Expected O, but got Unknown
		//IL_11e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Expected O, but got Unknown
		//IL_135b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1360: Unknown result type (might be due to invalid IL or missing references)
		//IL_1366: Expected O, but got Unknown
		//IL_12e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f3: Expected O, but got Unknown
		//IL_127d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Expected O, but got Unknown
		//IL_13d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13dc: Expected O, but got Unknown
		//IL_1385: Unknown result type (might be due to invalid IL or missing references)
		//IL_138f: Expected O, but got Unknown
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_137f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1385: Expected O, but got Unknown
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_131c: Expected O, but got Unknown
		//IL_146d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1477: Expected O, but got Unknown
		//IL_1462: Unknown result type (might be due to invalid IL or missing references)
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_146d: Expected O, but got Unknown
		//IL_13f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fb: Expected O, but got Unknown
		//IL_14b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14be: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c4: Expected O, but got Unknown
		//IL_141a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1424: Expected O, but got Unknown
		//IL_154b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1550: Unknown result type (might be due to invalid IL or missing references)
		//IL_1556: Expected O, but got Unknown
		//IL_14d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e3: Expected O, but got Unknown
		//IL_15c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cc: Expected O, but got Unknown
		//IL_1575: Unknown result type (might be due to invalid IL or missing references)
		//IL_157f: Expected O, but got Unknown
		//IL_156a: Unknown result type (might be due to invalid IL or missing references)
		//IL_156f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1575: Expected O, but got Unknown
		//IL_1502: Unknown result type (might be due to invalid IL or missing references)
		//IL_150c: Expected O, but got Unknown
		//IL_165d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1667: Expected O, but got Unknown
		//IL_1652: Unknown result type (might be due to invalid IL or missing references)
		//IL_1657: Unknown result type (might be due to invalid IL or missing references)
		//IL_165d: Expected O, but got Unknown
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15eb: Expected O, but got Unknown
		//IL_16a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b4: Expected O, but got Unknown
		//IL_160a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1614: Expected O, but got Unknown
		//IL_173b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1740: Unknown result type (might be due to invalid IL or missing references)
		//IL_1746: Expected O, but got Unknown
		//IL_16c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d3: Expected O, but got Unknown
		//IL_17b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bc: Expected O, but got Unknown
		//IL_1765: Unknown result type (might be due to invalid IL or missing references)
		//IL_176f: Expected O, but got Unknown
		//IL_175a: Unknown result type (might be due to invalid IL or missing references)
		//IL_175f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1765: Expected O, but got Unknown
		//IL_16f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fc: Expected O, but got Unknown
		//IL_184d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1857: Expected O, but got Unknown
		//IL_1842: Unknown result type (might be due to invalid IL or missing references)
		//IL_1847: Unknown result type (might be due to invalid IL or missing references)
		//IL_184d: Expected O, but got Unknown
		//IL_17d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17db: Expected O, but got Unknown
		//IL_1899: Unknown result type (might be due to invalid IL or missing references)
		//IL_189e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a4: Expected O, but got Unknown
		//IL_17fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1804: Expected O, but got Unknown
		//IL_192b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1930: Unknown result type (might be due to invalid IL or missing references)
		//IL_1936: Expected O, but got Unknown
		//IL_18b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c3: Expected O, but got Unknown
		//IL_19a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ac: Expected O, but got Unknown
		//IL_1955: Unknown result type (might be due to invalid IL or missing references)
		//IL_195f: Expected O, but got Unknown
		//IL_194a: Unknown result type (might be due to invalid IL or missing references)
		//IL_194f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1955: Expected O, but got Unknown
		//IL_18e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ec: Expected O, but got Unknown
		//IL_1a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a47: Expected O, but got Unknown
		//IL_1a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3d: Expected O, but got Unknown
		//IL_19c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cb: Expected O, but got Unknown
		//IL_19ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f4: Expected O, but got Unknown
		//IL_1a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Expected O, but got Unknown
		//IL_1b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2f: Expected O, but got Unknown
		//IL_1aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab9: Expected O, but got Unknown
		//IL_1bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc4: Expected O, but got Unknown
		//IL_1b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4e: Expected O, but got Unknown
		//IL_1ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae2: Expected O, but got Unknown
		//IL_1c4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c59: Expected O, but got Unknown
		//IL_1bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be3: Expected O, but got Unknown
		//IL_1b6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b77: Expected O, but got Unknown
		//IL_1ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cee: Expected O, but got Unknown
		//IL_1c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c78: Expected O, but got Unknown
		//IL_1c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0c: Expected O, but got Unknown
		//IL_1d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d83: Expected O, but got Unknown
		//IL_1d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0d: Expected O, but got Unknown
		//IL_1c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca1: Expected O, but got Unknown
		//IL_1e0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e18: Expected O, but got Unknown
		//IL_1d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da2: Expected O, but got Unknown
		//IL_1d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d36: Expected O, but got Unknown
		//IL_1ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ead: Expected O, but got Unknown
		//IL_1e2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e37: Expected O, but got Unknown
		//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcb: Expected O, but got Unknown
		//IL_1f37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f42: Expected O, but got Unknown
		//IL_1ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecc: Expected O, but got Unknown
		//IL_1e56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e60: Expected O, but got Unknown
		//IL_1fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd7: Expected O, but got Unknown
		//IL_1f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f61: Expected O, but got Unknown
		//IL_1eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef5: Expected O, but got Unknown
		//IL_1feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff6: Expected O, but got Unknown
		//IL_1f80: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8a: Expected O, but got Unknown
		//IL_2015: Unknown result type (might be due to invalid IL or missing references)
		//IL_201f: Expected O, but got Unknown
		CombatantData.IncomingDamageTypeDataObjects = new Dictionary<string, DamageTypeDef>
		{
			{
				"Simulated DoTs (Inc)",
				new DamageTypeDef("Simulated DoTs (Inc)", -1, Color.Red)
			},
			{
				"Incoming Damage",
				new DamageTypeDef("Incoming Damage", -1, Color.Red)
			},
			{
				"Damage Shields (Inc)",
				new DamageTypeDef("Damage Shields (Inc)", 1, Color.YellowGreen)
			},
			{
				"Simulated HoTs (Inc)",
				new DamageTypeDef("Simulated HoTs (Inc)", 1, Color.GreenYellow)
			},
			{
				"Healed (Inc)",
				new DamageTypeDef("Healed (Inc)", 1, Color.LimeGreen)
			},
			{
				"Other (Inc)",
				new DamageTypeDef("Other (Inc)", 0, Color.Lime)
			},
			{
				"Status (Inc)",
				new DamageTypeDef("Status (Inc)", 0, Color.Wheat)
			},
			{
				"Power Drain (Inc)",
				new DamageTypeDef("Power Drain (Inc)", -1, Color.Magenta)
			},
			{
				"Power Replenish (Inc)",
				new DamageTypeDef("Power Replenish (Inc)", 1, Color.MediumPurple)
			},
			{
				"Cure/Dispel (Inc)",
				new DamageTypeDef("Cure/Dispel (Inc)", 0, Color.Wheat)
			},
			{
				"Threat (Inc)",
				new DamageTypeDef("Threat (Inc)", -1, Color.Yellow)
			},
			{
				"All Incoming (Ref)",
				new DamageTypeDef("All Incoming (Ref)", 0, Color.Black)
			}
		};
		CombatantData.OutgoingDamageTypeDataObjects = new Dictionary<string, DamageTypeDef>
		{
			{
				"Auto-Attack (Out)",
				new DamageTypeDef("Auto-Attack (Out)", -1, Color.DarkGoldenrod)
			},
			{
				"Skill/Ability (Out)",
				new DamageTypeDef("Skill/Ability (Out)", -1, Color.DarkOrange)
			},
			{
				"Simulated DoTs (Out)",
				new DamageTypeDef("Simulated DoTs (Out)", -1, Color.OrangeRed)
			},
			{
				"Outgoing Damage",
				new DamageTypeDef("Outgoing Damage", 0, Color.Orange)
			},
			{
				"Damage Shields (Out)",
				new DamageTypeDef("Damage Shields (Out)", 1, Color.LightSkyBlue)
			},
			{
				"Simulated HoTs (Out)",
				new DamageTypeDef("Simulated HoTs (Out)", 1, Color.LightBlue)
			},
			{
				"Healed (Out)",
				new DamageTypeDef("Healed (Out)", 1, Color.Blue)
			},
			{
				"Other (Out)",
				new DamageTypeDef("Other (Out)", 0, Color.Lime)
			},
			{
				"Status (Out)",
				new DamageTypeDef("Status (Out)", 0, Color.Wheat)
			},
			{
				"Power Drain (Out)",
				new DamageTypeDef("Power Drain (Out)", -1, Color.Purple)
			},
			{
				"Power Replenish (Out)",
				new DamageTypeDef("Power Replenish (Out)", 1, Color.Violet)
			},
			{
				"Cure/Dispel (Out)",
				new DamageTypeDef("Cure/Dispel (Out)", 0, Color.Wheat)
			},
			{
				"Threat (Out)",
				new DamageTypeDef("Threat (Out)", -1, Color.Yellow)
			},
			{
				"All Outgoing (Ref)",
				new DamageTypeDef("All Outgoing (Ref)", 0, Color.Black)
			}
		};
		CombatantData.SwingTypeToDamageTypeDataLinksOutgoing = new SortedDictionary<int, List<string>>
		{
			{
				0,
				new List<string> { "Auto-Attack (Out)", "Outgoing Damage" }
			},
			{
				1,
				new List<string> { "Other (Out)" }
			},
			{
				2,
				new List<string> { "Skill/Ability (Out)", "Outgoing Damage" }
			},
			{
				3,
				new List<string> { "Simulated DoTs (Out)", "Outgoing Damage" }
			},
			{
				4,
				new List<string> { "Healed (Out)" }
			},
			{
				5,
				new List<string> { "Simulated HoTs (Out)", "Healed (Out)" }
			},
			{
				6,
				new List<string> { "Power Drain (Out)" }
			},
			{
				7,
				new List<string> { "Power Replenish (Out)" }
			},
			{
				8,
				new List<string> { "Status (Out)" }
			},
			{
				9,
				new List<string> { "Cure/Dispel (Out)" }
			},
			{
				10,
				new List<string> { "Threat (Out)" }
			},
			{
				11,
				new List<string> { "Damage Shields (Out)", "Healed (Out)" }
			}
		};
		CombatantData.SwingTypeToDamageTypeDataLinksIncoming = new SortedDictionary<int, List<string>>
		{
			{
				0,
				new List<string> { "Incoming Damage" }
			},
			{
				1,
				new List<string> { "Other (Inc)" }
			},
			{
				2,
				new List<string> { "Incoming Damage" }
			},
			{
				3,
				new List<string> { "Simulated DoTs (Inc)", "Incoming Damage" }
			},
			{
				4,
				new List<string> { "Healed (Inc)" }
			},
			{
				5,
				new List<string> { "Simulated HoTs (Inc)", "Healed (Inc)" }
			},
			{
				6,
				new List<string> { "Power Drain (Inc)" }
			},
			{
				7,
				new List<string> { "Power Replenish (Inc)" }
			},
			{
				8,
				new List<string> { "Status (Inc)" }
			},
			{
				9,
				new List<string> { "Cure/Dispel (Inc)" }
			},
			{
				10,
				new List<string> { "Threat (Inc)" }
			},
			{
				11,
				new List<string> { "Damage Shields (Inc)", "Healed (Inc)" }
			}
		};
		CombatantData.DamageSwingTypes = new List<int> { 0, 2, 3 };
		CombatantData.HealingSwingTypes = new List<int> { 4, 5, 8, 9, 1 };
		if (!EncounterData.ExportVariables.ContainsKey("CurrentZoneName"))
		{
			Dictionary<string, TextExportFormatter> exportVariables = EncounterData.ExportVariables;
			object obj = _003C_003Ec._003C_003E9__14_0;
			if (obj == null)
			{
				ExportStringDataCallback val = (EncounterData data, List<CombatantData> SelectiveAllies, string extra) => data.ZoneName;
				_003C_003Ec._003C_003E9__14_0 = val;
				obj = (object)val;
			}
			exportVariables.Add("CurrentZoneName", new TextExportFormatter("CurrentZoneName", "CurrentZoneName", "Current Zone Name", (ExportStringDataCallback)obj));
		}
		if (!CombatantData.ExportVariables.ContainsKey("Last10DPS"))
		{
			Dictionary<string, TextExportFormatter> exportVariables2 = CombatantData.ExportVariables;
			object obj2 = _003C_003Ec._003C_003E9__14_1;
			if (obj2 == null)
			{
				ExportStringDataCallback val2 = (CombatantData Data, string ExtraFormat) => Data.LastNDPS(10).ToString("0", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_1 = val2;
				obj2 = (object)val2;
			}
			exportVariables2.Add("Last10DPS", new TextExportFormatter("Last10DPS", "Last 10 Seconds DPS", "Average DPS for last 10 seconds.", (ExportStringDataCallback)obj2));
		}
		if (!CombatantData.ExportVariables.ContainsKey("Last30DPS"))
		{
			Dictionary<string, TextExportFormatter> exportVariables3 = CombatantData.ExportVariables;
			object obj3 = _003C_003Ec._003C_003E9__14_2;
			if (obj3 == null)
			{
				ExportStringDataCallback val3 = (CombatantData Data, string ExtraFormat) => Data.LastNDPS(30).ToString("0", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_2 = val3;
				obj3 = (object)val3;
			}
			exportVariables3.Add("Last30DPS", new TextExportFormatter("Last30DPS", "Last 30 Seconds DPS", "Average DPS for last 30 seconds.", (ExportStringDataCallback)obj3));
		}
		if (!CombatantData.ExportVariables.ContainsKey("Last60DPS"))
		{
			Dictionary<string, TextExportFormatter> exportVariables4 = CombatantData.ExportVariables;
			object obj4 = _003C_003Ec._003C_003E9__14_3;
			if (obj4 == null)
			{
				ExportStringDataCallback val4 = (CombatantData Data, string ExtraFormat) => Data.LastNDPS(60).ToString("0", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_3 = val4;
				obj4 = (object)val4;
			}
			exportVariables4.Add("Last60DPS", new TextExportFormatter("Last60DPS", "Last 60 Seconds DPS", "Average DPS for last 60 seconds.", (ExportStringDataCallback)obj4));
		}
		if (!EncounterData.ExportVariables.ContainsKey("Last10DPS"))
		{
			Dictionary<string, TextExportFormatter> exportVariables5 = EncounterData.ExportVariables;
			object obj5 = _003C_003Ec._003C_003E9__14_4;
			if (obj5 == null)
			{
				ExportStringDataCallback val5 = (EncounterData Data, List<CombatantData> SelectiveAllies, string Extra) => Data.LastNDPS(SelectiveAllies, 10).ToString("0", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_4 = val5;
				obj5 = (object)val5;
			}
			exportVariables5.Add("Last10DPS", new TextExportFormatter("Last10DPS", "Last 10 Seconds DPS", "Average DPS for last 10 seconds", (ExportStringDataCallback)obj5));
		}
		if (!EncounterData.ExportVariables.ContainsKey("Last30DPS"))
		{
			Dictionary<string, TextExportFormatter> exportVariables6 = EncounterData.ExportVariables;
			object obj6 = _003C_003Ec._003C_003E9__14_5;
			if (obj6 == null)
			{
				ExportStringDataCallback val6 = (EncounterData Data, List<CombatantData> SelectiveAllies, string Extra) => Data.LastNDPS(SelectiveAllies, 30).ToString("0", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_5 = val6;
				obj6 = (object)val6;
			}
			exportVariables6.Add("Last30DPS", new TextExportFormatter("Last30DPS", "Last 30 Seconds DPS", "Average DPS for last 30 seconds", (ExportStringDataCallback)obj6));
		}
		if (!EncounterData.ExportVariables.ContainsKey("Last60DPS"))
		{
			Dictionary<string, TextExportFormatter> exportVariables7 = EncounterData.ExportVariables;
			object obj7 = _003C_003Ec._003C_003E9__14_6;
			if (obj7 == null)
			{
				ExportStringDataCallback val7 = (EncounterData Data, List<CombatantData> SelectiveAllies, string Extra) => Data.LastNDPS(SelectiveAllies, 60).ToString("0", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_6 = val7;
				obj7 = (object)val7;
			}
			exportVariables7.Add("Last60DPS", new TextExportFormatter("Last60DPS", "Last 60 Seconds DPS", "Average DPS for last 60 seconds", (ExportStringDataCallback)obj7));
		}
		if (!MasterSwing.ColumnDefs.ContainsKey("StatusDuration"))
		{
			Dictionary<string, ColumnDef> columnDefs = MasterSwing.ColumnDefs;
			object obj8 = _003C_003Ec._003C_003E9__14_7;
			if (obj8 == null)
			{
				StringDataCallback val8 = (MasterSwing Data) => (!Data.Tags.ContainsKey("StatusDuration")) ? "" : ((double)Data.Tags["StatusDuration"]).ToString("0.#", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_7 = val8;
				obj8 = (object)val8;
			}
			object obj9 = _003C_003Ec._003C_003E9__14_8;
			if (obj9 == null)
			{
				StringDataCallback val9 = (MasterSwing Data) => (!Data.Tags.ContainsKey("StatusDuration")) ? "" : ((double)Data.Tags["StatusDuration"]).ToString("0.#", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_8 = val9;
				obj9 = (object)val9;
			}
			columnDefs.Add("StatusDuration", new ColumnDef("StatusDuration", true, "VARCHAR(8)", "StatusDuration", (StringDataCallback)obj8, (StringDataCallback)obj9, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => (Left.Tags.ContainsKey("StatusDuration") ? ((double)Left.Tags["StatusDuration"]) : 0.0).CompareTo(Right.Tags.ContainsKey("StatusDuration") ? ((double)Right.Tags["StatusDuration"]) : 0.0))));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("Job"))
		{
			Dictionary<string, ColumnDef> columnDefs2 = CombatantData.ColumnDefs;
			object obj10 = _003C_003Ec._003C_003E9__14_10;
			if (obj10 == null)
			{
				StringDataCallback val10 = (CombatantData Data) => Data.Job();
				_003C_003Ec._003C_003E9__14_10 = val10;
				obj10 = (object)val10;
			}
			object obj11 = _003C_003Ec._003C_003E9__14_11;
			if (obj11 == null)
			{
				StringDataCallback val11 = (CombatantData Data) => Data.Job();
				_003C_003Ec._003C_003E9__14_11 = val11;
				obj11 = (object)val11;
			}
			columnDefs2.Add("Job", new ColumnDef("Job", true, "VARCHAR(8)", "Job", (StringDataCallback)obj10, (StringDataCallback)obj11, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Job(), Right.Job(), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("Job"))
		{
			Dictionary<string, TextExportFormatter> exportVariables8 = CombatantData.ExportVariables;
			object obj12 = _003C_003Ec._003C_003E9__14_13;
			if (obj12 == null)
			{
				ExportStringDataCallback val12 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("Job");
				_003C_003Ec._003C_003E9__14_13 = val12;
				obj12 = (object)val12;
			}
			exportVariables8.Add("Job", new TextExportFormatter("Job", "Job Name", "Player's Job", (ExportStringDataCallback)obj12));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("ParryPct"))
		{
			Dictionary<string, ColumnDef> columnDefs3 = CombatantData.ColumnDefs;
			object obj13 = _003C_003Ec._003C_003E9__14_14;
			if (obj13 == null)
			{
				StringDataCallback val13 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct");
				_003C_003Ec._003C_003E9__14_14 = val13;
				obj13 = (object)val13;
			}
			object obj14 = _003C_003Ec._003C_003E9__14_15;
			if (obj14 == null)
			{
				StringDataCallback val14 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct");
				_003C_003Ec._003C_003E9__14_15 = val14;
				obj14 = (object)val14;
			}
			columnDefs3.Add("ParryPct", new ColumnDef("ParryPct", false, "VARCHAR(8)", "ParryPct", (StringDataCallback)obj13, (StringDataCallback)obj14, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct"), Right.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ParryPct"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("ParryPct"))
		{
			Dictionary<string, TextExportFormatter> exportVariables9 = CombatantData.ExportVariables;
			object obj15 = _003C_003Ec._003C_003E9__14_17;
			if (obj15 == null)
			{
				ExportStringDataCallback val15 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("ParryPct");
				_003C_003Ec._003C_003E9__14_17 = val15;
				obj15 = (object)val15;
			}
			exportVariables9.Add("ParryPct", new TextExportFormatter("ParryPct", "Parry Percent", "Percent of hits that were parried.", (ExportStringDataCallback)obj15));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("BlockPct"))
		{
			Dictionary<string, ColumnDef> columnDefs4 = CombatantData.ColumnDefs;
			object obj16 = _003C_003Ec._003C_003E9__14_18;
			if (obj16 == null)
			{
				StringDataCallback val16 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct");
				_003C_003Ec._003C_003E9__14_18 = val16;
				obj16 = (object)val16;
			}
			object obj17 = _003C_003Ec._003C_003E9__14_19;
			if (obj17 == null)
			{
				StringDataCallback val17 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct");
				_003C_003Ec._003C_003E9__14_19 = val17;
				obj17 = (object)val17;
			}
			columnDefs4.Add("BlockPct", new ColumnDef("BlockPct", false, "VARCHAR(8)", "BlockPct", (StringDataCallback)obj16, (StringDataCallback)obj17, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct"), Right.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("BlockPct"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("BlockPct"))
		{
			Dictionary<string, TextExportFormatter> exportVariables10 = CombatantData.ExportVariables;
			object obj18 = _003C_003Ec._003C_003E9__14_21;
			if (obj18 == null)
			{
				ExportStringDataCallback val18 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("BlockPct");
				_003C_003Ec._003C_003E9__14_21 = val18;
				obj18 = (object)val18;
			}
			exportVariables10.Add("BlockPct", new TextExportFormatter("BlockPct", "Block Percent", "Percent of hits that were blocked.", (ExportStringDataCallback)obj18));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("IncToHit"))
		{
			Dictionary<string, ColumnDef> columnDefs5 = CombatantData.ColumnDefs;
			object obj19 = _003C_003Ec._003C_003E9__14_22;
			if (obj19 == null)
			{
				StringDataCallback val19 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit");
				_003C_003Ec._003C_003E9__14_22 = val19;
				obj19 = (object)val19;
			}
			object obj20 = _003C_003Ec._003C_003E9__14_23;
			if (obj20 == null)
			{
				StringDataCallback val20 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit");
				_003C_003Ec._003C_003E9__14_23 = val20;
				obj20 = (object)val20;
			}
			columnDefs5.Add("IncToHit", new ColumnDef("IncToHit", false, "VARCHAR(8)", "IncToHit", (StringDataCallback)obj19, (StringDataCallback)obj20, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit"), Right.Items[CombatantData.DamageTypeDataIncomingDamage].GetColumnByName("ToHit"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("IncToHit"))
		{
			Dictionary<string, TextExportFormatter> exportVariables11 = CombatantData.ExportVariables;
			object obj21 = _003C_003Ec._003C_003E9__14_25;
			if (obj21 == null)
			{
				ExportStringDataCallback val21 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("IncToHit");
				_003C_003Ec._003C_003E9__14_25 = val21;
				obj21 = (object)val21;
			}
			exportVariables11.Add("IncToHit", new TextExportFormatter("IncToHit", "Incoming Hit Rate", "Incoming hits to the target.", (ExportStringDataCallback)obj21));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("ParryPct"))
		{
			Dictionary<string, ColumnDef> columnDefs6 = DamageTypeData.ColumnDefs;
			object obj22 = _003C_003Ec._003C_003E9__14_26;
			if (obj22 == null)
			{
				StringDataCallback val22 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("ParryPct");
				_003C_003Ec._003C_003E9__14_26 = val22;
				obj22 = (object)val22;
			}
			object obj23 = _003C_003Ec._003C_003E9__14_27;
			if (obj23 == null)
			{
				StringDataCallback val23 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("ParryPct");
				_003C_003Ec._003C_003E9__14_27 = val23;
				obj23 = (object)val23;
			}
			columnDefs6.Add("ParryPct", new ColumnDef("ParryPct", false, "VARCHAR(8)", "ParryPct", (StringDataCallback)obj22, (StringDataCallback)obj23));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("BlockPct"))
		{
			Dictionary<string, ColumnDef> columnDefs7 = DamageTypeData.ColumnDefs;
			object obj24 = _003C_003Ec._003C_003E9__14_28;
			if (obj24 == null)
			{
				StringDataCallback val24 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("BlockPct");
				_003C_003Ec._003C_003E9__14_28 = val24;
				obj24 = (object)val24;
			}
			object obj25 = _003C_003Ec._003C_003E9__14_29;
			if (obj25 == null)
			{
				StringDataCallback val25 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("BlockPct");
				_003C_003Ec._003C_003E9__14_29 = val25;
				obj25 = (object)val25;
			}
			columnDefs7.Add("BlockPct", new ColumnDef("BlockPct", false, "VARCHAR(8)", "BlockPct", (StringDataCallback)obj24, (StringDataCallback)obj25));
		}
		if (!AttackType.ColumnDefs.ContainsKey("Parry"))
		{
			Dictionary<string, ColumnDef> columnDefs8 = AttackType.ColumnDefs;
			object obj26 = _003C_003Ec._003C_003E9__14_30;
			if (obj26 == null)
			{
				StringDataCallback val26 = (AttackType Data) => Data.Parry().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_30 = val26;
				obj26 = (object)val26;
			}
			object obj27 = _003C_003Ec._003C_003E9__14_31;
			if (obj27 == null)
			{
				StringDataCallback val27 = (AttackType Data) => Data.Parry().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_31 = val27;
				obj27 = (object)val27;
			}
			columnDefs8.Add("Parry", new ColumnDef("Parry", false, "INT", "Parry", (StringDataCallback)obj26, (StringDataCallback)obj27, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.Parry().CompareTo(Right.Parry()))));
		}
		if (!AttackType.ColumnDefs.ContainsKey("ParryPct"))
		{
			Dictionary<string, ColumnDef> columnDefs9 = AttackType.ColumnDefs;
			object obj28 = _003C_003Ec._003C_003E9__14_33;
			if (obj28 == null)
			{
				StringDataCallback val28 = (AttackType Data) => ((double)Data.Parry() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_33 = val28;
				obj28 = (object)val28;
			}
			object obj29 = _003C_003Ec._003C_003E9__14_34;
			if (obj29 == null)
			{
				StringDataCallback val29 = (AttackType Data) => ((double)Data.Parry() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_34 = val29;
				obj29 = (object)val29;
			}
			columnDefs9.Add("ParryPct", new ColumnDef("ParryPct", false, "VARCHAR(8)", "ParryPct", (StringDataCallback)obj28, (StringDataCallback)obj29, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.Parry().CompareTo(Right.Parry()))));
		}
		if (!AttackType.ColumnDefs.ContainsKey("Block"))
		{
			Dictionary<string, ColumnDef> columnDefs10 = AttackType.ColumnDefs;
			object obj30 = _003C_003Ec._003C_003E9__14_36;
			if (obj30 == null)
			{
				StringDataCallback val30 = (AttackType Data) => Data.Block().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_36 = val30;
				obj30 = (object)val30;
			}
			object obj31 = _003C_003Ec._003C_003E9__14_37;
			if (obj31 == null)
			{
				StringDataCallback val31 = (AttackType Data) => Data.Block().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_37 = val31;
				obj31 = (object)val31;
			}
			columnDefs10.Add("Block", new ColumnDef("Block", false, "INT", "Block", (StringDataCallback)obj30, (StringDataCallback)obj31, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.Block().CompareTo(Right.Block()))));
		}
		if (!AttackType.ColumnDefs.ContainsKey("BlockPct"))
		{
			Dictionary<string, ColumnDef> columnDefs11 = AttackType.ColumnDefs;
			object obj32 = _003C_003Ec._003C_003E9__14_39;
			if (obj32 == null)
			{
				StringDataCallback val32 = (AttackType Data) => ((double)Data.Block() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_39 = val32;
				obj32 = (object)val32;
			}
			object obj33 = _003C_003Ec._003C_003E9__14_40;
			if (obj33 == null)
			{
				StringDataCallback val33 = (AttackType Data) => ((double)Data.Block() * 100.0 / (double)OneOrInt(Data.BlockParryCount())).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_40 = val33;
				obj33 = (object)val33;
			}
			columnDefs11.Add("BlockPct", new ColumnDef("BlockPct", false, "VARCHAR(8)", "BlockPct", (StringDataCallback)obj32, (StringDataCallback)obj33, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.Block().CompareTo(Right.Block()))));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("OverHealPct"))
		{
			Dictionary<string, ColumnDef> columnDefs12 = CombatantData.ColumnDefs;
			object obj34 = _003C_003Ec._003C_003E9__14_42;
			if (obj34 == null)
			{
				StringDataCallback val34 = (CombatantData Data) => (long.Parse(Data.Items[CombatantData.DamageTypeDataOutgoingHealing].GetColumnByName("OverHeal"), CultureInfo.InvariantCulture) * 100 / OneOrInt((!Data.Items[CombatantData.DamageTypeDataOutgoingHealing].Items.ContainsKey("All")) ? 0 : Data.DirectHeal())).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_42 = val34;
				obj34 = (object)val34;
			}
			object obj35 = _003C_003Ec._003C_003E9__14_43;
			if (obj35 == null)
			{
				StringDataCallback val35 = (CombatantData Data) => (long.Parse(Data.Items[CombatantData.DamageTypeDataOutgoingHealing].GetColumnByName("OverHeal"), CultureInfo.InvariantCulture) * 100 / OneOrInt((!Data.Items[CombatantData.DamageTypeDataOutgoingHealing].Items.ContainsKey("All")) ? 0 : Data.DirectHeal())).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_43 = val35;
				obj35 = (object)val35;
			}
			columnDefs12.Add("OverHealPct", new ColumnDef("OverHealPct", true, "VARCHAR(8)", "OverHealPct", (StringDataCallback)obj34, (StringDataCallback)obj35, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => long.Parse(Left.GetColumnByName("OverHealPct").Replace('%', ' '), CultureInfo.InvariantCulture).CompareTo(long.Parse(Right.GetColumnByName("OverHealPct").Replace('%', ' '), CultureInfo.InvariantCulture)))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("OverHealPct"))
		{
			Dictionary<string, TextExportFormatter> exportVariables12 = CombatantData.ExportVariables;
			object obj36 = _003C_003Ec._003C_003E9__14_45;
			if (obj36 == null)
			{
				ExportStringDataCallback val36 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("OverHealPct");
				_003C_003Ec._003C_003E9__14_45 = val36;
				obj36 = (object)val36;
			}
			exportVariables12.Add("OverHealPct", new TextExportFormatter("OverHealPct", "Over-Heal Percent", "Percent of heals above target's Max HP", (ExportStringDataCallback)obj36));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("OverHeal"))
		{
			Dictionary<string, ColumnDef> columnDefs13 = DamageTypeData.ColumnDefs;
			object obj37 = _003C_003Ec._003C_003E9__14_46;
			if (obj37 == null)
			{
				StringDataCallback val37 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? "0" : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("OverHeal");
				_003C_003Ec._003C_003E9__14_46 = val37;
				obj37 = (object)val37;
			}
			object obj38 = _003C_003Ec._003C_003E9__14_47;
			if (obj38 == null)
			{
				StringDataCallback val38 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? "0" : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("OverHeal");
				_003C_003Ec._003C_003E9__14_47 = val38;
				obj38 = (object)val38;
			}
			columnDefs13.Add("OverHeal", new ColumnDef("OverHeal", false, "INT", "OverHeal", (StringDataCallback)obj37, (StringDataCallback)obj38));
		}
		if (!AttackType.ColumnDefs.ContainsKey("OverHeal"))
		{
			Dictionary<string, ColumnDef> columnDefs14 = AttackType.ColumnDefs;
			object obj39 = _003C_003Ec._003C_003E9__14_48;
			if (obj39 == null)
			{
				StringDataCallback val39 = (AttackType Data) => Data.Overheal().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_48 = val39;
				obj39 = (object)val39;
			}
			object obj40 = _003C_003Ec._003C_003E9__14_49;
			if (obj40 == null)
			{
				StringDataCallback val40 = (AttackType Data) => Data.Overheal().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_49 = val40;
				obj40 = (object)val40;
			}
			columnDefs14.Add("OverHeal", new ColumnDef("OverHeal", false, "INT", "OverHeal", (StringDataCallback)obj39, (StringDataCallback)obj40, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.Overheal().CompareTo(Right.Overheal()))));
		}
		if (!MasterSwing.ColumnDefs.ContainsKey("OverHeal"))
		{
			Dictionary<string, ColumnDef> columnDefs15 = MasterSwing.ColumnDefs;
			object obj41 = _003C_003Ec._003C_003E9__14_51;
			if (obj41 == null)
			{
				StringDataCallback val41 = (MasterSwing Data) => (!Data.Tags.ContainsKey("overheal")) ? "0" : Data.Tags["overheal"].ToString();
				_003C_003Ec._003C_003E9__14_51 = val41;
				obj41 = (object)val41;
			}
			object obj42 = _003C_003Ec._003C_003E9__14_52;
			if (obj42 == null)
			{
				StringDataCallback val42 = (MasterSwing Data) => (!Data.Tags.ContainsKey("overheal")) ? "0" : Data.Tags["overheal"].ToString();
				_003C_003Ec._003C_003E9__14_52 = val42;
				obj42 = (object)val42;
			}
			columnDefs15.Add("OverHeal", new ColumnDef("OverHeal", false, "INT", "OverHeal", (StringDataCallback)obj41, (StringDataCallback)obj42, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("overheal") ? Left.Tags["overheal"].ToString() : "0", Right.Tags.ContainsKey("overheal") ? Right.Tags["overheal"].ToString() : "0", StringComparison.OrdinalIgnoreCase))));
		}
		if (!MasterSwing.ColumnDefs.ContainsKey("DirectHit"))
		{
			Dictionary<string, ColumnDef> columnDefs16 = MasterSwing.ColumnDefs;
			object obj43 = _003C_003Ec._003C_003E9__14_54;
			if (obj43 == null)
			{
				StringDataCallback val43 = (MasterSwing Data) => (!Data.Tags.ContainsKey("DirectHit")) ? "" : Data.Tags["DirectHit"].ToString();
				_003C_003Ec._003C_003E9__14_54 = val43;
				obj43 = (object)val43;
			}
			object obj44 = _003C_003Ec._003C_003E9__14_55;
			if (obj44 == null)
			{
				StringDataCallback val44 = (MasterSwing Data) => (!Data.Tags.ContainsKey("DirectHit")) ? "" : Data.Tags["DirectHit"].ToString();
				_003C_003Ec._003C_003E9__14_55 = val44;
				obj44 = (object)val44;
			}
			columnDefs16.Add("DirectHit", new ColumnDef("DirectHit", true, "BOOL", "DirectHit", (StringDataCallback)obj43, (StringDataCallback)obj44, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("DirectHit") ? Left.Tags["DirectHit"].ToString() : "", Right.Tags.ContainsKey("DirectHit") ? Right.Tags["DirectHit"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
		}
		if (!AttackType.ColumnDefs.ContainsKey("DirectHitPct"))
		{
			Dictionary<string, ColumnDef> columnDefs17 = AttackType.ColumnDefs;
			object obj45 = _003C_003Ec._003C_003E9__14_57;
			if (obj45 == null)
			{
				StringDataCallback val45 = (AttackType Data) => ((double)Data.DirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_57 = val45;
				obj45 = (object)val45;
			}
			object obj46 = _003C_003Ec._003C_003E9__14_58;
			if (obj46 == null)
			{
				StringDataCallback val46 = (AttackType Data) => ((double)Data.DirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_58 = val46;
				obj46 = (object)val46;
			}
			columnDefs17.Add("DirectHitPct", new ColumnDef("DirectHitPct", true, "VARCHAR(8)", "DirectHitPct", (StringDataCallback)obj45, (StringDataCallback)obj46, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.DirectHitCount().CompareTo(Right.DirectHitCount()))));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("DirectHitPct"))
		{
			Dictionary<string, ColumnDef> columnDefs18 = DamageTypeData.ColumnDefs;
			object obj47 = _003C_003Ec._003C_003E9__14_60;
			if (obj47 == null)
			{
				StringDataCallback val47 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0.0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitPct");
				_003C_003Ec._003C_003E9__14_60 = val47;
				obj47 = (object)val47;
			}
			object obj48 = _003C_003Ec._003C_003E9__14_61;
			if (obj48 == null)
			{
				StringDataCallback val48 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0.0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitPct");
				_003C_003Ec._003C_003E9__14_61 = val48;
				obj48 = (object)val48;
			}
			columnDefs18.Add("DirectHitPct", new ColumnDef("DirectHitPct", true, "VARCHAR(8)", "DirectHitPct", (StringDataCallback)obj47, (StringDataCallback)obj48));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("DirectHitPct"))
		{
			Dictionary<string, ColumnDef> columnDefs19 = CombatantData.ColumnDefs;
			object obj49 = _003C_003Ec._003C_003E9__14_62;
			if (obj49 == null)
			{
				StringDataCallback val49 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct");
				_003C_003Ec._003C_003E9__14_62 = val49;
				obj49 = (object)val49;
			}
			object obj50 = _003C_003Ec._003C_003E9__14_63;
			if (obj50 == null)
			{
				StringDataCallback val50 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct");
				_003C_003Ec._003C_003E9__14_63 = val50;
				obj50 = (object)val50;
			}
			columnDefs19.Add("DirectHitPct", new ColumnDef("DirectHitPct", true, "VARCHAR(8)", "DirectHitPct", (StringDataCallback)obj49, (StringDataCallback)obj50, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitPct"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("DirectHitPct"))
		{
			Dictionary<string, TextExportFormatter> exportVariables13 = CombatantData.ExportVariables;
			object obj51 = _003C_003Ec._003C_003E9__14_65;
			if (obj51 == null)
			{
				ExportStringDataCallback val51 = (CombatantData Data, string ExtraFormat) => ((Data.GetColumnByName("DirectHitPct") == "") ? 0.0 : Convert.ToDouble(Data.GetColumnByName("DirectHitPct").Replace("%", ""), CultureInfo.InvariantCulture)).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_65 = val51;
				obj51 = (object)val51;
			}
			exportVariables13.Add("DirectHitPct", new TextExportFormatter("DirectHitPct", "Direct Hit Percent", "Percent of hits that were Direct Hits.", (ExportStringDataCallback)obj51));
		}
		if (!AttackType.ColumnDefs.ContainsKey("DirectHitCount"))
		{
			Dictionary<string, ColumnDef> columnDefs20 = AttackType.ColumnDefs;
			object obj52 = _003C_003Ec._003C_003E9__14_66;
			if (obj52 == null)
			{
				StringDataCallback val52 = (AttackType Data) => Data.DirectHitCount().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_66 = val52;
				obj52 = (object)val52;
			}
			object obj53 = _003C_003Ec._003C_003E9__14_67;
			if (obj53 == null)
			{
				StringDataCallback val53 = (AttackType Data) => Data.DirectHitCount().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_67 = val53;
				obj53 = (object)val53;
			}
			columnDefs20.Add("DirectHitCount", new ColumnDef("DirectHitCount", false, "INT", "DirectHitCount", (StringDataCallback)obj52, (StringDataCallback)obj53, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.DirectHitCount().CompareTo(Right.DirectHitCount()))));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("DirectHitCount"))
		{
			Dictionary<string, ColumnDef> columnDefs21 = DamageTypeData.ColumnDefs;
			object obj54 = _003C_003Ec._003C_003E9__14_69;
			if (obj54 == null)
			{
				StringDataCallback val54 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? "0" : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitCount");
				_003C_003Ec._003C_003E9__14_69 = val54;
				obj54 = (object)val54;
			}
			object obj55 = _003C_003Ec._003C_003E9__14_70;
			if (obj55 == null)
			{
				StringDataCallback val55 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? "0" : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("DirectHitCount");
				_003C_003Ec._003C_003E9__14_70 = val55;
				obj55 = (object)val55;
			}
			columnDefs21.Add("DirectHitCount", new ColumnDef("DirectHitCount", false, "INT", "DirectHitCount", (StringDataCallback)obj54, (StringDataCallback)obj55));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("DirectHitCount"))
		{
			Dictionary<string, ColumnDef> columnDefs22 = CombatantData.ColumnDefs;
			object obj56 = _003C_003Ec._003C_003E9__14_71;
			if (obj56 == null)
			{
				StringDataCallback val56 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount");
				_003C_003Ec._003C_003E9__14_71 = val56;
				obj56 = (object)val56;
			}
			object obj57 = _003C_003Ec._003C_003E9__14_72;
			if (obj57 == null)
			{
				StringDataCallback val57 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount");
				_003C_003Ec._003C_003E9__14_72 = val57;
				obj57 = (object)val57;
			}
			columnDefs22.Add("DirectHitCount", new ColumnDef("DirectHitCount", false, "INT", "DirectHitCount", (StringDataCallback)obj56, (StringDataCallback)obj57, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("DirectHitCount"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("DirectHitCount"))
		{
			Dictionary<string, TextExportFormatter> exportVariables14 = CombatantData.ExportVariables;
			object obj58 = _003C_003Ec._003C_003E9__14_74;
			if (obj58 == null)
			{
				ExportStringDataCallback val58 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("DirectHitCount");
				_003C_003Ec._003C_003E9__14_74 = val58;
				obj58 = (object)val58;
			}
			exportVariables14.Add("DirectHitCount", new TextExportFormatter("DirectHitCount", "Direct Hit Count", "Number of hits that were direct hit.", (ExportStringDataCallback)obj58));
		}
		if (!AttackType.ColumnDefs.ContainsKey("CritDirectHitCount"))
		{
			Dictionary<string, ColumnDef> columnDefs23 = AttackType.ColumnDefs;
			object obj59 = _003C_003Ec._003C_003E9__14_75;
			if (obj59 == null)
			{
				StringDataCallback val59 = (AttackType Data) => Data.CritDirectHitCount().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_75 = val59;
				obj59 = (object)val59;
			}
			object obj60 = _003C_003Ec._003C_003E9__14_76;
			if (obj60 == null)
			{
				StringDataCallback val60 = (AttackType Data) => Data.CritDirectHitCount().ToString(CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_76 = val60;
				obj60 = (object)val60;
			}
			columnDefs23.Add("CritDirectHitCount", new ColumnDef("CritDirectHitCount", false, "INT", "CritDirectHitCount", (StringDataCallback)obj59, (StringDataCallback)obj60, (Comparison<AttackType>)((AttackType Left, AttackType Right) => Left.CritDirectHitCount().CompareTo(Right.CritDirectHitCount()))));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("CritDirectHitCount"))
		{
			Dictionary<string, ColumnDef> columnDefs24 = DamageTypeData.ColumnDefs;
			object obj61 = _003C_003Ec._003C_003E9__14_78;
			if (obj61 == null)
			{
				StringDataCallback val61 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? "0" : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitCount");
				_003C_003Ec._003C_003E9__14_78 = val61;
				obj61 = (object)val61;
			}
			object obj62 = _003C_003Ec._003C_003E9__14_79;
			if (obj62 == null)
			{
				StringDataCallback val62 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? "0" : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitCount");
				_003C_003Ec._003C_003E9__14_79 = val62;
				obj62 = (object)val62;
			}
			columnDefs24.Add("CritDirectHitCount", new ColumnDef("CritDirectHitCount", false, "INT", "CritDirectHitCount", (StringDataCallback)obj61, (StringDataCallback)obj62));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("CritDirectHitCount"))
		{
			Dictionary<string, ColumnDef> columnDefs25 = CombatantData.ColumnDefs;
			object obj63 = _003C_003Ec._003C_003E9__14_80;
			if (obj63 == null)
			{
				StringDataCallback val63 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount");
				_003C_003Ec._003C_003E9__14_80 = val63;
				obj63 = (object)val63;
			}
			object obj64 = _003C_003Ec._003C_003E9__14_81;
			if (obj64 == null)
			{
				StringDataCallback val64 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount");
				_003C_003Ec._003C_003E9__14_81 = val64;
				obj64 = (object)val64;
			}
			columnDefs25.Add("CritDirectHitCount", new ColumnDef("CritDirectHitCount", false, "INT", "CritDirectHitCount", (StringDataCallback)obj63, (StringDataCallback)obj64, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitCount"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("CritDirectHitCount"))
		{
			Dictionary<string, TextExportFormatter> exportVariables15 = CombatantData.ExportVariables;
			object obj65 = _003C_003Ec._003C_003E9__14_83;
			if (obj65 == null)
			{
				ExportStringDataCallback val65 = (CombatantData Data, string ExtraFormat) => Data.GetColumnByName("CritDirectHitCount");
				_003C_003Ec._003C_003E9__14_83 = val65;
				obj65 = (object)val65;
			}
			exportVariables15.Add("CritDirectHitCount", new TextExportFormatter("CritDirectHitCount", "Crit Direct Hit Count", "Number of hits that were critical as well as direct hit.", (ExportStringDataCallback)obj65));
		}
		if (!AttackType.ColumnDefs.ContainsKey("CritDirectHitPct"))
		{
			Dictionary<string, ColumnDef> columnDefs26 = AttackType.ColumnDefs;
			object obj66 = _003C_003Ec._003C_003E9__14_84;
			if (obj66 == null)
			{
				StringDataCallback val66 = (AttackType Data) => ((double)Data.CritDirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_84 = val66;
				obj66 = (object)val66;
			}
			object obj67 = _003C_003Ec._003C_003E9__14_85;
			if (obj67 == null)
			{
				StringDataCallback val67 = (AttackType Data) => ((double)Data.CritDirectHitCount() * 100.0 / (double)OneOrInt(Data.Items.Count)).ToString("0.0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_85 = val67;
				obj67 = (object)val67;
			}
			columnDefs26.Add("CritDirectHitPct", new ColumnDef("CritDirectHitPct", true, "VARCHAR(8)", "CritDirectHitPct", (StringDataCallback)obj66, (StringDataCallback)obj67, (Comparison<AttackType>)((AttackType Left, AttackType Right) => (Left.CritDirectHitCount() * 100 / OneOrInt(Left.Items.Count)).CompareTo(Right.CritDirectHitCount() * 100 / OneOrInt(Right.Items.Count)))));
		}
		if (!DamageTypeData.ColumnDefs.ContainsKey("CritDirectHitPct"))
		{
			Dictionary<string, ColumnDef> columnDefs27 = DamageTypeData.ColumnDefs;
			object obj68 = _003C_003Ec._003C_003E9__14_87;
			if (obj68 == null)
			{
				StringDataCallback val68 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0.0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitPct");
				_003C_003Ec._003C_003E9__14_87 = val68;
				obj68 = (object)val68;
			}
			object obj69 = _003C_003Ec._003C_003E9__14_88;
			if (obj69 == null)
			{
				StringDataCallback val69 = (DamageTypeData Data) => (!Data.Items.ContainsKey(ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)) ? 0.ToString("0.0'%", CultureInfo.InvariantCulture) : Data.Items[ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].GetColumnByName("CritDirectHitPct");
				_003C_003Ec._003C_003E9__14_88 = val69;
				obj69 = (object)val69;
			}
			columnDefs27.Add("CritDirectHitPct", new ColumnDef("CritDirectHitPct", true, "VARCHAR(8)", "CritDirectHitPct", (StringDataCallback)obj68, (StringDataCallback)obj69));
		}
		if (!CombatantData.ColumnDefs.ContainsKey("CritDirectHitPct"))
		{
			Dictionary<string, ColumnDef> columnDefs28 = CombatantData.ColumnDefs;
			object obj70 = _003C_003Ec._003C_003E9__14_89;
			if (obj70 == null)
			{
				StringDataCallback val70 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct");
				_003C_003Ec._003C_003E9__14_89 = val70;
				obj70 = (object)val70;
			}
			object obj71 = _003C_003Ec._003C_003E9__14_90;
			if (obj71 == null)
			{
				StringDataCallback val71 = (CombatantData Data) => Data.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct");
				_003C_003Ec._003C_003E9__14_90 = val71;
				obj71 = (object)val71;
			}
			columnDefs28.Add("CritDirectHitPct", new ColumnDef("CritDirectHitPct", true, "VARCHAR(8)", "CritDirectHitPct", (StringDataCallback)obj70, (StringDataCallback)obj71, (Comparison<CombatantData>)((CombatantData Left, CombatantData Right) => string.Compare(Left.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct"), Right.Items[CombatantData.DamageTypeDataOutgoingDamage].GetColumnByName("CritDirectHitPct"), StringComparison.OrdinalIgnoreCase))));
		}
		if (!CombatantData.ExportVariables.ContainsKey("CritDirectHitPct"))
		{
			Dictionary<string, TextExportFormatter> exportVariables16 = CombatantData.ExportVariables;
			object obj72 = _003C_003Ec._003C_003E9__14_92;
			if (obj72 == null)
			{
				ExportStringDataCallback val72 = (CombatantData Data, string ExtraFormat) => ((Data.GetColumnByName("CritDirectHitPct") == "") ? 0.0 : Convert.ToDouble(Data.GetColumnByName("CritDirectHitPct").Replace("%", ""), CultureInfo.InvariantCulture)).ToString("0'%", CultureInfo.InvariantCulture);
				_003C_003Ec._003C_003E9__14_92 = val72;
				obj72 = (object)val72;
			}
			exportVariables16.Add("CritDirectHitPct", new TextExportFormatter("CritDirectHitPct", "Crit Direct Hit Percent", "Percent of hits that were Direct Hits as well as Critical Hits.", (ExportStringDataCallback)obj72));
		}
		if (showDebug)
		{
			if (!MasterSwing.ColumnDefs.ContainsKey("Potency"))
			{
				Dictionary<string, ColumnDef> columnDefs29 = MasterSwing.ColumnDefs;
				object obj73 = _003C_003Ec._003C_003E9__14_93;
				if (obj73 == null)
				{
					StringDataCallback val73 = (MasterSwing Data) => (!Data.Tags.ContainsKey("potency")) ? "0" : ((double)Data.Tags["potency"]).ToString("0.00", CultureInfo.InvariantCulture);
					_003C_003Ec._003C_003E9__14_93 = val73;
					obj73 = (object)val73;
				}
				object obj74 = _003C_003Ec._003C_003E9__14_94;
				if (obj74 == null)
				{
					StringDataCallback val74 = (MasterSwing Data) => (!Data.Tags.ContainsKey("potency")) ? "0" : ((double)Data.Tags["potency"]).ToString("0.00", CultureInfo.InvariantCulture);
					_003C_003Ec._003C_003E9__14_94 = val74;
					obj74 = (object)val74;
				}
				columnDefs29.Add("Potency", new ColumnDef("Potency", true, "FLOAT", "Potency", (StringDataCallback)obj73, (StringDataCallback)obj74, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("potency") ? Left.Tags["potency"].ToString() : "0", Right.Tags.ContainsKey("potency") ? Right.Tags["potency"].ToString() : "0", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("StatusEffects"))
			{
				Dictionary<string, ColumnDef> columnDefs30 = MasterSwing.ColumnDefs;
				object obj75 = _003C_003Ec._003C_003E9__14_96;
				if (obj75 == null)
				{
					StringDataCallback val75 = (MasterSwing Data) => (!Data.Tags.ContainsKey("StatusEffects")) ? "" : Data.Tags["StatusEffects"]?.ToString();
					_003C_003Ec._003C_003E9__14_96 = val75;
					obj75 = (object)val75;
				}
				object obj76 = _003C_003Ec._003C_003E9__14_97;
				if (obj76 == null)
				{
					StringDataCallback val76 = (MasterSwing Data) => (!Data.Tags.ContainsKey("StatusEffects")) ? "" : Data.Tags["StatusEffects"]?.ToString();
					_003C_003Ec._003C_003E9__14_97 = val76;
					obj76 = (object)val76;
				}
				columnDefs30.Add("StatusEffects", new ColumnDef("StatusEffects", true, "VARCHAR(50)", "StatusEffects", (StringDataCallback)obj75, (StringDataCallback)obj76, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("StatusEffects") ? Left.Tags["StatusEffects"].ToString() : "", Right.Tags.ContainsKey("StatusEffects") ? Right.Tags["StatusEffects"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("DoTBase"))
			{
				Dictionary<string, ColumnDef> columnDefs31 = MasterSwing.ColumnDefs;
				object obj77 = _003C_003Ec._003C_003E9__14_99;
				if (obj77 == null)
				{
					StringDataCallback val77 = (MasterSwing Data) => (!Data.Tags.ContainsKey("dotbase")) ? "" : ((uint)Data.Tags["dotbase"]).ToString("0", CultureInfo.InvariantCulture);
					_003C_003Ec._003C_003E9__14_99 = val77;
					obj77 = (object)val77;
				}
				object obj78 = _003C_003Ec._003C_003E9__14_100;
				if (obj78 == null)
				{
					StringDataCallback val78 = (MasterSwing Data) => (!Data.Tags.ContainsKey("dotbase")) ? "" : ((uint)Data.Tags["dotbase"]).ToString("0", CultureInfo.InvariantCulture);
					_003C_003Ec._003C_003E9__14_100 = val78;
					obj78 = (object)val78;
				}
				columnDefs31.Add("DoTBase", new ColumnDef("DoTBase", true, "INT", "DoTBase", (StringDataCallback)obj77, (StringDataCallback)obj78, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("dotbase") ? Left.Tags["dotbase"].ToString() : "0", Right.Tags.ContainsKey("dotbase") ? Right.Tags["dotbase"].ToString() : "0", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("BuffByte1"))
			{
				Dictionary<string, ColumnDef> columnDefs32 = MasterSwing.ColumnDefs;
				object obj79 = _003C_003Ec._003C_003E9__14_102;
				if (obj79 == null)
				{
					StringDataCallback val79 = (MasterSwing Data) => (!Data.Tags.ContainsKey("BuffByte1")) ? "" : Data.Tags["BuffByte1"].ToString();
					_003C_003Ec._003C_003E9__14_102 = val79;
					obj79 = (object)val79;
				}
				object obj80 = _003C_003Ec._003C_003E9__14_103;
				if (obj80 == null)
				{
					StringDataCallback val80 = (MasterSwing Data) => (!Data.Tags.ContainsKey("BuffByte1")) ? "" : Data.Tags["BuffByte1"].ToString();
					_003C_003Ec._003C_003E9__14_103 = val80;
					obj80 = (object)val80;
				}
				columnDefs32.Add("BuffByte1", new ColumnDef("BuffByte1", false, "int", "BuffByte1", (StringDataCallback)obj79, (StringDataCallback)obj80, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("BuffByte1") ? Left.Tags["BuffByte1"].ToString() : "", Right.Tags.ContainsKey("BuffByte1") ? Right.Tags["BuffByte1"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("BuffByte2"))
			{
				Dictionary<string, ColumnDef> columnDefs33 = MasterSwing.ColumnDefs;
				object obj81 = _003C_003Ec._003C_003E9__14_105;
				if (obj81 == null)
				{
					StringDataCallback val81 = (MasterSwing Data) => (!Data.Tags.ContainsKey("BuffByte2")) ? "" : Data.Tags["BuffByte2"].ToString();
					_003C_003Ec._003C_003E9__14_105 = val81;
					obj81 = (object)val81;
				}
				object obj82 = _003C_003Ec._003C_003E9__14_106;
				if (obj82 == null)
				{
					StringDataCallback val82 = (MasterSwing Data) => (!Data.Tags.ContainsKey("BuffByte2")) ? "" : Data.Tags["BuffByte2"].ToString();
					_003C_003Ec._003C_003E9__14_106 = val82;
					obj82 = (object)val82;
				}
				columnDefs33.Add("BuffByte2", new ColumnDef("BuffByte2", false, "int", "BuffByte2", (StringDataCallback)obj81, (StringDataCallback)obj82, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("BuffByte2") ? Left.Tags["BuffByte2"].ToString() : "", Right.Tags.ContainsKey("BuffByte2") ? Right.Tags["BuffByte2"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("BuffByte3"))
			{
				Dictionary<string, ColumnDef> columnDefs34 = MasterSwing.ColumnDefs;
				object obj83 = _003C_003Ec._003C_003E9__14_108;
				if (obj83 == null)
				{
					StringDataCallback val83 = (MasterSwing Data) => (!Data.Tags.ContainsKey("BuffByte3")) ? "" : Data.Tags["BuffByte3"].ToString();
					_003C_003Ec._003C_003E9__14_108 = val83;
					obj83 = (object)val83;
				}
				object obj84 = _003C_003Ec._003C_003E9__14_109;
				if (obj84 == null)
				{
					StringDataCallback val84 = (MasterSwing Data) => (!Data.Tags.ContainsKey("BuffByte3")) ? "" : Data.Tags["BuffByte3"].ToString();
					_003C_003Ec._003C_003E9__14_109 = val84;
					obj84 = (object)val84;
				}
				columnDefs34.Add("BuffByte3", new ColumnDef("BuffByte3", false, "int", "BuffByte3", (StringDataCallback)obj83, (StringDataCallback)obj84, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("BuffByte3") ? Left.Tags["BuffByte3"].ToString() : "", Right.Tags.ContainsKey("BuffByte3") ? Right.Tags["BuffByte3"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("CritRate"))
			{
				Dictionary<string, ColumnDef> columnDefs35 = MasterSwing.ColumnDefs;
				object obj85 = _003C_003Ec._003C_003E9__14_111;
				if (obj85 == null)
				{
					StringDataCallback val85 = (MasterSwing Data) => (!Data.Tags.ContainsKey("CritRate")) ? "" : Data.Tags["CritRate"].ToString();
					_003C_003Ec._003C_003E9__14_111 = val85;
					obj85 = (object)val85;
				}
				object obj86 = _003C_003Ec._003C_003E9__14_112;
				if (obj86 == null)
				{
					StringDataCallback val86 = (MasterSwing Data) => (!Data.Tags.ContainsKey("CritRate")) ? "" : Data.Tags["CritRate"].ToString();
					_003C_003Ec._003C_003E9__14_112 = val86;
					obj86 = (object)val86;
				}
				columnDefs35.Add("CritRate", new ColumnDef("CritRate", false, "VARCHAR(8)", "CritRate", (StringDataCallback)obj85, (StringDataCallback)obj86, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("CritRate") ? Left.Tags["CritRate"].ToString() : "0", Right.Tags.ContainsKey("CritRate") ? Right.Tags["CritRate"].ToString() : "0", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("CritEffects"))
			{
				Dictionary<string, ColumnDef> columnDefs36 = MasterSwing.ColumnDefs;
				object obj87 = _003C_003Ec._003C_003E9__14_114;
				if (obj87 == null)
				{
					StringDataCallback val87 = (MasterSwing Data) => (!Data.Tags.ContainsKey("CritEffects")) ? "" : Data.Tags["CritEffects"].ToString();
					_003C_003Ec._003C_003E9__14_114 = val87;
					obj87 = (object)val87;
				}
				object obj88 = _003C_003Ec._003C_003E9__14_115;
				if (obj88 == null)
				{
					StringDataCallback val88 = (MasterSwing Data) => (!Data.Tags.ContainsKey("CritEffects")) ? "" : Data.Tags["CritEffects"].ToString();
					_003C_003Ec._003C_003E9__14_115 = val88;
					obj88 = (object)val88;
				}
				columnDefs36.Add("CritEffects", new ColumnDef("CritEffects", false, "VARCHAR(8)", "CritEffects", (StringDataCallback)obj87, (StringDataCallback)obj88, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("CritEffects") ? Left.Tags["CritEffects"].ToString() : "", Right.Tags.ContainsKey("CritEffects") ? Right.Tags["CritEffects"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("DHRate"))
			{
				Dictionary<string, ColumnDef> columnDefs37 = MasterSwing.ColumnDefs;
				object obj89 = _003C_003Ec._003C_003E9__14_117;
				if (obj89 == null)
				{
					StringDataCallback val89 = (MasterSwing Data) => (!Data.Tags.ContainsKey("DHRate")) ? "" : Data.Tags["DHRate"].ToString();
					_003C_003Ec._003C_003E9__14_117 = val89;
					obj89 = (object)val89;
				}
				object obj90 = _003C_003Ec._003C_003E9__14_118;
				if (obj90 == null)
				{
					StringDataCallback val90 = (MasterSwing Data) => (!Data.Tags.ContainsKey("DHRate")) ? "" : Data.Tags["DHRate"].ToString();
					_003C_003Ec._003C_003E9__14_118 = val90;
					obj90 = (object)val90;
				}
				columnDefs37.Add("DHRate", new ColumnDef("DHRate", false, "VARCHAR(8)", "DHRate", (StringDataCallback)obj89, (StringDataCallback)obj90, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("DHRate") ? Left.Tags["DHRate"].ToString() : "", Right.Tags.ContainsKey("DHRate") ? Right.Tags["DHRate"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
			if (!MasterSwing.ColumnDefs.ContainsKey("DHEffects"))
			{
				Dictionary<string, ColumnDef> columnDefs38 = MasterSwing.ColumnDefs;
				object obj91 = _003C_003Ec._003C_003E9__14_120;
				if (obj91 == null)
				{
					StringDataCallback val91 = (MasterSwing Data) => (!Data.Tags.ContainsKey("DHEffects")) ? "" : Data.Tags["DHEffects"].ToString();
					_003C_003Ec._003C_003E9__14_120 = val91;
					obj91 = (object)val91;
				}
				object obj92 = _003C_003Ec._003C_003E9__14_121;
				if (obj92 == null)
				{
					StringDataCallback val92 = (MasterSwing Data) => (!Data.Tags.ContainsKey("DHEffects")) ? "" : Data.Tags["DHEffects"].ToString();
					_003C_003Ec._003C_003E9__14_121 = val92;
					obj92 = (object)val92;
				}
				columnDefs38.Add("DHEffects", new ColumnDef("DHEffects", false, "VARCHAR(8)", "DHEffects", (StringDataCallback)obj91, (StringDataCallback)obj92, (Comparison<MasterSwing>)((MasterSwing Left, MasterSwing Right) => string.Compare(Left.Tags.ContainsKey("DHEffects") ? Left.Tags["DHEffects"].ToString() : "", Right.Tags.ContainsKey("DHEffects") ? Right.Tags["DHEffects"].ToString() : "", StringComparison.OrdinalIgnoreCase))));
			}
		}
		else
		{
			if (MasterSwing.ColumnDefs.ContainsKey("Potency"))
			{
				MasterSwing.ColumnDefs.Remove("Potency");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("StatusEffects"))
			{
				MasterSwing.ColumnDefs.Remove("StatusEffects");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("DoTBase"))
			{
				MasterSwing.ColumnDefs.Remove("DoTBase");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("BuffByte1"))
			{
				MasterSwing.ColumnDefs.Remove("BuffByte1");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("BuffByte2"))
			{
				MasterSwing.ColumnDefs.Remove("BuffByte2");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("BuffByte3"))
			{
				MasterSwing.ColumnDefs.Remove("BuffByte3");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("CritRate"))
			{
				MasterSwing.ColumnDefs.Remove("CritRate");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("CritEffects"))
			{
				MasterSwing.ColumnDefs.Remove("CritEffects");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("DHRate"))
			{
				MasterSwing.ColumnDefs.Remove("DHRate");
			}
			if (MasterSwing.ColumnDefs.ContainsKey("DHEffects"))
			{
				MasterSwing.ColumnDefs.Remove("DHEffects");
			}
		}
		ActGlobals.oFormActMain.ValidateLists();
		ActGlobals.oFormActMain.ValidateTableSetup();
	}

	private static int OneOrInt(int data)
	{
		if (data == 0)
		{
			return 1;
		}
		return data;
	}

	private static long OneOrInt(long data)
	{
		if (data == 0L)
		{
			return 1L;
		}
		return data;
	}

	public static Bitmap GenAttackTypeGraph(AttackType AttackTypeSource, int SizeX, int SizeY, string Sorting)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Expected O, but got Unknown
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		DateTime now = DateTime.Now;
		if (SizeX < 16 || SizeY < 16)
		{
			return new Bitmap(16, 16);
		}
		Bitmap val = new Bitmap(SizeX, SizeY);
		try
		{
			float result;
			List<MasterSwing> list = AttackTypeSource.Items.Where((MasterSwing x) => x.Tags.ContainsKey("potency") && float.TryParse(x.Tags["potency"].ToString(), out result) && result > 0f).ToList();
			try
			{
				list.Sort((IComparer<MasterSwing>?)new DualComparison(Sorting, Sorting));
			}
			catch (Exception ex)
			{
				ActGlobals.oFormActMain.WriteExceptionLog(ex, string.Empty);
				val = GraphDrawMessage(ex.ToString(), 12f, val);
			}
			SolidBrush val2 = new SolidBrush(Color.LightGray);
			try
			{
				SolidBrush val3 = new SolidBrush(Color.Black);
				try
				{
					Pen val4 = new Pen(Color.Black);
					try
					{
						Pen val5 = new Pen(Color.DarkGray);
						try
						{
							Dictionary<int, SolidBrush> dictionary = new Dictionary<int, SolidBrush>();
							foreach (KeyValuePair<int, List<string>> item in CombatantData.SwingTypeToDamageTypeDataLinksOutgoing)
							{
								if (!dictionary.ContainsKey(item.Key))
								{
									dictionary.Add(item.Key, new SolidBrush(Color.FromArgb(180, CombatantData.OutgoingDamageTypeDataObjects[item.Value[0]].TypeColor)));
								}
							}
							foreach (KeyValuePair<int, List<string>> item2 in CombatantData.SwingTypeToDamageTypeDataLinksIncoming)
							{
								if (!dictionary.ContainsKey(item2.Key))
								{
									dictionary.Add(item2.Key, new SolidBrush(Color.FromArgb(180, CombatantData.IncomingDamageTypeDataObjects[item2.Value[0]].TypeColor)));
								}
							}
							Dictionary<int, SolidBrush> dictionary2 = new Dictionary<int, SolidBrush>();
							foreach (KeyValuePair<int, List<string>> item3 in CombatantData.SwingTypeToDamageTypeDataLinksOutgoing)
							{
								if (!dictionary2.ContainsKey(item3.Key))
								{
									dictionary2.Add(item3.Key, new SolidBrush(Color.FromArgb(255, CombatantData.OutgoingDamageTypeDataObjects[item3.Value[0]].TypeColor)));
								}
							}
							foreach (KeyValuePair<int, List<string>> item4 in CombatantData.SwingTypeToDamageTypeDataLinksIncoming)
							{
								if (!dictionary2.ContainsKey(item4.Key))
								{
									dictionary2.Add(item4.Key, new SolidBrush(Color.FromArgb(255, CombatantData.IncomingDamageTypeDataObjects[item4.Value[0]].TypeColor)));
								}
							}
							Graphics val6 = Graphics.FromImage((Image)(object)val);
							val6.SmoothingMode = (SmoothingMode)4;
							val6.Clear(val2.Color);
							int num = 16;
							Rectangle rectangle = new Rectangle(4, 4, ((Image)val).Width - 1 - num * 4, ((Image)val).Height - 1 - num * 2);
							val6.DrawRectangle(val4, rectangle);
							float num2 = rectangle.Bottom;
							float num3;
							try
							{
								num3 = (float)rectangle.Width / (float)list.Count;
							}
							catch (Exception ex2)
							{
								return GraphDrawMessage(ex2.ToString(), 12f, val);
							}
							float num4 = rectangle.Left;
							float num5 = 0f;
							foreach (MasterSwing item5 in list)
							{
								if ((float)Math.Round(float.Parse(item5.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2) > num5)
								{
									num5 = (float)Math.Round(float.Parse(item5.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2);
								}
							}
							int num6 = num5.ToString().ToCharArray().Length;
							int num7 = (int)Math.Pow(10.0, num6);
							while ((float)(num7 / 2) > num5)
							{
								num7 /= 2;
							}
							if ((float)num7 / 1.25f > num5)
							{
								num7 = Convert.ToInt32((float)num7 / 1.25f);
							}
							float num8 = (float)rectangle.Height / (float)num7;
							_ = 1f / num8;
							Font val7 = new Font("Arial", 8f);
							val6.DrawString("0", val7, (Brush)(object)val3, (float)(rectangle.Right + 5), (float)rectangle.Bottom);
							val6.DrawString(Convert.ToInt32(num7).ToString(), val7, (Brush)(object)val3, (float)(rectangle.Right + 5), (float)rectangle.Top);
							try
							{
								int num9 = Convert.ToInt32(num2 - (float)Convert.ToInt32((float)(num7 / 4) * num8));
								val6.DrawLine(val5, rectangle.Left, num9, rectangle.Right, num9);
								val6.DrawString((num7 / 4).ToString(), val7, (Brush)(object)val3, (float)(rectangle.Right + 5), (float)num9);
								num9 = Convert.ToInt32(num2 - (float)Convert.ToInt32((float)(num7 / 4 * 2) * num8));
								val6.DrawLine(val5, rectangle.Left, num9, rectangle.Right, num9);
								val6.DrawString((num7 / 4 * 2).ToString(), val7, (Brush)(object)val3, (float)(rectangle.Right + 5), (float)num9);
								num9 = Convert.ToInt32(num2 - (float)Convert.ToInt32((float)(num7 / 4 * 3) * num8));
								val6.DrawLine(val5, rectangle.Left, num9, rectangle.Right, num9);
								val6.DrawString((num7 / 4 * 3).ToString(), val7, (Brush)(object)val3, (float)(rectangle.Right + 5), (float)num9);
							}
							catch
							{
							}
							for (int i = 0; i < list.Count; i++)
							{
								MasterSwing val8 = list[i];
								float num10 = num4;
								float num11 = num2 - (float)Math.Round(float.Parse(val8.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2) * num8;
								float num12 = num3;
								float num13 = (float)Math.Round(float.Parse(val8.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2) * num8;
								if (i > 0 && val8.Time != list[i - 1].Time)
								{
									val6.DrawLine(val5, num4, (float)rectangle.Top, num4, (float)rectangle.Bottom);
								}
								if (val8.Critical)
								{
									val6.FillRectangle((Brush)(object)dictionary2[val8.SwingType], num10, num11, num12, num13);
								}
								else
								{
									val6.FillRectangle((Brush)(object)dictionary[val8.SwingType], num10, num11, num12, num13);
								}
								val6.DrawRectangle(val4, num10, num11, num12, num13);
								if (rectangle.Width / list.Count > 15)
								{
									if (rectangle.Width / list.Count > 25)
									{
										val6.DrawString(Math.Round(float.Parse(val8.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2).ToString(CultureInfo.InvariantCulture), val7, (Brush)(object)val3, num4 - 8f + num3 / 2f, num2 + 2f);
									}
									else if (i % 2 == 0)
									{
										val6.DrawString(Math.Round(float.Parse(val8.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2).ToString(CultureInfo.InvariantCulture), val7, (Brush)(object)val3, num4 - 8f + num3 / 2f, num2 + 12f);
									}
									else
									{
										val6.DrawString(Math.Round(float.Parse(val8.Tags["potency"].ToString(), CultureInfo.InvariantCulture), 2).ToString(CultureInfo.InvariantCulture), val7, (Brush)(object)val3, num4 - 8f + num3 / 2f, num2 + 2f);
									}
								}
								num4 += num3;
							}
							val6.DrawLine(val5, num4, (float)rectangle.Top, num4, (float)rectangle.Bottom);
						}
						finally
						{
							((IDisposable)val5)?.Dispose();
						}
					}
					finally
					{
						((IDisposable)val4)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val3)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
		}
		catch (Exception ex3)
		{
			ActGlobals.oFormActMain.WriteExceptionLog(ex3, string.Empty);
			val = GraphDrawMessage(ex3.ToString(), 12f, val);
		}
		TimeSpan timeSpan = DateTime.Now - now;
		ActGlobals.oFormActMain.WriteDebugLog("GraphHitBars: " + timeSpan.TotalMilliseconds.ToString("F"));
		return val;
	}

	internal static Bitmap GraphDrawMessage(string Message, float FontSize, Bitmap BlankImage)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003e: Expected O, but got Unknown
		Graphics obj = Graphics.FromImage((Image)(object)BlankImage);
		obj.SmoothingMode = (SmoothingMode)4;
		obj.Clear(Color.White);
		obj.DrawString(Message, new Font("Arial Black", FontSize, (FontStyle)0), (Brush)new SolidBrush(Color.Black), 12f, 12f);
		return BlankImage;
	}
}
