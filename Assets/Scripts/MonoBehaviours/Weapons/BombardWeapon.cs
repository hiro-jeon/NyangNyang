using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardWeapon : MonoBehaviour
{
    public LayerMask targetLayer;
    public float explosionRadius = 3f;
    public float distance = 5f;
    public float damage = 2f;

    public float bombardInterval = 3f;

    // 디버그용
    public GameObject bombMarkerPrefab; 

    void Start()
    {
        StartCoroutine(RepeatBombard());
    }

    IEnumerator RepeatBombard()
    {
        while (true)
        {
            Bombard(distance, damage);
            yield return new WaitForSeconds(bombardInterval);
        }
    }


    public void Bombard(float dist, float maxDamage)
    {
        Vector3 bombPos = transform.position + (Vector3)Random.insideUnitCircle * dist;

        Collider2D[] targets = Physics2D.OverlapCircleAll(bombPos, explosionRadius, targetLayer);
        ShowBombMarker(bombPos, dist);

        foreach (var target in targets)
        {
            float distance = Vector2.Distance(bombPos, target.transform.position);
            float t = Mathf.Clamp01(1 - (distance / explosionRadius));
            float damage = maxDamage * t;
            target.GetComponent<Enemy>()?.TakeDamage((int)damage);
            // Debug.Log(damage + "의 데미지를 입혔습니다: " + target);
        }
    }

    void ShowBombMarker(Vector3 position, float range)
    {
        GameObject bombMarker = Instantiate(bombMarkerPrefab, position, Quaternion.identity);
    }
}
