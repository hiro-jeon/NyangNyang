using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public LayerMask enemyLayer;

    private Coroutine attackRoutine;
    private Transform currentTarget;
    private Weapon weapon;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        Auto();
    }

    void Auto()
    {
        if (weapon == null)
        {
            weapon = GetComponentInChildren<Weapon>();
            return ;
        }

        if (currentTarget == null || !IsTargetValid(currentTarget))
        {
            Transform newTarget = FindClosestEnemyInRange();

            if (newTarget != currentTarget)
            {
                currentTarget = newTarget;

                if (attackRoutine == null && currentTarget != null)
                {
                    attackRoutine = StartCoroutine(AttackLoop());
                }
            }
        }
    }
    
    Transform FindClosestEnemyInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, weapon.range, enemyLayer);

        Transform closest = null;
        float closestDist = Mathf.Infinity;
        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDist)
            {
                closest = hit.transform;
                closestDist = dist;
            }
        }
        return (closest);
    }    

    bool IsTargetValid(Transform target)
    {
        if (target == null) return false;

        float dist = Vector2.Distance(transform.position, target.position);
        if (dist > weapon.range) return false;

        return (true);
    }

    IEnumerator AttackLoop()
    {
        while (currentTarget != null && IsTargetValid(currentTarget))
        {
            weapon.Attack(currentTarget);
            yield return new WaitForSeconds(1 / weapon.speed);
        }
        currentTarget = null;
        attackRoutine = null;
    }
}