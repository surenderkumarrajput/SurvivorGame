using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCycle : MonoBehaviour
{
    public TextMeshProUGUI DayCount;
    public float DayTimeinmin;
    float rotation;
    float elapsedtime=0f;
    float Count=1f;
    void Start()
    {
        rotation =  360/(DayTimeinmin*60);
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, rotation*Time.deltaTime);
        if(elapsedtime>DayTimeinmin*60)
        {
            Count++;
            elapsedtime = 0f;
        }
        else
        {
            elapsedtime += Time.deltaTime;
        }
        DayCount.text = "DAY  " + Count;
        if(Count==4)
        {
            StartCoroutine(scenechange());
        }
    }
    IEnumerator scenechange()
    {
        yield return new WaitForSeconds(1f);
        ChangeScene.instance.SceneChangeTranstion("GameFinished");
    }
}
   

