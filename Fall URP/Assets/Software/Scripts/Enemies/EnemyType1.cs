using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    GameObject Player;
    void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            if(Mathf.Abs(transform.position.y) - Mathf.Abs(Player.transform.position.y) < 10)
            {
                if(Player.transform.position.x > transform.position.x)
                {
                    transform.position += new Vector3(4f * Time.deltaTime, 0);
                }
                else if(Player.transform.position.x < transform.position.x)
                {
                    transform.position -= new Vector3(4f * Time.deltaTime, 0);
                }
            }

        }
    }
}
