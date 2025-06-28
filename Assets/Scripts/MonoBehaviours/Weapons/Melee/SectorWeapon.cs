using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorWeapon : Weapon
{
    [Header("Crescent")]
    public float innerRadius = 0.5f;
    public float outerRadius = 2.0f;

    public LayerMask enemyLayer;
    private Vector2 dir;

    public GameObject rendererPrefab;
    
    void ShowClawMarker(Vector3 position, float radius)
    {
        GameObject bombMarker = Instantiate(rendererPrefab, position, Quaternion.identity);
        CircleRenderer renderer = bombMarker.GetComponent<CircleRenderer>();

        renderer.Initialize(radius);
    }

    public override void Attack(Vector2 direction)
    {
        direction = direction.normalized;
        PerformCrescentAttack(direction);
    }

    public override void Attack(Transform target)
    {
        Debug.Log("Attack");
        if (target == null) return;
        Vector2 direction = (target.position - transform.position).normalized;
        PerformCrescentAttack(direction);
    }

    private void PerformCrescentAttack(Vector2 forward)
    {
        dir = forward; // Debug
        Vector2 origin = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin + forward * outerRadius, outerRadius, enemyLayer);
        Collider2D[] nonhits = Physics2D.OverlapCircleAll(origin + forward * innerRadius, innerRadius, enemyLayer);

        ShowClawMarker(origin + forward * outerRadius, outerRadius);
        ShowClawMarker(origin + forward * innerRadius, innerRadius);

        HashSet<Collider2D> nonhitSet = new HashSet<Collider2D>(nonhits);

        foreach (Collider2D hit in hits)
        {
            if (nonhitSet.Contains(hit)) continue ;

            if (hit != null)
            {
                DealDamage(hit.transform);
            }
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
}
