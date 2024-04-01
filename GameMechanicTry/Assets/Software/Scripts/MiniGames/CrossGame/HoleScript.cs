using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    float crossRotation;
    float holeRotation;
    bool crossLocked;
    CrossGameManager gm;
    Rigidbody2D crossRb;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<CrossGameManager>();
        holeRotation = (int)transform.eulerAngles.z;
        Debug.Log(gameObject.name + "'s rotation is: " + holeRotation + " and euler angle z is: " + gameObject.transform.eulerAngles.z);
    }
    private void OnTriggerEnter2D(Collider2D other) {

        crossRotation = (int)other.gameObject.transform.eulerAngles.z;
        crossRb = other.gameObject.GetComponent<Rigidbody2D>();
        

        if(holeRotation == 0)
        {
            if(other.gameObject.CompareTag("BigCross"))
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = crossRotation == holeRotation ? new Vector3(transform.position.x, transform.position.y + 3.5f) : new Vector3(transform.position.x, transform.position.y + 1.5f);
                gm.CrossLocked();
                crossLocked = true;
            }
            else if(other.gameObject.CompareTag("SmallCross") && crossRotation == holeRotation)
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1);
                gm.CrossLocked();
                crossLocked = true;
            }
        }
        else if(holeRotation == 90)
        {
            if(other.gameObject.CompareTag("BigCross"))
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = crossRotation == holeRotation ? new Vector3(transform.position.x - 3.5f, transform.position.y) : new Vector3(transform.position.x - 1.5f, transform.position.y);
                gm.CrossLocked();
                crossLocked = true;
            }
            else if(other.gameObject.CompareTag("SmallCross") && crossRotation == holeRotation)
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y);
                gm.CrossLocked();
                crossLocked = true;
            }
        }
        else if (holeRotation == 180)
        {
            if (other.gameObject.CompareTag("BigCross"))
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = crossRotation == holeRotation ? new Vector3(transform.position.x, transform.position.y - 3.5f) : new Vector3(transform.position.x, transform.position.y - 1.5f);
                gm.CrossLocked();
                crossLocked = true;
            }
            else if (other.gameObject.CompareTag("SmallCross") && crossRotation == holeRotation)
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 1);
                gm.CrossLocked();
                crossLocked = true;
            }
        }
        else if (holeRotation == 270)
        {
            if (other.gameObject.CompareTag("BigCross"))
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = crossRotation == holeRotation ? new Vector3(transform.position.x + 3.5f, transform.position.y) : new Vector3(transform.position.x + 1.5f, transform.position.y);
                gm.CrossLocked();
                crossLocked = true;
            }
            else if (other.gameObject.CompareTag("SmallCross") && crossRotation == holeRotation)
            {
                crossRb.constraints = RigidbodyConstraints2D.FreezeAll;
                other.gameObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y);
                gm.CrossLocked();
                crossLocked = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(crossLocked && other.gameObject.CompareTag("BigCross") || other.gameObject.CompareTag("SmallCross"))
        {
            gm.CrossRelased();
            crossLocked = false;
        }

    }

}   