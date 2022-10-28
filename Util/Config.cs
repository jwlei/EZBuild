namespace EZBuild {

	using BepInEx;
	using BepInEx.Configuration;
	using UnityEngine;

	public partial class EZBuild : BaseUnityPlugin {
		private static string GENERAL_SETTINGS = "Settings";

		public static ConfigEntry<bool> enabledMod { 
			get;
			set;
		}
		public static ConfigEntry<bool> interactWhileBuilding { 
			get; 
			set;
		}
		public static ConfigEntry<bool> EZPipette
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> EZPipetteHotkey {
			get;
			set;
		}
		public static ConfigEntry<bool> EZAxe
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> EZAxeHotkey
		{
			get;
			set;
		}
		public static ConfigEntry<bool> EZPickaxe
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> EZPickaxeHotkey
		{
			get;
			set;
		}
		public static ConfigEntry<bool> EZRepair
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> EZRepairHotkey { 
			get; 
			set; 
		}
		public static ConfigEntry<bool> EZHammer
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> EZHammerHotkey { 
			get; 
			set;
		}





        private void LoadConfig()
        {
			enabledMod = base.Config.Bind<bool>(GENERAL_SETTINGS, "Enable", true, "Enable EZBuild (True/False)");
			interactWhileBuilding = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Enables interaction while holding tools");

			EZPipette = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Enables interaction while holding tools");
			EZPipetteHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EZPipetteHotkey", new KeyboardShortcut(KeyCode.Q), "Pipette tool to copy piece and orientation of the build piece you're pointing at.");

			EZAxe = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Enables interaction while holding tools");
			EZAxeHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EZAxeHotkey", new KeyboardShortcut(KeyCode.Q), "Equip the axe when pointing at an eligible resource.");

			EZPickaxe = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Enables interaction while holding tools");
			EZPickaxeHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EZPickaxeHotkey", new KeyboardShortcut(KeyCode.Q), "Equip the pickaxe when pointing at an eligible resource.");

			EZRepair = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Enables interaction while holding tools");
			EZRepairHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EZRepairHotkey", new KeyboardShortcut(KeyCode.V), "With the hammer equipped, press the hotkey to repair the item you are pointing at.");

			EZHammer = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Enables interaction while holding tools");
			EZHammerHotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "EZHammerHotkey", new KeyboardShortcut(KeyCode.B), "Hotkey for equipping Hammer");

		}
	}
}