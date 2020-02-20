using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Animator anim;
    public static ChangeScene instance;
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void SceneChangeTranstion(string scenename)
    {
        StartCoroutine(triggerTransition(scenename));
    }
    public IEnumerator triggerTransition(string scenename)
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(scenename);
    }
}
