using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject ammoPrefab;

    public float weaponVelocity;
    public bool attackPlayer;
    public float interval;
    public float range;
    Coroutine fireCoroutine;
    GameObject ammo;

   public IEnumerator FireAmmo(GameObject playerObject)
    {

        while (true)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = currentPosition + (playerObject.transform.position - currentPosition).normalized * range;
            ammo = GameManager.sharedInstance.GetAmmo();

            if (ammo != null)
            {
                ammo.transform.position = currentPosition;
                AmmoPhysics straightScript = ammo.GetComponent<AmmoPhysics>();
                float travelDuration = 1.0f / weaponVelocity;
                StartCoroutine(straightScript.TravelAmmo(targetPosition, travelDuration));
            }
            yield return new WaitForSeconds(interval);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackPlayer)
        {
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
            fireCoroutine = StartCoroutine(FireAmmo(collision.gameObject));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackPlayer)
        {
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
        }
    }

    void OnDestroy()
    {
        if (ammo != null)
        {
            ammo.SetActive(false);
        }
    }
}
