using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue; // Valor de la moneda

    public int GetValue()
    {
        return coinValue;
    }
}
