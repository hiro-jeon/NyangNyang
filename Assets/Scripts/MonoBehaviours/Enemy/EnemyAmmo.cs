using UnityEngine;

public class EnemyAmmo : MonoBehaviour
{
    public int damageInflicted;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                StartCoroutine(player.DamageCharacter(damageInflicted, 0.0f));
                GameManager.sharedInstance.ReturnAmmo(this.gameObject);
            }
        }
    }
}