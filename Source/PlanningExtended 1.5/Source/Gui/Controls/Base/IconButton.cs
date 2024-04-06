using System;
using PlanningExtended.Gui.Controls.Grid;
using UnityEngine;
using Verse;

namespace PlanningExtended.Gui.Controls
{
    internal class IconButton : BaseButton
    {
        protected Image Icon => (Image)Child;

        public virtual Color IconDrawColor => Color.white;

        public Texture2D IconTexture
        {
            get { return ((Image)Child).Texture; }
            set { ((Image)Child).Texture = value; }
        }

        public float IconScale
        {
            get { return ((Image)Child).Scale; }
            set { ((Image)Child).Scale = value; }
        }

        public IconButton(string texturePath, GridPosition? gridPosition = null, Thickness? margin = null, bool showBorder = true, float iconScale = 0.85f, Action onClick = null)
            : base(new Image(texturePath, iconScale), gridPosition, margin, showBorder, onClick)
        {
        }

        public IconButton(Texture2D texture, GridPosition? gridPosition = null, Thickness? margin = null, bool showBorder = true, float iconScale = 0.85f, Action onClick = null)
            : base(new Image(texture, iconScale), gridPosition, margin, showBorder, onClick)
        {
        }

        protected override void OnDraw(Rect rect)
        {
            base.OnDraw(rect);

            Icon.IsEnabled = IsEnabled;
            Icon.IconDrawColor = IconDrawColor;

            Child.Draw(rect);

            if (IsEnabled && Widgets.ButtonInvisible(rect))
                OnClick?.Invoke();
        }
    }
}
