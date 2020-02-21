using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI Weight;
    public InventoryObject inventory;
    public Dictionary<InventorySlot, GameObject> DisplayedItem = new Dictionary<InventorySlot, GameObject>();
    
    private void Start()
    {
        CreateDisplay();
        inventory.onitemchangedcallback+=UpdateDisplay;
    }
    
    private void Update()
    {
        UpdateDisplay();
    }
    public void PopupFunction(GameObject popup)
    {
        popup.SetActive(true);
    }
    public void popclosefunction(GameObject popup)
    {
        popup.SetActive(false);
    }
   
    public void RemoveItem(Itemobject _item)
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.Itemname == _item.Itemname)
            {
                if (inventory.Container[i].amount == 0)
                {
                    inventory.Container.Remove(inventory.Container[i]);
                }
                else if (inventory.Container[i].amount > 0)
                {
                    inventory.Container[i].amount--;
                }
            }
        }
        inventory.weight -= _item.Weight;
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item == null)
            {
                GameObject obj = Instantiate(inventory.Container[i].item.ItemPrefabImage, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = "x" + inventory.Container[i].amount.ToString("n0");
                inventory.onitemchangedcallback();
            }
            else
            {
                inventory.Container[i].amount++;
            }
        }
    }
    public void UpdateDisplay()
    {
        Weight.text = "Weight - "+inventory.weight.ToString()+" / "+inventory.inventoryweight;
        
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (DisplayedItem.ContainsKey(inventory.Container[i]))
            {
                DisplayedItem[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = "x" + inventory.Container[i].amount.ToString("n0");
                if (inventory.Container[i].amount == 0)
                {
                    Destroy(DisplayedItem[inventory.Container[i]]);
                    inventory.Container.Remove(inventory.Container[i]);
                }
            }
            else
            {
                GameObject obj = Instantiate(inventory.Container[i].item.ItemPrefabImage, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = "x" + inventory.Container[i].amount.ToString("n0");
                DisplayedItem.Add(inventory.Container[i], obj);
            }
        }
    }
}
