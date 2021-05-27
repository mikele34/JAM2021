using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public int portal = 1; // 1->forest   2->beach   3->city    4->GoIN    5->GoOUT

    public SceneFader sceneFader;

    string levelToLoad;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player" && portal == 1)
        {
            levelToLoad = "Forest";
            sceneFader.FadeTo(levelToLoad);
        }
        else if(collision.gameObject.tag == "Player" && portal == 2)
        {
            levelToLoad = "Beach";
            sceneFader.FadeTo(levelToLoad);
        }
        else if (collision.gameObject.tag == "Player" && portal == 3)
        {
            levelToLoad = "City";
            sceneFader.FadeTo(levelToLoad);
        }
        else if (collision.gameObject.tag == "Player" && portal == 4)
        {
            levelToLoad = "DormIN";
            sceneFader.FadeTo(levelToLoad);
        }
        else if (collision.gameObject.tag == "Player" && portal == 5)
        {
            Debug.Log(PlayerManager.m_first);

            if (!PlayerManager.m_first)
            {
                levelToLoad = "BaseLevel";
                sceneFader.FadeTo(levelToLoad);
            }
            else
            {
                levelToLoad = "DestoyedBaseLevel";
                sceneFader.FadeTo(levelToLoad);
            }            
        }
    }
}
