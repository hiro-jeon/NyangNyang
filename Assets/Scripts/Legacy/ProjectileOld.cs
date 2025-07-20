using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileOld : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    private Vector3 startPosition;

    // private float maxDistance = 20f;
    private Rigidbody2D rb2d;


    public void Initialize(Vector3 targetPosition) // [ ] Initialize
    {
        startPosition = transform.position;
        Vector3 direction = (targetPosition - startPosition).normalized;
        rb2d = GetComponent<Rigidbody2D>(); // GetComponent 는 오브젝트에 위치한 걸 반환한다.
        rb2d.velocity = direction * speed;
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<Enemy>();
        if (damageable != null)
        {
            damageable.TakeDamage((int)damage);
        }
        Destroy(gameObject);
    }
}
