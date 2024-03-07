using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PFeedBack : MonoBehaviour
{
    GameManager gm;
    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("DeadlyWall"))
        {
            gm.ReloadActiveScene();
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("GoalWall"))
        {
            Debug.Log("Win");
            gm.NextScene();
        }
    }
}
