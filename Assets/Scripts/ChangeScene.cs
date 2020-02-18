using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    void Start()
    {
        instance = this;   
    }

    void Update()
    {
        
    }
    public void scene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
