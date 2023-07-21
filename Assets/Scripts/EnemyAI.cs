using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    public float walkPointRange, attackTime;
    public bool walkPointSet, isPlayerInRange, isPlayerInAttackRange;
    public int id;
    public Transform player;
    public Animator animator;
    public NavMeshAgent agent;
    public EnemyHealthHandler enemyHealthHandler;
    public AttackController attackController;
    public Vector3 walkPoint;
    public LayerMask whatIsGround, whatIsPlayer;

    private void Update()
    {
        if (enemyHealthHandler.health > 50f)
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
        else if(enemyHealthHandler.health > 0)
        {
            ChasePlayer();
        }
        //else
        //{
        //    Debug.Log("Enemy is dead");
        //}
    }

    private void ChasePlayer()
    {
        if(!isPlayerInAttackRange)
        {
            agent.SetDestination(player.position);
        }
        else if(isPlayerInAttackRange && attackTime <= 0f)
        {
            agent.SetDestination(transform.position + new Vector3(0.1f, 0f, 0.1f));
            agent.isStopped = true;
            transform.DOLookAt(player.position, 0.5f);
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
            if(attackController.isDefending == true)
            {
                EventManager.OnEnemyHit(0, id);
            }
            else
            {
                EventManager.OnEnemyHit(10, id);
            }
            attackTime = 3f;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
        Vector3 distanceToPlayer = transform.position - player.position;
        if(distanceToPlayer.magnitude < 0.5f)
        {
            isPlayerInAttackRange = true;
        }
    }

    private void Patrol()
    {
        isPlayerInAttackRange = false;
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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            walkPointSet = true;
        }
    }
}
