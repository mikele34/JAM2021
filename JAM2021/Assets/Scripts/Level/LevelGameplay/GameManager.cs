using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text Timer;
    public SceneFader sceneFader;
    public GameObject Left_popUp;
    public GameObject Death_popUp;
    public GameObject Earn_popUp;

    float m_timer = 2.0f;

    int m_logDrop = 30;
    int m_logDrop2 = 15;

    bool m_timerStart = true;
    bool m_forest = false;

    string levelToLoad = "BaseLevel";

    void Awake()
    {
        m_timerStart = true;
        Timer.enabled = true;

        if (SceneManager.GetActiveScene().name == "Forest")
        {
            m_forest = true;
        }
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
            Timer.enabled = false;

            if (m_forest)
            {
                Forest();
            }                      
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Forest()
    {
        InventoryManager.m_log += m_logDrop2;
        Left_popUp.SetActive(true);
        m_forest = false;
    }

    public void Gohome()
    {
        sceneFader.FadeTo(levelToLoad);
    }
}
