using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector]
    public static InventoryManager instance;
    [HideInInspector]
    public Itemobject item;
    public RawImage RenderingImage;
    public bool selected=false;
    public TextMeshProUGUI description,Itemname;
    public GameObject InfoButton,Usebutton,deleteButton,useeffect;
    public Player player;
    public InventoryDisplay InventoryDisplay;
    public Transform playertransform;
    void Start()
    {
        description.text = "";
        Itemname.text = "";
        instance = this;
        item = null;
        InfoButton.SetActive(false);
        Usebutton.SetActive(false);
        deleteButton.SetActive(false);
    }

    void Update()
    {
       if(selected==true)
        {
            InfoButton.SetActive(true);
            Usebutton.SetActive(true);
            deleteButton.SetActive(true);
        }
        else
        {
            InfoButton.SetActive(false);
            Usebutton.SetActive(false);
            deleteButton.SetActive(false);
        }
    }
    public void Show()
    {
        if (selected == true)
        {
            description.text = "Description:-\n" + item.Description;
            Itemname.text = item.name;
            RenderingImage.texture = item.renderImage.texture;
            selected = false;
        }
        else
        {
            description.text = "";
            Itemname.text ="";
            RenderingImage.texture = null;
        }
    }
    public void UseButton()
    {
        if(selected==true)
        {
            item.Use(item);
            StartCoroutine(playUsesound());
            selected = false;
        }
    }
    public void DeleteItems()
    {
        if(selected==true)
        {
            player.Spawn(item.ItemGameobject);
            InventoryDisplay.RemoveItem(item);
            selected = false;
        }
    }
    IEnumerator playUsesound()
    {
        FindObjectOfType<AudioManager>().play("Use");
        yield return new WaitForSeconds(0.6f);
        Instantiate(useeffect, playertransform.position, Quaternion.identity);
    }
}
