using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Texture mainTexture;
    float saturation;
    Vector2 checkers;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && material.mainTexture != null) material.mainTexture = null;
        else if(Input.GetKeyDown(KeyCode.Space)) material.mainTexture = mainTexture;

        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            saturation = Mathf.Lerp(saturation, 1, Time.deltaTime);
            material.SetFloat("_Saturation", saturation);
        }
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            saturation = Mathf.Lerp(saturation, 0, Time.deltaTime);
            material.SetFloat("_Saturation", saturation);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            checkers = checkers.x < 10 ? new Vector2(checkers.x + 1, checkers.y + 1) : new Vector2(10, 10);
            material.SetFloat("_Checker1", checkers.x);
            material.SetFloat("_Checker2", checkers.y);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            checkers = checkers.x > 0 ? new Vector2(checkers.x - 1, checkers.y - 1) : new Vector2(0, 0);
            material.SetFloat("_Checker1", checkers.x);
            material.SetFloat("_Checker2", checkers.y);
        }

    }
}
