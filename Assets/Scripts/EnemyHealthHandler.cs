using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public float health = 100;
    public Animator animator;
    
    public void Start()
    {
        EventManager.EnemyHit += TakeDamage;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            transform.GetComponent<RagdollHandler>().ActivateRagdoll();
            Debug.Log("Enemy is dead");
        }
        else
        {
            //animator.SetTrigger("isHit");
            Debug.Log("Enemy took " + damage + " damage");
        }
    }
    public void OnDestroy()
    {
        EventManager.EnemyHit -= TakeDamage;
    }
}
