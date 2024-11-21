using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Guns
{
    public override void Shoot()
    {
        if (Time.time > lastFire + FireDelay)
        {
            Projectile projectile = Instantiate(Projectile, _firePoint.position, transform.rotation);
            projectile.LaunchProjectile(transform.right);
            projectile.SetOwner(this);

            lastFire = Time.time;
        }
    }
}
