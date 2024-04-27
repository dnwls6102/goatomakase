using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class gizmo : MonoBehaviour
{
    public Vector2 center;
    public Vector2 size;

    private void Start()
    {
        center = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
