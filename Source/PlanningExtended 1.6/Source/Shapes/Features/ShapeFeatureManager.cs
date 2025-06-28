using System.Collections.Generic;
using System.Linq;

namespace PlanningExtended.Shapes.Features
{
    public class ShapeFeatureManager
    {
        readonly List<IShapeFeature> _shapeFeatures = new();

        public RotationShapeFeature RotationShapeFeature { get; }

        public SegmentShapeFeature SegmentShapeFeature { get; }

        public bool HasRotationFeature => RotationShapeFeature != null;

        public bool HasSegmentFeature => SegmentShapeFeature != null;

        public ShapeDisplayOptions ShapeDisplayOptions { get; }

        public bool RequiresUpdate => _shapeFeatures.Any(sf => sf.RequiresUpdate);

        public ShapeFeatureManager(IEnumerable<IShapeFeature> shapeFeatures)
        {
            if (shapeFeatures != null)
                _shapeFeatures.AddRange(shapeFeatures);

            RotationShapeFeature = _shapeFeatures.FirstOrDefault(fs => fs is RotationShapeFeature) as RotationShapeFeature;
            SegmentShapeFeature = _shapeFeatures.FirstOrDefault(fs => fs is SegmentShapeFeature) as SegmentShapeFeature;

            ShapeDisplayOptions = ShapeDisplayOptions.DisplayVariant;

            foreach (IShapeFeature shapeFeature in _shapeFeatures)
                ShapeDisplayOptions |= shapeFeature.DisplayOptions;
        }

        public void ChangeShapeOption(ShapeOptions shapeOptions, ShapeOptionDirection shapeOptionDirection)
        {
            foreach (var shapeFeature in _shapeFeatures)
                if (shapeFeature.DisplayOptions == shapeOptions.GetShapeDisplayOptions())
                    shapeFeature.ChangeShapeOption(shapeOptions, shapeOptionDirection);
        }

        public virtual void HandleKeyboardInput()
        {
            foreach (IShapeFeature shapeFeature in _shapeFeatures)
                shapeFeature.HandleKeyboardInput();
        }

        public void HandledUpdates()
        {
            _shapeFeatures.ForEach(fs => fs.HandledUpdate());
        }
    }
}
