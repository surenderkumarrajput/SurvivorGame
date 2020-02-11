using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    public Transform Destination;
    public GameObject Prefab;
    public Transform spawnpoint;
    void Start()
    {
        var navmesh = GetComponent<NavMeshAgent>();
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 90, 0);
        Instantiate(Prefab, spawnpoint.position, rotation);
        navmesh.SetDestination(Destination.position);
    }
    void Update()
    {
    }
}
