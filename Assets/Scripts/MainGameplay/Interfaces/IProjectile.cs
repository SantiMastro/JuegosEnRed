using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float Speed { get; }
    float LifeTime { get; }
    LayerMask HitteableLayer { get; }

    IGuns Owner { get; }

    void Init();
    void LaunchProjectile(Vector2 direction);
    void SetOwner(IGuns guns);
}
