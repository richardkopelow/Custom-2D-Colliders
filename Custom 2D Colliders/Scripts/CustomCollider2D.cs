using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
public abstract class CustomCollider2D : MonoBehaviour
{
    [Range(10, 90), SerializeField]
    protected int smoothness = 24;
    public int Smoothness { get { return smoothness; } set { smoothness = value; updateCollider(); } }

    public abstract Vector2[] getPoints();
    protected void updateCollider()
    {
        GetComponent<PolygonCollider2D>().points = getPoints();
    } 
}
