using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public float DayTimeinmin;
    float rotation;
    void Start()
    {
        rotation =  360/(DayTimeinmin*60);
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, rotation*Time.deltaTime);
    }
    }
   

