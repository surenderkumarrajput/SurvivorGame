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
    Player player;

    public Image healthbar;

    public LayerMask playerlayer;

    Collider Enemycollider;
    public Collider BodyColliderDeath;

    public Transform Gizmosposition;

    NavMeshAgent navmesh;

    public GameObject EnemyTag;
    public GameObject health;
    public GameObject BloodSplash;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
        navmesh.stoppingDistance = 4.2f;
        healthsystem = GetComponent<HealthSystem>();
        Enemycollider = GetComponent<Collider>();
        BodyColliderDeath.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    void Update()
    {
        healthbar.fillAmount = healthsystem.Health / 100;
        if (healthsystem.Health == 0)
        {
            anim.SetTrigger("Dead");
            BodyColliderDeath.enabled = true;
            ishit = false;
            navmesh.isStopped=true;
            Enemycollider.enabled = false;
            health.SetActive(false);
            EnemyTag.SetActive(false);
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
            anim.SetTrigger("Attack");
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
        }
        if (player.Died == true)
        {
            ishit = false;
            isFollow = false;
            navmesh.isStopped = true;
            anim.SetFloat("Speed", 0f);
            anim.ResetTrigger("Attack");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Gizmosposition.position,radius);
        Gizmos.DrawWireSphere(Gizmosposition.position, AttackRadius);
    }
}
