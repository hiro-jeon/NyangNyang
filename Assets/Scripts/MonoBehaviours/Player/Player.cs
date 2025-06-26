using UnityEngine;
using System.Collections;

public class Player : Character
{
    public Stats stats;

    public StatsBar statsBarPrefab;
    public Shop shopPrefab;  
    public Inventory inventoryPrefab;

    public float maxExPoints;

    StatsBar statsBar;
    public Inventory inventory;

    private NewWeapon weapon;
    
    void Start()
    {
        weapon = GetComponentInChildren<NewWeapon>();
    }

    void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.LeftControl))
        {
            weapon.Attack(Vector2.right);
        }
    }   

    private void OnEnable()
    {
        ResetCharacter();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);
                        break ;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break ;
                    default:
                        break ;
                }
                collision.gameObject.SetActive(false);
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (stats.hitPoints < maxHitPoints)
        {
            stats.hitPoints = stats.hitPoints + amount;
            print("Adjusted hitpoints by: " + amount + ". New value: " + stats.hitPoints);
            return true;
        }
        return false;
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        // stats.hitPoints = stats.hitPoints - damage;

        while (true)
        {
            if (stats.hitPoints <= float.Epsilon)
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

    public void GetExp(int amount)
    {
        stats.exPoints += amount;
        if (stats.exPoints >= maxExPoints)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        stats.exPoints -= maxExPoints;
        Debug.Log("레벨업");
        stats.level++;
        maxExPoints = stats.level * stats.level * 16;
    }

    public override void KillCharacter()
    {
        gameObject.SetActive(false);
        // base.KillCharacter();

        Destroy(statsBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        maxExPoints = stats.level * stats.level * 16;

        inventory = Instantiate(inventoryPrefab);
        statsBar = Instantiate(statsBarPrefab);
        statsBar.character = this;

        stats.hitPoints = startingHitPoints;
    }
}