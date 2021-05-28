using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TypeWriting TypeWriting;
    public PlayerManager playerManager;


    public string[] phrases;
    int step = 0;

    int writingStep = 0; // 0 = write; 1 = wait;

    public void DialogStart()
    {
        
        if (step < phrases.Length)
        {
                switch (writingStep)
                {
                    case 0: //write
                        TypeWriting.write(phrases[step], 10, Color.black);
                        step++;
                        writingStep = 1;
                        break;

                    case 1: // wait
                        TypeWriting.skip();
                        writingStep = 0;
                        break;
                }
        }
        else
        {
                TypeWriting.skip();
        }        
    }
}
