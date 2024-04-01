using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllMenuManager : MonoBehaviour
{
    public void CrossGameStart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void FlappyBirdStart()
    {
        SceneManager.LoadScene("FlappyBird");
    }

    public void SpecialVideo()
    {
        SceneManager.LoadScene("RickRoll");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
