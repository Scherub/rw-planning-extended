using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal class Image : BaseControl
    {
        public Texture2D Texture { get; set; }

        public float Scale { get; set; }

        public Color IconDrawColor { get; set; }

        public Image(string texturePath, float scale = 1f, GridPosition? gridPosition = null, Thickness? margin = null)
            : base(gridPosition, margin)
        {
            Texture = ContentFinder<Texture2D>.Get(texturePath, true);
            Scale = scale;
        }

        public Image(Texture2D texture, float scale = 1f, GridPosition? gridPosition = null, Thickness? margin = null)
            : base(gridPosition, margin)
        {
            Texture = texture;
            Scale = scale;
        }

        protected override void OnDraw(Rect rect)
        {
            Material material = IsEnabled ? null : TexUI.GrayscaleGUI;

            Color oldColor = GUI.color;

            GUI.color = IconDrawColor;

            Widgets.DrawTextureFitted(rect, Texture, Scale, material);

            GUI.color = oldColor;
        }
    }
}
