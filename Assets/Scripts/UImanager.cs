using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject Inventorytab;
    public GameObject[] obj;
    public GameObject pausemenu;
    public InventoryObject inventory;

    int i;
    void Start()
    {
        Inventorytab.SetActive(false);
    }
    public void DisplayFoodmenu(GameObject foodmenu)
    {
        bool active = foodmenu.activeSelf;
        foodmenu.SetActive(!active);
    }
    public void DisplayInfo(int i)
    {
      if(i==0)
        {
            bool active = obj[i].activeSelf;
            obj[i].SetActive(!active);
        }
      else if(i==1)
        {
            bool active = obj[i].activeSelf;
            obj[i].SetActive(!active);
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventorytab.SetActive(true);
            FindObjectOfType<AudioManager>().play("Inventory");
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Inventorytab.SetActive(false);
            FindObjectOfType<AudioManager>().play("Inventory");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            bool active = pausemenu.activeSelf;
            if(active)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            pausemenu.SetActive(!active);
        }
        if (inventory.Container.Count == 0)
        {
            inventory.weight = 0f;
        }
        if (inventory.weight < inventory.inventoryweight)
        {
            inventory.ispickable = true;
        }
        else if (inventory.weight == inventory.inventoryweight || inventory.weight > inventory.inventoryweight)
        {
            inventory.ispickable = false;
        }
    }   
    public void resume()
    {
        Time.timeScale = 1f;
        pausemenu.SetActive(false);
    }
    public void Mainmenu()
    {
        Time.timeScale = 1f;
        pausemenu.SetActive(false);
        ChangeScene.instance.SceneChangeTranstion("Main");
    }
    public void quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void PopupDisplay(GameObject pop)
    {
        pop.SetActive(true);
    }
    public void PopupInactive(GameObject pop)
    {
        pop.SetActive(false);
    }
}
