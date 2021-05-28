using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static Text logNumber;
    public static Text screwNumber;
    public static Text floorNumber;

    public static int m_log;

    void Update()
    {
        Debug.Log(m_log.ToString());

        logNumber.text = m_log.ToString();
    }
}
