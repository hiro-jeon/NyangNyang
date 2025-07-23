using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescentColliderBuilder : MonoBehaviour
{
    public static void CreateCresentCollider(
        PolygonCollider2D collider,
        Vector2 direction,
        float innerRadius,
        float outerRadius,
        int segments
    )
    {
        var points = PointsBuilder.GetCrescentPoints(direction, innerRadius, outerRadius, segments);
        collider.pathCount = 1;
        collider.SetPath(0, points.ToArray());
    }

    public static void DrawCrescentGizmo(
        Vector2 origin,
        Vector2 direction,
        float innerRadius,
        float outerRadius,
        int segments,
        Color color
    )
    {
        var points = PointsBuilder.GetCrescentPoints(direction, innerRadius, outerRadius, segments);

        Gizmos.color = color;

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 current = points[i];
            Vector2 next = points[(i + 1) % points.Count];
            Gizmos.DrawLine(current, next);
        }
    }
}
