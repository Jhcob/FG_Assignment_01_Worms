using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent AI;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private float health = 100f;
    
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject _projectile;

    [SerializeField] private float forceForward = 32f, forceUp = 8f;
    
    //States
    public float attackRange;
    public bool  playerInAttackRange;

    private void Awake()
    {

    }

    private void Start()
    {
        AI = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player01_Gun").transform;

    }

    private void Update()
    {
        //Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (health > 0)
        {
            if (playerInAttackRange)
            {
                AttackPlayer();
            }
            else
            {
                Idle();
            }
        }
        else
        {
            Invoke(nameof(Dead), .5f);
        }

    }

    private void Idle()
    {
        // Idle Animation
        Debug.Log("AI idle");
    }
    
    private void AttackPlayer()
    {
        Debug.Log("AI attack");

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Atack code here
            Rigidbody rb = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * forceForward, ForceMode.Impulse);
            rb.AddForce(transform.up * forceUp, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    private void Dead()
    {
        //Dead animation
        Debug.Log("AI dead");
    }
    
    //Visual attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
