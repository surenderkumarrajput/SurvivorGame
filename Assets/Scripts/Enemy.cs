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

    bool isFollow = false;
    bool ishit = false;


    HealthSystem healthsystem;

    public Image healthbar;

    public LayerMask playerlayer;

    Collider Enemycollider;
    public Collider BodyColliderDeath;

    public Transform Gizmosposition;

    NavMeshAgent navmesh;
    Animator anim;

    public GameObject health;
    void Start()
    {
        anim = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
        navmesh.stoppingDistance = 4.2f;
        healthsystem = GetComponent<HealthSystem>();
        Enemycollider = GetComponent<Collider>();
        BodyColliderDeath.enabled = false;
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
        }
       Collider[] collider = Physics.OverlapSphere(Gizmosposition.position,radius,playerlayer);
        var temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        foreach (var hit in collider)
        {
            if(hit.gameObject.CompareTag("Player"))
            {
                isFollow = true;
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
            }
        }
        if (ishit)
        {
            anim.SetTrigger("Attack");
        }
        if(Vector3.Distance(transform.position,temp.position)>AttackingDistance)
        {
            ishit = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Gizmosposition.position,radius);
        Gizmos.DrawWireSphere(Gizmosposition.position, AttackRadius);
    }
}
