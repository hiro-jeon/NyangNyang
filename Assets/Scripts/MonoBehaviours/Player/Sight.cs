using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    NewWeapon weapon;

    private Coroutine attackCoroutine;
    private Transform currentTarget;

    private List<Transform> enemiesInRange = new List<Transform>();

    void Start()
    {
        weapon = transform.parent.GetChild(0).GetComponent<NewWeapon>();
    }

    void Update()
    {
        TryUpdateTarget();
        if (enemiesInRange.Count != 0)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackRoutine());
            }
        }
        else if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.transform);
        }

    }
    
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (currentTarget != null)
            {
                weapon.Attack(currentTarget);
                yield return new WaitForSeconds(weapon.attackSpeed);
            }
        }
    }

    void TryUpdateTarget()
    {
        Transform closest = null;
        float closestDist = Mathf.Infinity;

        if (enemiesInRange.Count == 0)
        {
            currentTarget = null;
        }

        foreach (Transform enemy in enemiesInRange)
        {
            float dist = Vector2.Distance(transform.position, enemy.position);
            if (dist < closestDist)
            {
                closest = enemy;
                closestDist = dist;
            }
        }
        currentTarget = closest;
    }
}
