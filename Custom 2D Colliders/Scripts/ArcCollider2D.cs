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
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("Physics 2D/Arc Collider 2D")]
public class ArcCollider2D : CustomCollider2D {

    [Range(1, 25), HideInInspector, SerializeField]
    private float radius = 3;
    public float Radius { get { return radius; } set { radius = value; updateCollider(); } }
    [Range(1, 25), HideInInspector, SerializeField]
    private float thickness = 0.4f;
    public float Thickness { get { return thickness; } set { thickness = value; updateCollider(); } }
    [Range(0, 360), SerializeField]
    private float totalAngle = 360;
    public float TotalAngle { get { return totalAngle; } set { totalAngle = value; updateCollider(); } }
    [Range(0, 360), SerializeField]
    private float offsetRotation = 0;
    public float OffsetRotation { get { return offsetRotation; } set { offsetRotation = value; updateCollider(); } }
    [Header("Let there be Pizza"), SerializeField]
    private bool pizzaSlice;
    public bool PizzaSlice { get { return pizzaSlice; } set { pizzaSlice = value; updateCollider(); } }
    [HideInInspector]
    public bool advanced = false;

    public override Vector2[] getPoints()
    {
        List<Vector2> points = new List<Vector2>();
        
        float ang = offsetRotation;

        if (pizzaSlice && totalAngle % 360 != 0)
        {
            points.Add(Vector2.zero);
        }

        for (int i = 0; i <= smoothness; i++)
        {
            float x = radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(ang * Mathf.Deg2Rad);

            points.Add(new Vector2(x, y));
            ang += (float)totalAngle/smoothness;
        }

        if (!pizzaSlice)
        {
            for (int i = 0; i <= smoothness; i++)
            {
                ang -= (float)totalAngle / smoothness;
                float x = (radius - thickness) * Mathf.Cos(ang * Mathf.Deg2Rad);
                float y = (radius - thickness) * Mathf.Sin(ang * Mathf.Deg2Rad);

                points.Add(new Vector2(x, y));
            }
        }

        return points.ToArray();
    }
}