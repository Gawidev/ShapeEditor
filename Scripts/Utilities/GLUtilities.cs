﻿#if UNITY_EDITOR

using UnityEngine;

namespace AeternumGames.ShapeEditor
{
    public static class GLUtilities
    {
        public static void DrawRectangle(float x, float y, float w, float h)
        {
            w += x;
            h += y;
            GL.TexCoord2(0, 0);
            GL.Vertex3(x, y, 0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(x, h, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(w, h, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(w, y, 0);
        }

        public static void DrawFlippedRectangle(float x, float y, float w, float h)
        {
            w += x;
            h += y;
            GL.TexCoord2(0, 1);
            GL.Vertex3(x, y, 0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(x, h, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(w, h, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(w, y, 0);
        }

        public static void DrawLine(float thickness, float x1, float y1, float x2, float y2)
        {
            var point1 = new Vector2(x1, y1);
            var point2 = new Vector2(x2, y2);

            Vector2 startPoint = Vector2.zero;
            Vector2 endPoint = Vector2.zero;

            var diffx = Mathf.Abs(point1.x - point2.x);
            var diffy = Mathf.Abs(point1.y - point2.y);

            if (diffx > diffy)
            {
                if (point1.x <= point2.x)
                {
                    startPoint = point1;
                    endPoint = point2;
                }
                else
                {
                    startPoint = point2;
                    endPoint = point1;
                }
            }
            else
            {
                if (point1.y <= point2.y)
                {
                    startPoint = point1;
                    endPoint = point2;
                }
                else
                {
                    startPoint = point2;
                    endPoint = point1;
                }
            }

            var angle = Mathf.Atan2(endPoint.y - startPoint.y, endPoint.x - startPoint.x);
            var perp = angle + Mathf.PI * 0.5f;

            var p1 = Vector3.zero;
            var p2 = Vector3.zero;
            var p3 = Vector3.zero;
            var p4 = Vector3.zero;

            var cosAngle = Mathf.Cos(angle);
            var cosPerp = Mathf.Cos(perp);
            var sinAngle = Mathf.Sin(angle);
            var sinPerp = Mathf.Sin(perp);

            var distance = Vector2.Distance(startPoint, endPoint);

            p1.x = startPoint.x - (thickness * 0.5f) * cosPerp;
            p1.y = startPoint.y - (thickness * 0.5f) * sinPerp;

            p2.x = startPoint.x + (thickness * 0.5f) * cosPerp;
            p2.y = startPoint.y + (thickness * 0.5f) * sinPerp;

            p3.x = p2.x + distance * cosAngle;
            p3.y = p2.y + distance * sinAngle;

            p4.x = p1.x + distance * cosAngle;
            p4.y = p1.y + distance * sinAngle;

            GL.Vertex3(p1.x, p1.y, 0);
            GL.Vertex3(p2.x, p2.y, 0);
            GL.Vertex3(p3.x, p3.y, 0);
            GL.Vertex3(p4.x, p4.y, 0);
        }
    }
}

#endif