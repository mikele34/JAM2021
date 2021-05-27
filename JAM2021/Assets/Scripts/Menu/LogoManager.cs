using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    public string levelToLoad = "Menu";

    public SceneFader sceneFader;

    float m_timer = 0.0f;


    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= 4f)
        {
            sceneFader.FadeTo(levelToLoad);
            m_timer = 0.0f;
        }
    }

}
