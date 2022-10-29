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

			EnableMod = base.Config.Bind<bool>(GENERAL_SETTINGS, "Enable", true, "Enable EZBuild");

			InteractWithToolEquipped = base.Config.Bind<bool>(GENERAL_SETTINGS, "Enable interaction with items while a tool is equipped", true, "Enables interaction with e.g. chests, ..., workbenches while holding tools");

			Pipette = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Pipette tool");
			Pipette_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "Pipette hotkey", new KeyboardShortcut(KeyCode.Q), "Pipette tool to copy piece and orientation of the build piece you're pointing at, or equipping the correct tool for the item you're aimed at.");

			Repair = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Hotkey for repairing");
			Repair_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "Repair hotkey", new KeyboardShortcut(KeyCode.V), "With the hammer equipped, press the hotkey to repair the item you are pointing at.");

			Hammer = base.Config.Bind<bool>(GENERAL_SETTINGS, "Interact with tool equipped", true, "Hotkey for equipping hammer");
			Hammer_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS, "Equip hammer hotkey", new KeyboardShortcut(KeyCode.B), "Hotkey for equipping Hammer");

		}
	}
}