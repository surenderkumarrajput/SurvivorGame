using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float radius;
    public float AttackRadius;
    float StopFollowDistance=20f;
    float AttackingDistance = 4f;

    bool isFollow = false;
    bool ishit = false;

    public LayerMask playerlayer;

    NavMeshAgent navmesh;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
       Collider[] collider = Physics.OverlapSphere(transform.position,radius,playerlayer);
       Collider[] attackhit = Physics.OverlapSphere(transform.position, AttackRadius, playerlayer);
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
        Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}
