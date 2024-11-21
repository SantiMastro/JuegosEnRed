using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S0Manager : MonoBehaviour
{
    public ZombieStatsSO FastSO;
    public ZombieStatsSO TankSO;   
    public ZombieStatsSO NormalSO;
    void Start()
    {
        SetSCriptableObjectValues();
    }

    // Update is called once per frame
   private void SetSCriptableObjectValues()
    {
        FastSO.speed = 10;
        FastSO.damage = 10;
        FastSO.health = 100;
        FastSO.scoreValue = 75;

        TankSO.speed = 5f;
        TankSO.damage = 20;
        TankSO.health = 250;
        TankSO.scoreValue = 100;

        NormalSO.speed = 7.5f;
        NormalSO.damage = 15;
        NormalSO.health = 150;
        NormalSO.scoreValue = 50;
    }
}
