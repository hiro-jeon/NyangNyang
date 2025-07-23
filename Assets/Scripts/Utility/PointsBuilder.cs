using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsBuilder : MonoBehaviour
{
    public static List<Vector2> GetCirclePoints(
        float radius,
        int segments
    )
    {
        List<Vector2> points = new List<Vector2>();

        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad + Mathf.PI;
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            points.Add(point);
        }
        return (points);
    }

    public static List<Vector2> GetCrescentPoints(
        Vector2 direction,
        float innerRadius,
        float outerRadius,
        int segments
    )
    {
        var innerPoints = GetCirclePoints(innerRadius, segments);
        var outerPoints = GetCirclePoints(outerRadius, segments);

        Vector3 innerOffset = direction.normalized * innerRadius;
        Vector3 outerOffset = direction.normalized * outerRadius;

        Quaternion rot = Quaternion.FromToRotation(Vector2.right, direction.normalized);

        for (int i = 0; i <= segments; i++)
        {
            innerPoints[i] = rot * innerPoints[i] + innerOffset;
            outerPoints[i] = rot * outerPoints[i] + outerOffset;
        }
        List<Vector2> points = new List<Vector2>(innerPoints);
        points.AddRange(outerPoints);
        return (points);
    }
}
