using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    GameManager m_gameManager;

    public static Text logNumber;
    public static Text screwNumber;
    public static Text floorNumber;

    public static int log;

    void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
    }


    void Update()
    {
        if (m_gameManager.m_forestLeft)
        {
            Forest();
        }

        //logNumber.text = log.ToString();
    }

    void Forest()
    {
        log += m_gameManager.m_logDrop2;
        m_gameManager.m_forestLeft = false;
        m_gameManager.m_forest = false;
    }
}
