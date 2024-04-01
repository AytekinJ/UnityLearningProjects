using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FBGameManager : MonoBehaviour
{
    [SerializeField] private Text PointText;
    [SerializeField] private GameObject Pipe, Ground;
    int point;

    private void Update() {
        Pipe.transform.position -= new Vector3(4 * Time.deltaTime, 0, 0);
        Ground.transform.position -= new Vector3(4 * Time.deltaTime, 0, 0);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void EarnPoint()
    {
        point++;
        PointText.text = point.ToString();
    }
    public void ReloadScene()
    {
        StartCoroutine(ReloadingScene());
    }

    IEnumerator ReloadingScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
