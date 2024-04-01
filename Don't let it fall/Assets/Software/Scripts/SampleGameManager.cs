using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGameManager : MonoBehaviour
{
    [SerializeField] public LayerMask ObjectLayer;
    GameObject[] gameObjects;
    void Start()
    {
        
    }

    public void ReverseGravity()
    {
        gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject go in gameObjects){

            if(go.name!="_s" && go.layer == ObjectLayer)
            {
                Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
                rb2d.gravityScale *= -1;
            }
        }
    } 

    void Update()
    {
        
    }
}
