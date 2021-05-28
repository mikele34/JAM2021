using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameManager m_gameManager;

    public Text logNumber;
    public Text screwNumber;
    public Text floorNumber;
    
    public Text logNumberLeft;
    //public Text screwNumberLeft;
    //public Text floorNumberLeft;

    public Text logNumbertot;
    //public Text screwNumbertot;
    //public Text floorNumbertot;

    public static int log;
    public static int screw;
    public static int floor;

    void Update()
    {
        if (m_gameManager.m_forestLeft || m_gameManager.m_forestEarn)
        {
            Forest();
        }

        logNumber.text = log.ToString();
        logNumberLeft.text = log.ToString();
        logNumbertot.text = log.ToString();

        screwNumber.text = screw.ToString();
        //screwNumberLeft.text = screw.ToString();
        //screwNumbertot.text = screw.ToString();

        floorNumber.text = floor.ToString();
        //floorNumberLeft.text = floor.ToString();
        //floorNumbertot.text = floor.ToString();
    }

    void Forest()
    {
        if (m_gameManager.m_forestLeft)
        {
            log += m_gameManager.m_logDrop2;
        }
        else if (m_gameManager.m_forestEarn)
        {
            log += m_gameManager.m_logDrop;
        }
        
        m_gameManager.m_forestLeft = false;
        m_gameManager.m_forest = false;
    }
}
