using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardWeapon : Weapon
{
    public LayerMask targetLayer;

    [Header("Bombard")]
    public float explosionRadius = 3f;

    // 디버그용 --------------------------------------------------

    public GameObject bombMarkerPrefab; 

    void ShowBombMarker(Vector3 position, float radius)
    {
        GameObject bombMarker = Instantiate(bombMarkerPrefab, position, Quaternion.identity);
        CircleRenderer renderer = bombMarker.GetComponent<CircleRenderer>();

        renderer.Initialize(radius);
    }

    // ---------------------------------------------------------

    void Start()
    {
        StartCoroutine(RepeatBombard());
    }

    public override void Attack(Transform target)
    {

    }

    public override void Attack(Vector2 direction)
    {

    }

    IEnumerator RepeatBombard()
    {
        while (true)
        {
            Bombard(range, damage);
            yield return new WaitForSeconds(1 / speed);
        }
    }

    public void Bombard(float dist, float maxDamage)
    {
        Vector3 bombPos = transform.position + (Vector3)Random.insideUnitCircle * dist;

        Collider2D[] targets = Physics2D.OverlapCircleAll(bombPos, explosionRadius, targetLayer);
        ShowBombMarker(bombPos, explosionRadius); // 디버그용

        foreach (var target in targets)
        {
            float distance = Vector2.Distance(bombPos, target.transform.position);
            float t = Mathf.Clamp01(1 - (distance / explosionRadius));
            float damage = maxDamage * t;
            target.GetComponent<Enemy>()?.TakeDamage((int)damage);
            // Debug.Log(damage + "의 데미지를 입혔습니다: " + target);
        }
    }

}