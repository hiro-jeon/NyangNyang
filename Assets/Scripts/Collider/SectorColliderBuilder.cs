using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SectorColliderBuilder
{
    public static List<Vector2> CalculateSectorPoints(float radius, float angle, int segments)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(Vector2.zero);

        float halfAngle = angle / 2f;
        float angleStep = angle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = -halfAngle + i * angleStep;
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector2 point = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
            points.Add(point);
        }
        return points;
    }

    public static void CreateSectorCollider(
        PolygonCollider2D collider,
        float radius,
        float angle,
        int segments,
        Vector2 direction
    )
    {
        var points = CalculateSectorPoints(radius, angle, segments);

        Quaternion rot = Quaternion.FromToRotation(Vector2.right, direction.normalized);
        for (int i = 0; i < points.Count; i++)
        {
            points[i] = rot * points[i];
        }
        collider.pathCount = 1;
        collider.SetPath(0, points.ToArray());
    }

    public static void DrawSectorGizmo(
        Vector2 origin,
        float radius,
        float angle,
        int segments,
        Vector2 direction,
        Color color
    )
    {
        Gizmos.color = color;

        float halfAngle = angle / 2f;
        float angleStep = angle / segments;
        var points = CalculateSectorPoints(radius, angle, segments);

        Quaternion rot = Quaternion.FromToRotation(Vector2.right, direction.normalized);

        Vector2 lastPoint = origin;
        for (int i = 0; i <= segments; i++)
        {
            Vector2 worldPoint = origin + (Vector2)(rot * points[i]);

            if (i > 0)
            {
                Gizmos.DrawLine(lastPoint, worldPoint);
            }

            lastPoint = worldPoint;

            if (i == 0 || i == segments)
            {
                Gizmos.DrawLine(origin, worldPoint);
            }


        }
    }
}
