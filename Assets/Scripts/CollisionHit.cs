using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHit : MonoBehaviour
{
    public string hitobjectname;

    public float DamagetoGive;

    public GameObject bloodeffect;

    Animator Playeranim;
    private void Start()
    {
        Playeranim=GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(hitobjectname))
        {
            other.gameObject.GetComponent<HealthSystem>().Takedamage(DamagetoGive);
            Instantiate(bloodeffect,transform.position,Quaternion.identity);
            FindObjectOfType<AudioManager>().play("Attack");
            if(other.gameObject.name=="Player")
            {
                Playeranim.SetTrigger("Hurt");
            }
            else
            {
                Playeranim.ResetTrigger("Hurt");
            }
        }
    }
}
