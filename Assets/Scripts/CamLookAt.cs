using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAt : MonoBehaviour
{
    public Transform cam;
    void Start()
    {
    }

    void Update()
    {
        transform.LookAt(cam.position);
    }
}
