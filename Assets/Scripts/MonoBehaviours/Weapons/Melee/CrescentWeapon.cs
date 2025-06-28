using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescentWeapon : Weapon
{
    [Header("Crescent")]
    public float innerRadius = 0.5f;
    public float outerRadius = 2.0f;

    private int segments = 20;
    public LayerMask targetLayer;

    // 콜라이더 
    GameObject crescentGO;

    PolygonCollider2D crescentCollider;
    ContactFilter2D filter;

    void Start()
    {
        crescentGO = new GameObject("crescentCollider");
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
        
        filter = new ContactFilter2D();
        filter.SetLayerMask(targetLayer);
        filter.useLayerMask = true;
    }

    private void PerformCrescentAttack()
    {
        Collider2D[] hits = new Collider2D[20];

        int count = Physics2D.OverlapCollider(crescentCollider, filter, hits);

        for (int i = 0; i < count; i++)
        {
            DealDamage(hits[i].transform);
        }
    }

    void DealDamage(Transform transform)
    {
        Enemy enemy = transform.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)damage);
        }
    }

    public override void Attack(Transform target)
    {
        if (target == null) return;
        Vector2 direction = (target.position - transform.position).normalized;

        Attack(direction);
    }

    public override void Attack(Vector2 direction)
    {
        direction = direction.normalized;

        Quaternion rotation = Quaternion.FromToRotation(crescentGO.transform.right, direction);
        crescentGO.transform.rotation = rotation;

        PerformCrescentAttack();
        ShowClawMarker((Vector2)transform.position + direction * outerRadius, outerRadius);
        ShowClawMarker((Vector2)transform.position + direction * innerRadius, innerRadius);
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

    // Debug --------------------------------------------------

    public GameObject rendererPrefab;
    
    void ShowClawMarker(Vector3 position, float radius)
    {
        GameObject bombMarker = Instantiate(rendererPrefab, position, Quaternion.identity);
        CircleRenderer renderer = bombMarker.GetComponent<CircleRenderer>();

        renderer.Initialize(radius);
    }

    // Debug --------------------------------------------------

}