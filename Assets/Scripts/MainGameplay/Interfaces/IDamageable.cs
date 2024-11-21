using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHealt { get; }
    int CurrentHealth { get; }

    void TakeDamage(int damage);
    void Die();
}
