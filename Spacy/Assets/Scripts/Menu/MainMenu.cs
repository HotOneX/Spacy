﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    /*public void GoToGarage()
    {
        SceneManager.LoadScene("Garage");
    }*/
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Test()
    {
        Debug.Log("Play Pressed!!");
    }
}
