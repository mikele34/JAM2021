using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriting : MonoBehaviour
{
    float m_timer = 0.0f;
    float m_timerD = 0.0f;
    int step = 1;

    int m_charactersPerSecond = 30;
    float characterDT = 0.0f;

    Text m_text;

    string testoDaScrivere = "hello world";

    bool m_startWrite = false;

    public GameObject Dialog;

    void Awake()
    {
        m_text = GetComponent<Text>();


        characterDT = 1.0f / m_charactersPerSecond;
    }

    void Update()
    {
        if (m_startWrite) //cancello
        {
            m_timer += Time.deltaTime;

            if (m_timer >= characterDT)
            {
                
                if (step <= testoDaScrivere.Length)
                {
                    m_text.text = testoDaScrivere.Substring(0, step);
                    step++;
                }
                else
                {
                    m_startWrite = false;
                }

                m_timer = 0.0f;
            }
        }
        else
        {
            m_timerD += Time.deltaTime;
            if(m_timerD >= 1.0f)
            Dialog.SetActive(false);
        }
    }

    public void write(string phrase, int charactersPerSecond, Color textColor)
    {
        testoDaScrivere = phrase;
        m_charactersPerSecond = charactersPerSecond;
        characterDT = 1.0f / m_charactersPerSecond;

        m_text.color = textColor;

        m_timer = 0.0f;
        step = 1;
        m_startWrite = true;
    }

    public void skip()
    {
        m_startWrite = false;
        m_text.text = testoDaScrivere;
    }
}
