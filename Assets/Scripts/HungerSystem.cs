using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    public float Hunger=0;
    public float HungerIncreaseFactor=10;
    private float ElapsedTime = 0f, FixedTime = 8f;
    public float Player_Hunger(float health)
    {
        Hunger = Mathf.Clamp(Hunger, 0, 100);
        if (ElapsedTime > FixedTime)
        {
            Hunger += HungerIncreaseFactor;
            ElapsedTime = 0f;
            if (Hunger >= 40f)
            {
                health -= 20f;
            }
        }
        else
        {
            ElapsedTime += Time.deltaTime;
        }
        return health;
    }

}



