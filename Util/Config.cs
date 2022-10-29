namespace EZBuild {

	using BepInEx;
	using BepInEx.Configuration;
	using UnityEngine;

	public partial class EZBuild : BaseUnityPlugin {
		/*
		 * Class for defining the Configuration manager config menu
		 */


		private static string GENERAL_SETTINGS = "Settings";

		public static ConfigEntry<bool> EnableMod { 
			get;
			set;
		}
		public static ConfigEntry<bool> InteractWithToolEquipped { 
			get; 
			set;
		}
		public static ConfigEntry<bool> Pipette
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> Pipette_Hotkey {
			get;
			set;
		}

		public static ConfigEntry<bool> Repair
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> Repair_Hotkey { 
			get; 
			set; 
		}
		public static ConfigEntry<bool> Hammer
		{
			get;
			set;
		}
		public static ConfigEntry<KeyboardShortcut> Hammer_Hotkey { 
			get; 
			set;
		}


        private void LoadConfig() {
			// Config menu in order

			// 1
			EnableMod = base.Config.Bind<bool>(GENERAL_SETTINGS, "Enable EZ Build", true, "Enables or disables the mod");

			InteractWithToolEquipped = base.Config.Bind<bool>(GENERAL_SETTINGS, "Enable interaction when a tool is equipped", true, "Enables interaction with e.g. chests, ..., workbenches while holding tools");

			Pipette_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "Hotkey for pipette", new KeyboardShortcut(KeyCode.Q), "Pipette tool to copy piece and orientation of the build piece you're pointing at, or equipping the correct tool for the item you're aimed at.");

			Repair_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "Hotkey for repairing", new KeyboardShortcut(KeyCode.V), "With the hammer equipped, press the hotkey to repair the item you are pointing at.");

			Hammer_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "Hotkey for equipping hammer", new KeyboardShortcut(KeyCode.B), "Hotkey for equipping Hammer");

		}
	}
}