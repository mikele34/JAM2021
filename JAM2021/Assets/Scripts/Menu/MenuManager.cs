using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public string levelToLoad = "Base";

    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
        Time.timeScale = 1f;
    }

    public void Credits()
    {
        levelToLoad = "Credits";
        sceneFader.FadeTo(levelToLoad);
    }

    public void Back()
    {
        levelToLoad = "Menu";
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
