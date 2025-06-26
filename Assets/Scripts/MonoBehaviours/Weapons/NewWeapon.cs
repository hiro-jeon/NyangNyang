using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewWeapon : MonoBehaviour
{
    public float attackSpeed;
    public float damage;

    public abstract void Attack(Transform target);
    public abstract void Attack(Vector2 direction);
}
