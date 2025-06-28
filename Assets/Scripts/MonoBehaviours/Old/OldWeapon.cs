using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class OldWeapon : MonoBehaviour
{
    public GameObject ammoPrefab;

    static List<GameObject> ammoPool;
    public int poolSize;
    public float weaponVelocity;

    void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FireAmmo(mousePosition);
        }
    }


    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    void FireAmmo(Vector3 position)
    {
        GameObject ammo = SpawnAmmo(transform.position);

        if (ammo != null)
        {
            AmmoPhysics straightScript = ammo.GetComponent<AmmoPhysics>();
            float travelDuration = 1.0f / weaponVelocity;
            StartCoroutine(straightScript.TravelAmmo(position, travelDuration));
        }
    }

    void OnDestroy()
    {
        ammoPool = null;
    }

}
