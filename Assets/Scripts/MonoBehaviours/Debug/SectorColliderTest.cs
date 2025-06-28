using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorColliderTest : MonoBehaviour
{
    public float radius = 3f;
    public float angle = 90f;
    public int segments = 20;
    public LayerMask targetLayer;

    private PolygonCollider2D sectorCollider;

    void Start()
    {
        GameObject sectorGO = new GameObject("SectorCollider");
        sectorGO.transform.position = transform.position;
        sectorGO.transform.parent = this.transform;

        sectorCollider = sectorGO.AddComponent<PolygonCollider2D>();
        sectorCollider.isTrigger = true;

        SectorColliderBuilder.CreateSectorCollider(
            sectorCollider,
            radius,
            angle,
            segments,
            transform.right
        );

        DetectEnemies();
    }

    void DetectEnemies()
    {
        Collider2D[] results = new Collider2D[20];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(targetLayer);
        filter.useLayerMask = true;
    
        int count = Physics2D.OverlapCollider(sectorCollider, filter, results);

        Debug.Log($"감지된 적 수: {count}");
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"적: {results[i].name}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        SectorColliderBuilder.DrawSectorGizmo(
            origin: transform.position,
            radius: radius,
            angle: angle,
            segments: segments,
            direction: transform.right,
            color: Color.green
        );
    }

}
