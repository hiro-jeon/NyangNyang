using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : NewWeapon
{
    [Header("Stats")]
    public float attackRange = 1f;

    public LayerMask enemyLayer;

    public override void Attack(Transform target)
    {
        if (target == null)
        {
            return ;
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {
            DealDamage(target);
        }
    }

    public override void Attack(Vector2 direction)
    {
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction. normalized, attackRange, enemyLayer);

        if (hit.collider != null)
        {
            DealDamage(hit.collider.transform);
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
