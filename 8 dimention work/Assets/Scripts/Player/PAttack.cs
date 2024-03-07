using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttack : MonoBehaviour
{
    float comboTimer;
    public int comboCounter;
    int attackCounter;

    PMovement pMovement;
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        pMovement = GetComponent<PMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputAndAnimator();
    }

    void InputAndAnimator()
    {
        if(Input.GetKeyDown(KeyCode.Z) && comboCounter < 3)
        {
            Debug.Log("Hit");
            comboCounter++;
            animator.SetInteger("AttackCount", comboCounter);
            comboTimer = 0.333f;
        }

        if(comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else if(comboTimer <= 0 && comboCounter > 0)
        {
            animator.SetInteger("AttackCount", 0);
            comboCounter = 0;
        }
        
        



    }
}
