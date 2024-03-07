using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttack : MonoBehaviour
{
    #region Attack Variables
    public bool isAttacking;
    public float ikinciSaldiriIcinKalanSure;
    public int comboCounter;
    int animComboCounter;
    float attackCoolDown;
    public Transform AttackPoint;
    public float attackRange;
    public LayerMask EnemyLayer;
    #endregion
    
    bool groundControl;
    bool crouchCheck;

    Animator animator;
    Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AttackAndAnimations();
    }

    void AttackAndAnimations()
    {
        groundControl = GetComponent<PMovement>().groundCheck;
        crouchCheck = GetComponent<PMovement>().isCrouched;
        animator.SetInteger("ComboCounter", animComboCounter);

        if(Input.GetKeyDown(KeyCode.Z) && ikinciSaldiriIcinKalanSure < 0.01 && attackCoolDown <= 0)
        {
            animComboCounter = 1;
            Attack();
            comboCounter = 1;
            ikinciSaldiriIcinKalanSure = 0.583f;
        }
        else if(Input.GetKeyDown(KeyCode.Z) && ikinciSaldiriIcinKalanSure > 0.01 && crouchCheck == false && groundControl && attackCoolDown <= 0)
        {
            comboCounter = 2;
        }
        

        

        if(ikinciSaldiriIcinKalanSure < 0.1 && comboCounter == 2 && animComboCounter == 1)
        {
            animComboCounter = 2;
            Attack();
            ikinciSaldiriIcinKalanSure = 0.417f;
        }

        if(ikinciSaldiriIcinKalanSure > 0 && groundControl)
        {
            ikinciSaldiriIcinKalanSure -= Time.deltaTime;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
        else if(groundControl)
        {
            comboCounter = 0;
            animComboCounter = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if(ikinciSaldiriIcinKalanSure > 0 && groundControl == false)
        {
            ikinciSaldiriIcinKalanSure -= Time.deltaTime;
            attackCoolDown = 0.7f;
        }
        else if(groundControl == false)
        {
            comboCounter = 0;
            animComboCounter = 0;
        }

        if(attackCoolDown > 0)
        {
            attackCoolDown -= Time.deltaTime;
        }
        

    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, EnemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.CompareTag("EnemyGreen"))
            {
                Debug.Log("We hit Green Zombie" );
                enemy.GetComponent<GreenZombie>().GotHit();
            }
            else
            {
                Debug.Log("We didn't hit an enemy");
            }
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
