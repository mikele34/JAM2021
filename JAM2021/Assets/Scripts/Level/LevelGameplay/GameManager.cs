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
    public PlayerManager playerManager;

    float m_timer = 120.0f;

    public int m_logDrop = 30;
    public int m_logDrop2 = 15;
    public int m_alive = 3;

    bool m_timerStart = true;

    [HideInInspector]public bool m_forest = false;    
    [HideInInspector]public bool m_forestLeft = false;
    [HideInInspector]public bool m_forestEarn = false;

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
                m_forestLeft = true;
                Left_popUp.SetActive(true);
            }                      
        }

        if (playerManager.death)
        {
            Death_popUp.SetActive(true);
        }

        if (m_alive == 0)
        {
            m_forestEarn = true;
            Earn_popUp.SetActive(true);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }    

    public void Gohome()
    {
        Left_popUp.SetActive(false);
        Death_popUp.SetActive(false);
        Earn_popUp.SetActive(false);
        sceneFader.FadeTo(levelToLoad);
    }

    public void Menu()
    {
        levelToLoad = "Menu";
        sceneFader.FadeTo(levelToLoad);
    }

    public void Close()
    {
        playerManager.Pause.SetActive(false);
    }
}
