using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using Verse.Steam;

namespace PlanningExtended.Gui
{
    public static class GuiUtilities
    {
        public static TResult DrawImageButton<TResult>(Rect rect, Texture2D texture2D, string keyLabel, TResult currentResult, Func<TResult> resultOnClick)
        {
            if (Widgets.ButtonImage(rect, texture2D, true))
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);
                currentResult = resultOnClick();
                Event.current.Use();
            }

            if (!SteamDeck.IsSteamDeck)
            {
                Widgets.Label(rect, keyLabel);
            }

            return currentResult;
        }
    }
}
