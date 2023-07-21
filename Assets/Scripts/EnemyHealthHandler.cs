using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public float health = 100;
    public int characterID;
    public Animator animator;
    
    public void Start()
    {
        EventManager.EnemyHit += TakeDamage;
    }
    public void TakeDamage(int damage, int id)
    {
        if (id == characterID)
        {
            health -= damage;
            if (health <= 0)
            {
                if(tag == "Player")
                {
                    animator.SetTrigger("isDead");
                    // end the level
                }
                else
                {
                    transform.GetComponent<RagdollHandler>().ActivateRagdoll();
                }
                
            }
            else
            {
                //animator.SetTrigger("isHit");
                Debug.Log(transform.name + " took " + damage + " damage");
            }
        }
    }
    public void OnDestroy()
    {
        EventManager.EnemyHit -= TakeDamage;
    }
}
