﻿using PlanningExtended.Shapes.Features;
using PlanningExtended.Shapes.Generators;
using PlanningExtended.Shapes.Modifiers.Dimensions;

namespace PlanningExtended.Shapes.Variants.Hexagons
{
    internal class HexagonOutlineShapeVariant : BaseShapeGeneratorVariant<HexagonGenerator>
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.HexagonOutline;

        public HexagonOutlineShapeVariant()
            : base(new RegularHexagonShapeModifier(), new HexagonGenerator(false), new RotationShapeFeature(Direction.Horizontal, Direction.MainAxes))
        {
        }
    }
}
