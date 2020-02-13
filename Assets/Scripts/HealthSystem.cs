using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float Health = 100f;
    public void Takedamage(float damage)
    {
        Health -= damage;
    }
}
