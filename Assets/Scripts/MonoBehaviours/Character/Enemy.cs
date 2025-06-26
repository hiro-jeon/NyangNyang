using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;
    public int exPoints;
    public int damageStrength;
    Coroutine damageCoroutine;
    public GameObject coinPrefab;

    public Player player;

    private void OnEnable()
    {
        ResetCharacter();
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints -= damage;
            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break ; 
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break ;
            }
        }
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    public override void KillCharacter()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        player.GetExp(exPoints);
        base.KillCharacter();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null && damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        Debug.Log("폭격으로 데미지: " + damage);
        if (hitPoints <= 0f)
            KillCharacter();
    }
}
