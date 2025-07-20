using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public GameObject movementPrefab;

    [Header("Projectile Stats")]
    public float projectileSpeed;

    public override void Attack(Vector2 direction)   
    {
        Vector3 targetPosition = transform.position + (Vector3)direction.normalized * 5f;

        GameObject fakeTarget = new GameObject("FakeTarget");
        fakeTarget.transform.position = targetPosition;

        Attack(fakeTarget.transform);
        Destroy(fakeTarget, 1f);
    }

    public override void Attack(Transform target)
    {
        GameObject projObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile proj = projObj.GetComponent<Projectile>();

        GameObject moverObj = Instantiate(movementPrefab, projObj.transform);
        Mover mover = moverObj.GetComponent<Mover>();
        moverObj.transform.SetParent(projObj.transform, false);
        

        if (proj != null)
        {
            proj.damage = damage;
        }

        if (mover != null)
        {
            mover.speed = projectileSpeed;
            mover.lifetime = 3f;
            mover.Fire(target);
        }
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