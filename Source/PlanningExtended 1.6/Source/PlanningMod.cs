using System;
using HarmonyLib;
using PlanningExtended.Defs;
using PlanningExtended.Settings;
using UnityEngine;
using Verse;

namespace PlanningExtended;

public class PlanningMod : Mod
{
    public static Version CurrentVersion { get; } = new(1, 1, 0);

    public static PlanningSettings Settings { get; private set; }

    public static event Action<PlanningSettings> SettingsChanged;

    public PlanningMod(ModContentPack content)
        : base(content)
    {
        Settings = GetSettings<PlanningSettings>();

        Harmony harmony = new("scherub.planningextended");
        harmony.PatchAll();

#if RIMWORLD_1_6
        Log.Message("[Planning Extended] Loaded v1.6 assembly");
#elif RIMWORLD_1_7
        Log.Message("[Planning Extended] Loaded v1.7 assembly");
#endif

        //DefsUpdater.UpdateDefs();
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

        DefsUpdater.UpdateDefs();
        SettingsChanged?.Invoke(Settings);
    }
}
