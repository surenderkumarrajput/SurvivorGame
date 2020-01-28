﻿using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector3 movePos = Vector3.zero;
    public float speed;
    private Vector3 moveDir = Vector3.zero,moveforward;
    private bool canrun=false;
    private CharacterController characterController;
    private Animator animator;
    private bool canJump = false;
    public float jumpspeed = 20.0f;
    public float rotationSpeed = 30.0f;
    public float walkSpeed = 10.0f;
    public float runspeed = 10f;
    public Image HealthBar,hungerBar;
    private float dummyspeed;
    public InventoryObject inventory;
    public HungerSystem hunger;
    private HealthSystem healthsystem;
    public InventoryDisplay inventoryDisplay;
    public GameObject popup;
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
    }
   
    private void FixedUpdate()
    {

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
                    inventoryDisplay.popclosefunction(popup);
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
        healthsystem.Health = healthsystem.Player_Hunger(healthsystem.Health);
        speed = walkSpeed;
        canrun = false;
        canJump = false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed =runspeed;
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
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime, 0));
        moveDir.y -= 9.8f *Time.deltaTime;  //to make it drop
        characterController.Move(moveDir * speed * Time.deltaTime);
        var magnitude = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
        dummyspeed = magnitude;
        if (!canrun)
        {
            if(dummyspeed>0.5f)
            dummyspeed = 0.5f;
        }
        animator.SetFloat("speed", dummyspeed);
        animator.SetBool("Run", canrun);
        animator.SetBool("Jump", canJump);
    }
}
