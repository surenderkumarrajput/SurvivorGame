using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHit : MonoBehaviour
{
    public string hitobjectname;
    public float DamagetoGive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(hitobjectname))
        {
            other.gameObject.GetComponent<HealthSystem>().Takedamage(DamagetoGive);
        }
    }
}
