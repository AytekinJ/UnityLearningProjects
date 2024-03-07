using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject FadeIn;
    public GameObject FadeOut;

    public GameObject Button1, Button2, Button3;

    private void Awake() {
        FadeOut.gameObject.SetActive(true);
        FadeIn.gameObject.SetActive(false);
    }
    public void ReloadActiveScene()
    {
        StartCoroutine(ReloadSameScene());
        FadeIn.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(false);
    }

    public void NextScene()
    {
        if(Button1 != null)
        {
            Button1.GetComponent<Animator>().SetBool("GoBack", true);
            Button2.GetComponent<Animator>().SetBool("GoBack", true);
            Button3.GetComponent<Animator>().SetBool("GoBack", true);

        }

        StartCoroutine(GoNextScene());
        FadeIn.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(false);
    }

    public void QuitApp()
    {
        Application.Quit();
    }


    IEnumerator ReloadSameScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator GoNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
