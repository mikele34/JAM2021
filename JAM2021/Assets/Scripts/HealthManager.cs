using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int Health;
    public int numOfHearts;


    void Awake()
    {
        Health = numOfHearts;
    }


    void Update()
    {
        if(Health > numOfHearts)
        {
            Health = numOfHearts;
        }
    }
}