namespace EZBuild
{
    using BepInEx;
    using BepInEx.Configuration;
    using UnityEngine;

    public partial class EZBuild : BaseUnityPlugin
    {
        /*
		 * Class for defining the Configuration manager config menu
		 */

        private static string GENERAL_SETTINGS = "Settings";

        public static ConfigEntry<bool> EnableMod
        {
            get;
            set;
        }

        public static ConfigEntry<bool> InteractWithToolEquipped
        {
            get;
            set;
        }

        public static ConfigEntry<bool> ScrollingSnapPointSelection
        {
            get;
            set;
        }

        public static ConfigEntry<KeyboardShortcut> ScrollingSnapPointSelectionModifier
        {
            get;
            set;
        }

        public static ConfigEntry<KeyboardShortcut> ScrollingSnapPointSelectionModifier2
        {
            get;
            set;
        }

        public static ConfigEntry<bool> Prefer_2H_Axe
        {
            get;
            set;
        }

        public static ConfigEntry<KeyboardShortcut> Pipette_Hotkey
        {
            get;
            set;
        }

        public static ConfigEntry<KeyboardShortcut> Repair_Hotkey
        {
            get;
            set;
        }

        public static ConfigEntry<KeyboardShortcut> Hammer_Hotkey
        {
            get;
            set;
        }

        public static ConfigEntry<KeyboardShortcut> ScrollPieceSelection_Hotkey
        {
            get;
            set;
        }

        private void LoadConfig()
        {
            // Config menu in order

            EnableMod = base.Config.Bind<bool>(GENERAL_SETTINGS,
                                                "1. Enable EZ Build",
                                                true,
                                                "Enables or disables the mod");

            InteractWithToolEquipped = base.Config.Bind<bool>(GENERAL_SETTINGS,
                                                "2. Enable interaction when a tool is equipped",
                                                true,
                                                "Enables interaction with e.g. chests, ..., workbenches while holding tools");

            ScrollingSnapPointSelectionModifier = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS,
                                                 "3. Modifier for scrolling through snap-points",
                                                 new KeyboardShortcut(KeyCode.LeftAlt),
                                                 "Hold modifier+mouse scroll to iterate over items in placemode (while building and not in the menu).");

            Prefer_2H_Axe = base.Config.Bind<bool>(GENERAL_SETTINGS,
                                                "4. Prefer 2H Axe over 1H Axe",
                                                false,
                                                "Toggles which axe is preffered when pipetting on trees etc.");

            Pipette_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS,
                                                "5. Hotkey for pipette",
                                                new KeyboardShortcut(KeyCode.Q),
                                                "Pipette tool to copy piece and orientation of the build piece you're pointing at, or equipping the correct tool for the item you're aimed at.");

            Repair_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS,
                                                "6. Hotkey for repairing",
                                                new KeyboardShortcut(KeyCode.V),
                                                "With the hammer equipped, press the hotkey to repair the item you are pointing at.");

            Hammer_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS,
                                                "7. Hotkey for equipping hammer",
                                                new KeyboardShortcut(KeyCode.B),
                                                "Hotkey for equipping Hammer");

            ScrollPieceSelection_Hotkey = base.Config.Bind<KeyboardShortcut>(GENERAL_SETTINGS,
                                                "8. Modifier for scrolling piece selection",
                                                new KeyboardShortcut(KeyCode.LeftControl), "Hold modifier+mouse scroll to iterate over items in the hammer/hoe/cultivator menu");
        }
    }
}