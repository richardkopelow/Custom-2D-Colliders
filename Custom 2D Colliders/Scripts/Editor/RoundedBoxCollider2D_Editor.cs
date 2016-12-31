/*
The MIT License (MIT)

Modified work Copyright (c) 2016 Richard Kopelow
Original work Copyright (c) 2016 GuyQuad

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

You can contact me by email at guyquad27@gmail.com or on Reddit at https://www.reddit.com/user/GuyQuad
*/


using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(RoundedBoxCollider2D))]
public class RoundedBoxCollider_Editor : Editor {

    RoundedBoxCollider2D rb;
    PolygonCollider2D polyCollider;
    Vector2 off;

    void OnEnable()
    {
        rb = (RoundedBoxCollider2D)target;

        polyCollider = rb.GetComponent<PolygonCollider2D>();
        if (polyCollider == null) {
            polyCollider = rb.gameObject.AddComponent<PolygonCollider2D>();
        }

        Vector2[] pts = rb.getPoints();
        if (pts != null) polyCollider.points = pts;
    }

    public override void OnInspectorGUI()
    {
        GUI.changed = false;
        DrawDefaultInspector();       


        // automatically adjust the radius according to width and height
        float lesser = (rb.Width > rb.Height) ? rb.Height : rb.Width;
        lesser /= 2f;
        lesser = Mathf.Round(lesser * 100f) / 100f; 
        rb.Radius = EditorGUILayout.Slider("Radius",rb.Radius, 0f, lesser);
        rb.Radius = Mathf.Clamp(rb.Radius, 0f, lesser);
        
        rb.advanced = EditorGUILayout.Toggle("Advanced", rb.advanced);
        if (rb.advanced)
        {
            rb.Height = EditorGUILayout.FloatField("Height", rb.Height);
            rb.Width = EditorGUILayout.FloatField("Width", rb.Width);
        }
        else
        {
            rb.Height = EditorGUILayout.Slider("Height", rb.Height, 1, 25);
            rb.Width = EditorGUILayout.Slider("Width", rb.Width, 1, 25);
        }

        if (GUILayout.Button("Reset"))
        {
            rb.Smoothness = 15;
            rb.Width = 2;
            rb.Height = 2;
            rb.Trapezoid = 0.5f;
            rb.Radius = 0.5f;
            polyCollider.offset = Vector2.zero;
        }

        if (GUI.changed || !off.Equals(polyCollider.offset))
        {
            Vector2[] pts = rb.getPoints();
            if (pts != null) polyCollider.points = pts;
        }

        off = polyCollider.offset;
    }
    
}
