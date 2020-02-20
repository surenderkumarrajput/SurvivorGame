using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public string Speakername;
    [TextArea(0,3)]
    public string[] dialogues;

    public GameObject ContinueButton;

    public TextMeshProUGUI text;

    int index=0;
    void Start()
    {
        StartCoroutine(DialogueDelay());
        ContinueButton.SetActive(false);
    }
    IEnumerator DialogueDelay()
    {
        FindObjectOfType<AudioManager>().play("Narrator");
        foreach (char item in dialogues[index].ToCharArray())
        {
            text.text += item;
            yield return new WaitForSeconds(0.05f);
        }
        FindObjectOfType<AudioManager>().stop("Narrator");
    }
    public void Continue()
    {
        ContinueButton.SetActive(false);
        if (index < dialogues.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(DialogueDelay());
        }
        else
        {
            text.text = "";
            ChangeScene.instance.SceneChangeTranstion("Survivor");
        }
    }
    void Update()
    {
        if(text.text==dialogues[index])
        {
            ContinueButton.SetActive(true);
        }
    }
}
