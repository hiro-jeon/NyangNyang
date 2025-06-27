using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewWeapon : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float damage;
    public float range;

    public abstract void Attack(Transform target);
    public abstract void Attack(Vector2 direction);
}
