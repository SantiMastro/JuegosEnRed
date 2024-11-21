using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunsStats", menuName = "Stats/GunsStats", order = 0)]
public class GunsStats : ScriptableObject
{
    [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float FireDelay { get; private set; }

}
