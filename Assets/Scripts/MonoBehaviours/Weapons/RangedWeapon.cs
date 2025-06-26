using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : NewWeapon
{
    // public float attackSpeed;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public GameObject movementPrefab;

    [Header("Stats")]
    public int projectileDamage;
    public float projectileSpeed;

    public override void Attack(Transform target)
    {
        GameObject projObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject moverObj = Instantiate(movementPrefab, projObj.transform);
        moverObj.transform.SetParent(projObj.transform, false);
        
        Projectile proj = projObj.GetComponent<Projectile>();
        Mover mover = moverObj.GetComponent<Mover>();

        if (mover != null)
        {
            mover.speed = projectileSpeed;
            mover.lifetime = 3f;
            mover.Fire(target);
        }
    }

    public override void Attack(Vector2 direction)   
    {
        Vector3 targetPosition = transform.position + (Vector3)direction.normalized * 5f;

        GameObject fakeTarget = new GameObject("FakeTarget");
        fakeTarget.transform.position = targetPosition;

        Attack(fakeTarget.transform);
        Destroy(fakeTarget, 1f);
    }
}
    
/*
    // 디버그용 ------------------------------------------

    public bool isAttacking = false;
    Coroutine attackCoroutine = null;

    public IEnumerator AttackRoutine()
    {
        while (true)
        {
            FireProjectile();
            yield return new WaitForSeconds(1.0f);
        }
    }
    
    void Start()
    {
        isAttacking = true;
    }

    void Update()
    {
        if (isAttacking)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            StopCoroutine(attackCoroutine);
        }
    }

    // --------------------------------------------------

*/