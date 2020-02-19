using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public float Energy = 100f;
    public float rechargetime;
    void Start()
    {
        InvokeRepeating("energyIncrease", 5f, rechargetime);
    }

    void Update()
    {
    }
    public void energyIncrease()
    {
        if(Energy<100)
        {
            Energy = (Energy + 25);
            Energy=Mathf.Clamp(Energy, 0, 100);
        }
    }
}
