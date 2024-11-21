using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuns
{
    Projectile Projectile { get; }
    int Damage { get; }
    float FireDelay { get; }

    void Shoot();
    void PointShoot();
}
