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
    public GameObject healthPrefab;

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
        float coinChance = 0.25f;
        if (Random.value < coinChance)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        float healthChance = 0.10f;
        if (Random.value < healthChance)
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.GetExp(exPoints);
        base.KillCharacter();
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0f)
            KillCharacter();
    }

    // 플레이어와 닿으면
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
}
