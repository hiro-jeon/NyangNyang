using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover : Mover
{
   private Projectile projectile;

   public override void Fire(Transform target)
   {
      projectile = transform.parent.GetComponent<Projectile>();
      projectile.direction = (target.position - transform.parent.position).normalized;
      Destroy(projectile.gameObject, lifetime);
   }

   void Update()
   {
      if (projectile != null)
      {
         projectile.transform.position += (Vector3)(projectile.direction * speed * Time.deltaTime);
      }
      else
      {
         Destroy(projectile.gameObject, lifetime);
      }
   }
}