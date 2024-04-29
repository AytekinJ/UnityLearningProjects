using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        player = player == null ? GameObject.FindGameObjectWithTag("Player") : player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
