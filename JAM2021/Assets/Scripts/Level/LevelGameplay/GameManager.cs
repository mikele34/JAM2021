using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text Timer;
    public SceneFader sceneFader;

    float m_timer = 2.0f;
    float m_timerLoad = 0.0f;

    bool m_timerStart = true;

    string levelToLoad = "BaseLevel";

    void Awake()
    {
        m_timerStart = true;
    }

    void Update()
    {
        if (m_timerStart)
        {
            m_timer -= Time.deltaTime;
            DisplayTime(m_timer);
        }
        else
        {
            m_timer = 300.0f;
            m_timerStart = false;
        }
        

        if (m_timer <= 0.0f)
        {
            sceneFader.FadeTo(levelToLoad);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
