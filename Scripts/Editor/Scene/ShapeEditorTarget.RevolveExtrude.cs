﻿#if UNITY_EDITOR

using UnityEngine;

namespace AeternumGames.ShapeEditor
{
    public partial class ShapeEditorTarget
    {
        // revolves the shape around the center.

        [SerializeField]
        [Min(1)]
        internal int revolveExtrudePrecision = 8;

        [SerializeField]
        [Range(0.0f, 360.0f)]
        internal float revolveExtrudeDegrees = 90f;

        [SerializeField]
        [Min(0f)]
        internal float revolveExtrudeRadius = 2f;

        private void RevolveExtrude_Rebuild()
        {
            var mesh = MeshGenerator.CreateRevolveExtrudedMesh(convexPolygons2D, revolveExtrudePrecision, revolveExtrudeDegrees, revolveExtrudeRadius);
            OnShapeEditorMesh(mesh);
        }
    }
}

#endif