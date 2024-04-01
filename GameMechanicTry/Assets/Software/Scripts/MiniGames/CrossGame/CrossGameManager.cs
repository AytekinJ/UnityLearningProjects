using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrossGameManager : MonoBehaviour
{
    [SerializeField] private KeyCode RotationKey = KeyCode.R;

    int holeNumbers;
    int lockedCross;
    bool isDragging;
    bool levelPassed;
    GameObject DragObject;
    [SerializeField] private GameObject LevelText;
    [SerializeField] private GameObject ProgressText;
    [SerializeField] private GameObject WinText;
    Camera mainCamera;

    void Start()
    {
        ProgressText.gameObject.SetActive(true);

        mainCamera = Camera.main;

        holeNumbers = GameObject.FindGameObjectsWithTag("Hole").Length;

        LevelText = LevelText == null ? GameObject.FindGameObjectWithTag("LevelName") : LevelText;
        ProgressText = ProgressText == null ? GameObject.FindGameObjectWithTag("Progress") : ProgressText;
        WinText = WinText == null ? GameObject.FindGameObjectWithTag("Win") : WinText;

        WinText.SetActive(false);
        levelPassed = false;

        LevelText.GetComponent<Text>().text = " " + SceneManager.GetActiveScene().name;
        ProgressText.GetComponent<Text>().text = holeNumbers + "/" + lockedCross + " ";

    }

    void Update()
    {
        MouseInput();
        ObjectDragger();


        if(lockedCross == holeNumbers && levelPassed == false)
        {
            StartCoroutine(SceneReloader());
            levelPassed = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void MouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                DragObject = hit.collider.gameObject;
                if(DragObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    isDragging = true;
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            DragObject = null;
            isDragging = false;
        }
    }

    void ObjectDragger()
    {
       if(isDragging && DragObject != null)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - DragObject.transform.position;
            DragObject.GetComponent<Rigidbody2D>().MovePosition(DragObject.transform.position + direction * 100 * Time.deltaTime);

            if(Input.GetKeyDown(RotationKey) && DragObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.FreezeAll)
            {
                DragObject.transform.eulerAngles += new Vector3(0, 0, 90);
            }
        } 
    }

    public void CrossLocked()
    {
        //UI Number Up
        lockedCross++;
        ProgressText.GetComponent<Text>().text = holeNumbers + "/" + lockedCross + " ";
        Debug.Log("Cross locked");
    }

    public void CrossRelased()
    {
        //UI Number Down
        lockedCross--;
        ProgressText.GetComponent<Text>().text = holeNumbers + "/" + lockedCross + " ";
        Debug.Log("Cross relased");
    }

    IEnumerator SceneReloader()
    {
        Debug.Log("WIN WIN WIN");
        WinText.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
