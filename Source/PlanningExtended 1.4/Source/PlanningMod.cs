using System;
using PlanningExtended.Settings;
using UnityEngine;
using Verse;

namespace PlanningExtended
{
    public class PlanningMod : Mod
    {
        public static PlanningSettings Settings { get; private set; }

        public static event Action<PlanningSettings> SettingsChanged;

        public PlanningMod(ModContentPack content)
            : base(content)
        {
            Settings = GetSettings<PlanningSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            SettingsGuiUtilities.DisplaySettings(Settings, inRect);

            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Planning Extended";
        }

        public override void WriteSettings()
        {
            base.WriteSettings();

            SettingsChanged?.Invoke(Settings);
        }
    }
}
