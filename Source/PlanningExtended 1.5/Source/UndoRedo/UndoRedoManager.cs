using System;
using System.Collections.Generic;
using PlanningExtended.Plans;
using PlanningExtended.Settings;
using Verse;

namespace PlanningExtended.UndoRedo
{
    public static class UndoRedoManager
    {
        readonly static List<UndoRedoSnapshot> _undoSnapshots = new();

        readonly static List<UndoRedoSnapshot> _redoSnapshots = new();

        public static bool CanUndo => _undoSnapshots.Count > 0;

        public static bool CanRedo => _redoSnapshots.Count > 0;

        public static bool UseUndoRedo => PlanningMod.Settings.useUndoRedo;

        public static int MaxUndoOperations => PlanningMod.Settings.maxUndoOperations;

        public static event Action OnChanged;

        static UndoRedoManager()
        {
            PlanningMod.SettingsChanged += PlanningMod_SettingsChanged;
        }

        public static void Undo(Map map)
        {
            if (!UseUndoRedo || !CanUndo)
                return;

            UndoRedoSnapshot undoRedoSnapshot = _undoSnapshots[_undoSnapshots.Count - 1];

            PlanLayoutUtilities.DesignateSnapshot(undoRedoSnapshot.UndoPlanLayout, map);

            _undoSnapshots.RemoveAt(_undoSnapshots.Count - 1);
            _redoSnapshots.Add(undoRedoSnapshot);

            //LogText($"Undo: {undoRedoSnapshot.UndoPlanLayout}");

            OnChanged?.Invoke();
        }

        public static void Redo(Map map)
        {
            if (!UseUndoRedo || !CanRedo)
                return;

            UndoRedoSnapshot undoRedoSnapshot = _redoSnapshots[_redoSnapshots.Count - 1];

            PlanLayoutUtilities.DesignateSnapshot(undoRedoSnapshot.RedoPlanLayout, map);

            _redoSnapshots.RemoveAt(_redoSnapshots.Count - 1);
            _undoSnapshots.Add(undoRedoSnapshot);

            //LogText($"Redo: {undoRedoSnapshot.RedoPlanLayout}");

            OnChanged?.Invoke();
        }

        public static void Add(PlanLayout undoPlanLayout, PlanLayout redoPlanLayout)
        {
            if (!UseUndoRedo)
                return;

            UndoRedoSnapshot undoRedoSnapshot = new(undoPlanLayout, redoPlanLayout);

            _undoSnapshots.Add(undoRedoSnapshot);
            _redoSnapshots.Clear();

            ClearUndoSteps();

            OnChanged?.Invoke();

            //LogText($"Added to Undo: {undoPlanLayout} - {redoPlanLayout}");
        }

        static void ClearUndoSteps()
        {
            while (_undoSnapshots.Count > MaxUndoOperations)
                _undoSnapshots.RemoveAt(0);
        }

        //static void LogText(string text)
        //{
        //    Log.Warning($"{text}: Undo-Count ({_undoSnapshots.Count}), Redo-Count ({_redoSnapshots.Count})");
        //}

        static void PlanningMod_SettingsChanged(PlanningSettings settings)
        {
            ClearUndoSteps();
        }
    }
}
