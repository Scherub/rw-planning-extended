namespace PlanningExtended.Shapes.Options
{
    public abstract class BaseShapeOptions
    {
        public virtual bool CanToggleMode => false;

        public virtual bool CanDecreaseIncrease => false;

        public virtual void ToggleMode()
        {

        }

        public virtual void Decrease()
        {

        }

        public virtual void Increase()
        {

        }
    }
}
