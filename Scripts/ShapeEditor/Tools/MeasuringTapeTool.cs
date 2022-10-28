#if UNITY_EDITOR

using Unity.Mathematics;
using UnityEngine;

namespace AeternumGames.ShapeEditor
{
    public class MeasuringTapeTool : Tool
    {
        private enum MeasuringMode
        {
            None,
            Draw,
            Drag
        }

        private MeasuringMode curMeasuringMode;
        
        private readonly Pivot startPivot = new Pivot();
        private readonly Pivot endPivot = new Pivot();
        
        private Pivot grabbedPivot;
        private float measuringLength;
        private bool isShiftDown;

        private const int grabDistance = 10;
        private static readonly Color pivotColor = new Color(1.0f, 0.5f, 0.0f);

        public override void OnActivate()
        {
            // SetPivotPosition(startPivot);
            // SetPivotPosition(endPivot);
            curMeasuringMode = MeasuringMode.None;
        }

        public override void OnRender()
        {
            float2 p1 = editor.GridPointToScreen(startPivot.position);
            float2 p2 = editor.GridPointToScreen(endPivot.position);

            GLUtilities.DrawGui((() =>
            {
                GL.Color(Color.white);
                GLUtilities.DrawLine(1f, p1, p2);
                GL.Color(Color.magenta);
                GLUtilities.DrawDottedLine(1f, p1, p2, 16f);

                GLUtilities.DrawCircle(2f, p1, 6f, pivotColor, 4);
                GLUtilities.DrawCircle(2f, p2, 6f, pivotColor, 4);
            }));

            // if (curMeasuringMode == MeasuringMode.None) return;

            string distance = measuringLength.ToStringHumans() + "m";
            if (distance == "0m") return;

            float2 mid = (p1 + p2) / 2f;
            GLUtilities.DrawGuiText(ShapeEditorResources.fontSegoeUI14, distance, mid);
        }

        public override void OnMouseDown(int button)
        {
            if (button == 0)
            {
                if (isShiftDown)
                {
                    SetPivotPosition(startPivot);
                    SetPivotPosition(endPivot);
                    curMeasuringMode = MeasuringMode.Draw;
                    measuringLength = 0f;
                }
                else
                {
                    float2 p1 = editor.GridPointToScreen(startPivot.position);
                    float2 p2 = editor.GridPointToScreen(endPivot.position);
                    float startDist = math.distance(p1, editor.mousePosition);
                    float endDist = math.distance(p2, editor.mousePosition);
                    
                    if (startDist < grabDistance)
                    {
                        grabbedPivot = startPivot;
                        curMeasuringMode = MeasuringMode.Drag;
                    }
                    else if (endDist < grabDistance)
                    {
                        grabbedPivot = endPivot;
                        curMeasuringMode = MeasuringMode.Drag;
                    }
                }
            }
        }

        public override void OnMouseDrag(int button, float2 screenDelta, float2 gridDelta)
        {
            if (button == 0)
            {
                switch (curMeasuringMode)
                {
                    default:
                    case MeasuringMode.None:
                        break;
                    
                    case MeasuringMode.Draw:
                        SetPivotPosition(endPivot);
                        measuringLength = math.distance(startPivot.position, endPivot.position);
                        break;
                    
                    case MeasuringMode.Drag:
                        SetPivotPosition(grabbedPivot);
                        measuringLength = math.distance(startPivot.position, endPivot.position);
                        break;
                }
            }
        }

        public override void OnMouseUp(int button)
        {
            if (button == 0)
            {
                switch (curMeasuringMode)
                {
                    default:
                    case MeasuringMode.None:
                        break;
                    
                    case MeasuringMode.Draw:
                        SetPivotPosition(endPivot);
                        break;
                    
                    case MeasuringMode.Drag:
                        SetPivotPosition(grabbedPivot);
                        break;
                }
                
                curMeasuringMode = MeasuringMode.None;
            }
        }

        private void SetPivotPosition(Pivot pivot)
        {
            pivot.position = editor.isSnapping ?
                editor.mouseGridPosition.Snap(editor.gridSnap) : editor.mouseGridPosition;
        }

        public override bool OnKeyDown(KeyCode keyCode)
        {
            if (keyCode == KeyCode.LeftShift || keyCode == KeyCode.RightShift)
            {
                isShiftDown = true;
            }

            return isShiftDown;
        }

        public override bool OnKeyUp(KeyCode keyCode)
        {
            if (keyCode == KeyCode.LeftShift || keyCode == KeyCode.RightShift)
            {
                isShiftDown = false;
            }

            return isShiftDown;
        }
    }
}

#endif