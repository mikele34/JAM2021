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
    public Text screwNumberLeft;
    public Text floorNumberLeft;

    public Text logNumbertot;
    public Text screwNumbertot;
    public Text floorNumbertot;

    public static int log = 0;
    public static int screw = 0;
    public static int floor = 0;

    void Update()
    {
        if (m_gameManager.m_forestLeft || m_gameManager.m_forestEarn)
        {
            Forest();
        }
        
        if (logNumber != null) { logNumber.text = log.ToString(); }
        if (logNumberLeft != null) { logNumberLeft.text = log.ToString(); }        
        if (logNumbertot != null) { logNumbertot.text = log.ToString(); }

        if (screwNumber != null) { screwNumber.text = screw.ToString(); }
        if (screwNumberLeft != null) { screwNumberLeft.text = screw.ToString(); }
        if (screwNumbertot != null) { screwNumbertot.text = screw.ToString(); }

        if (floorNumber != null) { floorNumber.text = floor.ToString(); }
        if (floorNumber != null) { floorNumberLeft.text = floor.ToString(); }
        if (floorNumbertot != null) { floorNumbertot.text = floor.ToString(); }
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
        m_gameManager.m_forestEarn = false;
        m_gameManager.m_forest = false;
    }
}
