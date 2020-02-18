using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero,moveforward;

    private bool canrun=false;
    private bool canJump = false;
    [HideInInspector]
    public bool Died = false;

    private CharacterController characterController;
    private Animator animator;

    public float speed;
    public float jumpspeed = 20.0f;
    public float rotationSpeed = 30.0f;
    public float walkSpeed = 10.0f;
    public float runspeed = 10f;
    private float dummyspeed;

    public Image HealthBar, hungerBar;

    public InventoryObject inventory;
    HungerSystem hunger;
    HealthSystem healthsystem;
    public InventoryDisplay inventoryDisplay;

    public GameObject popup;
    public GameObject effects,puncheffects;

    public Collider kickcollider;
    public Collider punchcollider;

    public Transform punchtransform,kicktransform;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hunger = GetComponent<HungerSystem>();
        healthsystem = GetComponent<HealthSystem>();
    }
  
    void Start()
    {
        popup.SetActive(false);
        kickcollider.enabled = false;
        punchcollider.enabled = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
     {
         var obj = hit.gameObject.GetComponent<Item>();
             if(obj)
             {
                 inventoryDisplay.PopupFunction(popup);
                 if (Input.GetKeyDown(KeyCode.E))
                 {
                     inventory.AddItem(obj.item, 1);
                     Destroy(obj.gameObject);
                 }
             }
         else if(!obj)
         {
            inventoryDisplay.popclosefunction(popup);
         }
     }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
    public void Spawn(GameObject obj)
    {
        var go = Instantiate(obj as GameObject);
        var spawnpoint=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localPosition+(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().forward * 10);
        spawnpoint.y += 1000;
        var ray = new Ray(spawnpoint, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            spawnpoint.y = hit.point.y + go.transform.localScale.y * 0.5f;
        }
        go.transform.position = spawnpoint;

    }
    void Update()
    {
        hunger.Hunger = Mathf.Clamp(hunger.Hunger, 0, 100);
        hungerBar.fillAmount = hunger.Hunger / 100;
        healthsystem.Health = Mathf.Clamp(healthsystem.Health, 0, 100);
        HealthBar.fillAmount = healthsystem.Health / 100;
        healthsystem.Health = hunger.Player_Hunger(healthsystem.Health);
        speed = walkSpeed;
        canrun = false;
        canJump = false;
        if(healthsystem.Health==0)
        {
            StartCoroutine(DeathScenechange());
        }
      
        if(Died==false)
        {
            transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime, 0));
            moveDir.y -= 9.8f * Time.deltaTime;  //to make it drop
            characterController.Move(moveDir * speed * Time.deltaTime);
            var magnitude = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
            dummyspeed = magnitude;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runspeed;
                canrun = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                canrun = false;
            }
            if (characterController.isGrounded)
            {
                moveDir = new Vector3(0, 0, Input.GetAxis("Vertical"));
                moveDir = transform.TransformDirection(moveDir);
                moveDir *= speed;

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    canJump = true;
                    moveDir.y = jumpspeed;
                }
            }
            if (!canrun)
            {
                if (dummyspeed > 0.5f)
                    dummyspeed = 0.5f;
            }
            animator.SetFloat("speed", dummyspeed);
            animator.SetBool("Run", canrun);
            animator.SetBool("Jump", canJump);
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Kick");
            }
            else
            {
                animator.ResetTrigger("Kick");
            }
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Punch");
            }
            else
            {
                animator.ResetTrigger("Punch");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                int temp = Random.Range(1, 4);
                {
                    if (temp == 1)
                    {
                        animator.SetTrigger("Taunt1");
                    }
                    else if (temp == 2)
                    {
                        animator.SetTrigger("Taunt2");
                    }
                    else if (temp == 3)
                    {
                        animator.SetTrigger("Taunt3");
                    }
                }
            }
            else
            {
                animator.ResetTrigger("Taunt1");
                animator.ResetTrigger("Taunt2");
                animator.ResetTrigger("Taunt3");
            }
        }
    }
    IEnumerator kickCoroutine()
    {
     kickcollider.enabled = true;
     Instantiate(puncheffects, kicktransform.position, Quaternion.identity);
     yield return new WaitForSeconds(0.8f);
     kickcollider.enabled = false;
    }
    IEnumerator punchCoroutine()
    {
        punchcollider.enabled = true;
        Instantiate(puncheffects, punchtransform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        punchcollider.enabled = false;
    }
    IEnumerator DeathScenechange()
    {
        speed = 0f;
        animator.SetTrigger("Die");
        transform.rotation = Quaternion.identity;
        Died = true;
        yield return new WaitForSeconds(2f);
        ChangeScene.instance.scene("End");
    }
}
