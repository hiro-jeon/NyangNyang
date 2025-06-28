using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescentColliderBuilder : MonoBehaviour
{
    public static List<Vector2> CalculateCrescentPoints(float innerRadius, float outerRadius, int segments)
    {
        List<Vector2> points = new List<Vector2>();

        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = i * angleStep;
            float rad = currentAngle * Mathf.Deg2Rad + Mathf.PI;
            Vector2 point = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * outerRadius;
            points.Add(point);
        }

        for (int i = segments; i >= 0; i--) 
        {
            float currentAngle = i * angleStep;
            float rad = currentAngle * Mathf.Deg2Rad + Mathf.PI;
            Vector2 point = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * innerRadius;
            points.Add(point);
        }
        return (points);
    }

    public static void CreateCresentCollider(
        PolygonCollider2D collider,
        float innerRadius,
        float outerRadius,
        int segments,
        Vector2 direction
    )
    {
        var points = CalculateCrescentPoints(innerRadius, outerRadius, segments);
        Vector3 outerOffset = direction.normalized * outerRadius;
        Vector3 innerOffset = direction.normalized * innerRadius;
        Quaternion rot = Quaternion.FromToRotation(Vector2.right, direction.normalized);
        
        for (int i = 0; i < points.Count / 2; i++)
        {
            points[i] = rot * points[i] + outerOffset;
        }
        for (int i = points.Count / 2; i < points.Count; i++)
        {
            points[i] = rot * points[i] + innerOffset;
        }
        collider.pathCount = 1;
        collider.SetPath(0, points.ToArray());
    }

    public static void DrawCrescentGizmo(
        Vector2 origin,
        float innerRadius,
        float outerRadius,
        int segments,
        Vector2 direction,
        Color color
    )
    {
        Gizmos.color = color;

        Vector3 outerOffset = direction.normalized * outerRadius + origin;
        Vector3 innerOffset = direction.normalized * innerRadius + origin;

        var points = CalculateCrescentPoints(innerRadius, outerRadius, segments);

        Quaternion rot = Quaternion.FromToRotation(Vector2.right, direction.normalized);

        for (int i = 0; i < points.Count / 2; i++)
        {
            points[i] = rot * points[i] + outerOffset;
        }
        for (int i = points.Count / 2; i < points.Count; i++)
        {
            points[i] = rot * points[i] + innerOffset;
        }

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 current = points[i];
            Vector2 next = points[(i + 1) % points.Count];
            Gizmos.DrawLine(current, next);
        }
    }
}
