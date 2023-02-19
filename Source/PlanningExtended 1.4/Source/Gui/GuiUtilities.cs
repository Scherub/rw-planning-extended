using RimWorld;
using UnityEngine;
using Verse.Steam;
using Verse;
using Verse.Sound;

namespace PlanningExtended.Gui
{
    public static class GuiUtilities
    {
        public static RotationDirection DrawButtonImageRotation(Rect rect, Texture2D texture2D, string keyLabel, RotationDirection resultingRotationDirection, RotationDirection currentRotationDirection)
        {
            if (Widgets.ButtonImage(rect, texture2D, true))
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);
                currentRotationDirection = resultingRotationDirection;
                Event.current.Use();
            }

            if (!SteamDeck.IsSteamDeck)
            {
                Widgets.Label(rect, keyLabel);
            }

            return currentRotationDirection;
        }

        public static FlipDirection DrawButtonImageFlip(Rect rect, Texture2D texture2D, string keyLabel, FlipDirection resultingFlipDirection, FlipDirection currentFlipDirection)
        {
            if (Widgets.ButtonImage(rect, texture2D, true))
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);
                currentFlipDirection = resultingFlipDirection;
                Event.current.Use();
            }

            if (!SteamDeck.IsSteamDeck)
            {
                Widgets.Label(rect, keyLabel);
            }

            return currentFlipDirection;
        }

        public static ShapeOptionDirection DrawButtonImageShapeOption(Rect rect, Texture2D texture2D, string keyLabel, ShapeOptionDirection resultingShapeOptionDirection, ShapeOptionDirection currentShapeOptionDirection)
        {
            if (Widgets.ButtonImage(rect, texture2D, true))
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera(null);
                currentShapeOptionDirection = resultingShapeOptionDirection;
                Event.current.Use();
            }

            if (!SteamDeck.IsSteamDeck)
            {
                Widgets.Label(rect, keyLabel);
            }

            return currentShapeOptionDirection;
        }
    }
}
