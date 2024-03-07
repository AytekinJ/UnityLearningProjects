using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenZombie : MonoBehaviour
{
    #region  Attacking Variables
    public Transform EAttackPoint;
    public float EAttackRange;
    public LayerMask EAttackLayer;
    bool isAttacking;
    float attackCoolDown;
    public Transform FollowingDistance;
    public Transform StayingPlace;
    GameObject TargetObject;
    #endregion

    float takingDamage;
    SpriteRenderer sr;
    Rigidbody2D rb;

    
    Animator animator;
    public int maxHealth = 3;
    int currentHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        

    }

    private void FixedUpdate() {
        EnemyMovement();
        EnemyAttacK();
        if(takingDamage > 0)
        {
            takingDamage -= Time.deltaTime;
        }
    }

    public void GotHit()
    {
        if(currentHealth >= 0)
        {
            animator.SetTrigger("GotHit");
            currentHealth--;
            takingDamage = 0.3f;
        }
        else
        {
            animator.SetBool("Died", true);
            Destroy(gameObject, 0.7f);
        }
    }

    void EnemyAttacK()
    {
        if(attackCoolDown <= 0)
        {
            Collider2D[] hitTarget = Physics2D.OverlapCircleAll(EAttackPoint.position, EAttackRange, EAttackLayer);
            foreach(Collider2D enemy in hitTarget)
            {
                if(enemy.CompareTag("Player"))
                {
                    Debug.Log("Hit Player" );
                    animator.SetTrigger("isAttacking");
                    attackCoolDown = 0.5f;
                }
            }
        }
        else
        {
            attackCoolDown -= Time.deltaTime;
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(EAttackPoint.position, EAttackRange);
    }
    
    void EnemyMovement()
    {
        TargetObject = GameObject.FindGameObjectWithTag("Player");
        
        // animator.SetBool("isAttaking", isAttacking);
        if(attackCoolDown <= 0 && takingDamage <= 0)
        {
            if(FollowingDistance.position.x + 5 > TargetObject.transform.position.x && TargetObject.transform.position.x > FollowingDistance.position.x)
            {
                transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
                animator.SetFloat("Horizontal", 1f);
                sr.flipX = true;
            }
            else if(FollowingDistance.position.x - 5 < TargetObject.transform.position.x && TargetObject.transform.position.x < FollowingDistance.position.x)
            {
                transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
                animator.SetFloat("Horizontal", 1f);
                sr.flipX = false;
            }
            else
            {
                if(transform.position.x > StayingPlace.position.x + 1)
                {
                    transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
                    animator.SetFloat("Horizontal", 1f);
                    sr.flipX = false;
                }
                else if(transform.position.x < StayingPlace.position.x -1)
                {
                    transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
                    animator.SetFloat("Horizontal", 1f);
                    sr.flipX = true;
                }
                else
                {
                    animator.SetFloat("Horizontal", 0);
                }
            }
        }
        
    }
}
