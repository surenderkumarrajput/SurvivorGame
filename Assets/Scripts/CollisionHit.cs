using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHit : MonoBehaviour
{
    public string hitobjectname;
    public float DamagetoGive;
    public GameObject bloodeffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(hitobjectname))
        {
            other.gameObject.GetComponent<HealthSystem>().Takedamage(DamagetoGive);
            GameObject instance=Instantiate(bloodeffect,transform.position,Quaternion.identity);
            Destroy(instance, 0.5f);
            FindObjectOfType<AudioManager>().play("Attack");
        }
    }
}
