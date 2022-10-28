#if UNITY_EDITOR

using Unity.Mathematics;
using UnityEngine;

namespace AeternumGames.ShapeEditor
{
    public class BottomToolbarWindow : GuiWindow
    {
        private GuiLabel statusLabel;
        private GuiFloatTextbox gridZoomTextbox;
        private GuiLabel gridZoomLabel;
        
        private GuiFloatTextbox scaleSnapTextbox;
        private GuiButton scaleDoubleSnapButton;
        private GuiButton scaleHalfSnapButton;
        private GuiLabel scaleSnapLabel;

        private GuiFloatTextbox gridSnapTextbox;
        private GuiButton gridDoubleSnapButton;
        private GuiButton gridHalfSnapButton;
        private GuiLabel gridSnapLabel;
        
        private GuiFloatTextbox angleSnapTextbox;
        private GuiButton angleDoubleSnapButton;
        private GuiButton angleHalfSnapButton;
        private GuiLabel angleSnapLabel;
        
        private GuiButton snappingToggleButton;

        public BottomToolbarWindow(float2 position, float2 size) : base(position, size) { }

        public override void OnActivate()
        {
            base.OnActivate();

            var resources = ShapeEditorResources.Instance;
            colorWindowBackground = new Color(0.192f, 0.192f, 0.192f);

            Add(statusLabel = new GuiLabel("", new float2(7f, 4f), new float2(200, 20)));

            Add(gridZoomTextbox = new GuiFloatTextbox(new float2(50, 16)) { allowNegativeNumbers = false });
            Add(gridZoomLabel = new GuiLabel("Zoom:", new float2(32, 20)));
            
            Add(scaleSnapTextbox = new GuiFloatTextbox(new float2(50, 16)) { allowNegativeNumbers = false });
            Add(scaleDoubleSnapButton = new GuiButton(resources.shapeEditorGridDouble, 20, editor.ScaleTenTimesUpSnap));
            Add(scaleHalfSnapButton = new GuiButton(resources.shapeEditorGridHalf, 20, editor.ScaleTenTimesDownSnap));
            Add(scaleSnapLabel = new GuiLabel("Scale:", new float2(30, 20)));

            Add(gridSnapTextbox = new GuiFloatTextbox(new float2(50, 16)) { allowNegativeNumbers = false });
            Add(gridDoubleSnapButton = new GuiButton(resources.shapeEditorGridDouble, 20, editor.GridDoubleSnap));
            Add(gridHalfSnapButton = new GuiButton(resources.shapeEditorGridHalf, 20, editor.GridHalfSnap));
            Add(gridSnapLabel = new GuiLabel("Snap:", new float2(30, 20)));
            
            Add(angleSnapTextbox = new GuiFloatTextbox(new float2(50, 16)) { allowNegativeNumbers = false });
            Add(angleDoubleSnapButton = new GuiButton(resources.shapeEditorGridDouble, 20, editor.AngleDoubleSnap));
            Add(angleHalfSnapButton = new GuiButton(resources.shapeEditorGridHalf, 20, editor.AngleHalfSnap));
            Add(angleSnapLabel = new GuiLabel("Angle:", new float2(32, 20)));

            Add(snappingToggleButton = new GuiButton(resources.shapeEditorSnapping, 20, editor.UserToggleGridSnapping));
        }

        public override void OnRender()
        {
            var resources = ShapeEditorResources.Instance;

            // stretch over the width of the window.
            position = new float2(0f, editor.position.height - 22f);
            size = new float2(editor.position.width, 22f);

            statusLabel.text = "2D Shape Editor (" + editor.totalSegmentsCount + " Segments, Render: " + editor.lastRenderTime + "ms)";

            var xpos = size.x;

            // grid zoom textbox:
            xpos -= gridZoomTextbox.size.x + 3f;
            gridZoomTextbox.position = new float2(xpos, 3f);
            editor.gridZoom = gridZoomTextbox.UpdateValue(editor.gridZoom);

            // grid zoom label:
            xpos -= gridZoomLabel.size.x + 3f;
            gridZoomLabel.position = new float2(xpos, 4f);

            
            // scale snap textbox:
            xpos -= scaleSnapTextbox.size.x + 3f;
            scaleSnapTextbox.position = new float2(xpos, 3f);
            editor.scaleSnap = scaleSnapTextbox.UpdateValue(editor.scaleSnap);

            // scale double snap button:
            xpos -= scaleDoubleSnapButton.size.x + 3f;
            scaleDoubleSnapButton.position = new float2(xpos, 1f);

            // scale half snap button:
            xpos -= scaleHalfSnapButton.size.x + 3f;
            scaleHalfSnapButton.position = new float2(xpos, 1f);

            // scale snap label:
            xpos -= scaleSnapLabel.size.x + 3f;
            scaleSnapLabel.position = new float2(xpos, 4f);

            
            // grid snap textbox:
            xpos -= gridSnapTextbox.size.x + 3f;
            gridSnapTextbox.position = new float2(xpos, 3f);
            editor.gridSnap = gridSnapTextbox.UpdateValue(editor.gridSnap);

            // grid double snap button:
            xpos -= gridDoubleSnapButton.size.x + 3f;
            gridDoubleSnapButton.position = new float2(xpos, 1f);

            // grid half snap button:
            xpos -= gridHalfSnapButton.size.x + 3f;
            gridHalfSnapButton.position = new float2(xpos, 1f);

            // grid snap label:
            xpos -= gridSnapLabel.size.x + 3f;
            gridSnapLabel.position = new float2(xpos, 4f);

            
            // angle snap textbox:
            xpos -= angleSnapTextbox.size.x + 3f;
            angleSnapTextbox.position = new float2(xpos, 3f);
            editor.angleSnap = angleSnapTextbox.UpdateValue(editor.angleSnap);

            // angle double snap button:
            xpos -= angleDoubleSnapButton.size.x + 3f;
            angleDoubleSnapButton.position = new float2(xpos, 1f);

            // angle half snap button:
            xpos -= angleHalfSnapButton.size.x + 3f;
            angleHalfSnapButton.position = new float2(xpos, 1f);

            // angle snap label:
            xpos -= angleSnapLabel.size.x + 3f;
            angleSnapLabel.position = new float2(xpos, 4f);

            
            // snapping toggle:
            xpos -= snappingToggleButton.size.x + 3f;
            snappingToggleButton.position = new float2(xpos, 1f);
            snappingToggleButton.isChecked = editor.snapEnabled;
            snappingToggleButton.icon = editor.snapEnabled ? resources.shapeEditorSnapping : resources.shapeEditorSnappingDisabled;

            base.OnRender();
        }
    }
}

#endif