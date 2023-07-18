using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float walkPointRange, attackTime;
    public bool walkPointSet, isPlayerInRange, isPlayerInAttackRange;
    public Transform player;
    public Animator animator;
    public NavMeshAgent agent;
    public Vector3 walkPoint;
    public LayerMask whatIsGround, whatIsPlayer;
    public EnemyHealthHandler enemyHealthHandler;

    private void Update()
    {
        if (enemyHealthHandler.health > 0f)
        {
            isPlayerInRange = Physics.CheckSphere(transform.position, walkPointRange, whatIsPlayer);
            if (!isPlayerInRange)
            {
                Patrol();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        if(!isPlayerInAttackRange)
        {
            agent.SetDestination(player.position);
        }
        else if(isPlayerInAttackRange && attackTime <= 0f)
        {
            agent.isStopped = true;
            var attackType = Random.Range(0, 2);
            switch (attackType)
            {
                case 0:
                    animator.SetTrigger("PerformLeftAttack");
                    break;
                case 1:

                    animator.SetTrigger("PerformRightAttack");
                    break;
                default:
                    break;
            }
            attackTime = 3f;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
        Vector3 distanceToPlayer = transform.position - player.position;
        if(distanceToPlayer.magnitude < 1f)
        {
            isPlayerInAttackRange = true;
        }
    }

    private void Patrol()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("isWalking", true);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 0.5f)
        {
            walkPointSet = false;
        }

    }
    public void SearchWalkPoint() { 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;
    }
}
