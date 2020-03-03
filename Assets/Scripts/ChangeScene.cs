using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public InventoryObject inventory;
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
        inventory.Container.Clear();
    }
    public IEnumerator triggerTransition(string scenename)
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(scenename);
    }
}
