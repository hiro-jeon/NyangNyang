using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescentColliderTest : MonoBehaviour
{
    public float innerRadius = 1f;
    public float outerRadius = 3f;
    public int segments = 30;
    public LayerMask targetLayer;

    private PolygonCollider2D crescentCollider;

    void Start()
    {
        GameObject crescentGO = new GameObject("crescentCollider");
        crescentGO.transform.position = transform.position;
        crescentGO.transform.parent = this.transform;

        crescentCollider = crescentGO.AddComponent<PolygonCollider2D>();
        crescentCollider.isTrigger = true;

        CrescentColliderBuilder.CreateCresentCollider(
            crescentCollider,
            innerRadius,
            outerRadius,
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
    
        int count = Physics2D.OverlapCollider(crescentCollider, filter, results);

        Debug.Log($"감지된 적 수: {count}");
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"적: {results[i].name}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        CrescentColliderBuilder.DrawCrescentGizmo(
            origin: transform.position,
            innerRadius: innerRadius,
            outerRadius: outerRadius,
            segments: segments,
            direction: transform.right,
            color: Color.green
        );
    }

}
