using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy : MonoBehaviour
{
    public GameObject goTarget;
    public GameObject prefProjectile;
    private ObjectTracker objectTracker;

    public float fTimeTilTarget = 1.2f;
    public bool bUseConstantSpeed = false;
    public float fSpeed = 20;
    public int iMaxIterations = 100;
    public float fBaseCheckTime = 0.15f;
    private float fTimePerCheck = 0.05f;
    
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    [SerializeField] private float health = 1f;

    public float attackRange;
    public bool  playerInAttackRange;
    [SerializeField] private LayerMask whatIsPlayer;
    
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Animator animRoaster;
    
    [SerializeField] private TurnManager turnManager;

    [SerializeField] public ActivePlayer player01;
    private ActivePlayer currentPlayer;
    
    private float fDistance = 9999;

    
    // Start is called before the first frame update
    void Start()
    {
        objectTracker = GetComponent<ObjectTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        
        if (health > 0 )
        {
            if (playerInAttackRange &&  turnManager.TurnNumber()%2 == 0)
            {
                animRoaster.GetComponent<Animator>().SetBool("isAttacking", true);
                AttackPlayer();
            }
            else
            {
                animRoaster.GetComponent<Animator>().SetBool("isAttacking", false);
            }
        }
        else
        {
            animRoaster.GetComponent<Animator>().SetBool("isDead", true);
        }
        this.transform.LookAt(goTarget.transform.position);
    }


    private void AttackPlayer()
    {
        if (!alreadyAttacked && turnManager.TurnNumber()%2 == 0)
        {
            Debug.Log("AI attack player 01!");

            if (bUseConstantSpeed)
            {
                int iIterations = 0;
                AI_BulletController bullet = GameObject.Instantiate(prefProjectile, bulletSpawn.position, transform.rotation).GetComponent<AI_BulletController>();

                float fCheckTime = fBaseCheckTime;
                Vector3 v3TargetPosition = objectTracker.GetProjectedPosition(fBaseCheckTime);
                Debug.DrawLine(this.transform.position, v3TargetPosition, Color.red, 1);

                //Predict projectile position
                Vector3 v3PredictedProjectilePosition = this.transform.position + ((v3TargetPosition - this.transform.position).normalized * fSpeed * fCheckTime);
                Debug.DrawLine(this.transform.position, v3PredictedProjectilePosition, Color.green, 3);
                fDistance = (v3TargetPosition - v3PredictedProjectilePosition).magnitude;

                while (fDistance > 1.5f && iIterations < iMaxIterations)
                {
                    iIterations++;
                    fCheckTime += fTimePerCheck;
                    v3TargetPosition = objectTracker.GetProjectedPosition(fCheckTime);
                    Debug.DrawLine(goTarget.transform.position, v3TargetPosition, Color.red, 3);

                    v3PredictedProjectilePosition = this.transform.position + ((v3TargetPosition - this.transform.position).normalized * fSpeed * fCheckTime);
                    Debug.DrawLine(this.transform.position, v3PredictedProjectilePosition, Color.green, 3);
                    fDistance = (v3TargetPosition - v3PredictedProjectilePosition).magnitude;
                }

                Vector3 v3Velocity = v3TargetPosition - this.transform.position;
                bullet.Shoot(v3Velocity.normalized, fSpeed);
            }
            else
            {
                Vector3 v3TargetPosition = objectTracker.GetProjectedPosition(fTimeTilTarget);
                Debug.DrawLine(this.transform.position, v3TargetPosition, Color.red, 1);
                AI_BulletController bullet = GameObject.Instantiate(prefProjectile, this.transform.position, transform.rotation).GetComponent<AI_BulletController>();
                Vector3 v3Velocity = v3TargetPosition - this.transform.position;
                float fVelocity = v3Velocity.magnitude / fTimeTilTarget;
                bullet.Shoot(v3Velocity.normalized, fVelocity);
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
        //animRoaster.GetComponent<Animator>().SetBool("isAttacking", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }


    //Visual attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

