using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float radius;
    public float AttackRadius;
    float StopFollowDistance=20f;
    float AttackingDistance = 4f;
    float followradius = 8f;

    [HideInInspector]
    public bool isFollow = false;
    [HideInInspector]
    public bool ishit = false;


    HealthSystem healthsystem;
    EnergySystem energySystem;
    Player player;

    public Image healthbar;
    public Image EnergyBar;

    public LayerMask playerlayer;

    Collider Enemycollider;
    public Collider BodyColliderDeath;

    NavMeshAgent navmesh;

    public GameObject health;
    public GameObject deatheffect;
    public GameObject puncheffect;

    public Transform hips;
    public Transform AttackPart;
    public Transform Gizmosposition;


    public Collider punch;
    public Collider Attack2Collider;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
        healthsystem = GetComponent<HealthSystem>();
        Enemycollider = GetComponent<Collider>();
        BodyColliderDeath.enabled = false;
        energySystem = GetComponent<EnergySystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        punch.enabled = false;
    }


    void Update()
    {
        EnergyBar.fillAmount = energySystem.Energy / 100f;
        healthbar.fillAmount = healthsystem.Health / 100;
        if (healthsystem.Health <= 0)
        {
            anim.SetTrigger("Dead");
            BodyColliderDeath.enabled = true;
            ishit = false;
            navmesh.isStopped=true;
            Enemycollider.enabled = false;
            health.SetActive(false);
            Destroy(gameObject, 2f);
            
        }

        Collider[] collider = Physics.OverlapSphere(Gizmosposition.position,radius,playerlayer);
        var temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        foreach (var hit in collider)
        {
            if(hit.gameObject.CompareTag("Player"))
            {
                anim.SetTrigger("Entry");
            }
        }

        if (isFollow)
        {
            navmesh.SetDestination(temp.position);
            anim.SetFloat("Speed", 1f);
        }
        if (Vector3.Distance(transform.position, temp.position) > StopFollowDistance)
        {
            isFollow = false;
            anim.SetFloat("Speed", 0f);
            navmesh.ResetPath();
        }

        Collider[] attackhit = Physics.OverlapSphere(Gizmosposition.position, AttackRadius, playerlayer);
        foreach (var hit in attackhit)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                ishit = true;
                isFollow = true;
            }
        }
        if (ishit)
        {
            if(energySystem.Energy==100)
            {
                anim.SetTrigger("Attack2");
            }
            else
            {
                anim.SetTrigger("Attack");
            }
        }
        Collider[] follow = Physics.OverlapSphere(Gizmosposition.position, followradius, playerlayer);
        foreach (var hit in follow)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                isFollow = true;
            }
        }
        if (Vector3.Distance(transform.position,temp.position)>AttackingDistance)
        {
            ishit = false;
            anim.ResetTrigger("Attack");
            anim.ResetTrigger("Attack2");
        }
        if (player.Died == true)
        {
            ishit = false;
            isFollow = false;
            navmesh.isStopped = true;
            anim.SetFloat("Speed", 0f);
            anim.ResetTrigger("Attack");
            anim.ResetTrigger("Attack2");
        }
    }
    IEnumerator punchattackanim()
    {
        punch.enabled = true;
        yield return new WaitForSeconds(0.9f);
        punch.enabled = false;
    }
    IEnumerator Attack2()
    {
        Attack2Collider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        Attack2Collider.enabled = false;
    }
    public void punchanimeffect()
    {
        Instantiate(puncheffect, AttackPart.position, Quaternion.identity);
        energySystem.Energy -= 100;
        FindObjectOfType<AudioManager>().play("AttackEffect");
    }
    public void Deatheffect()
    {
        FindObjectOfType<AudioManager>().play("Die");
        Instantiate(deatheffect, hips.position, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Gizmosposition.position,radius);
        Gizmos.DrawWireSphere(Gizmosposition.position, AttackRadius);
    }
}
