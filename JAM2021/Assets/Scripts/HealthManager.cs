using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int Health;
    public int numOfHearts;


    void Update()
    {

        if(Health > numOfHearts)
        {
            Health = numOfHearts;
        }


    }
}
