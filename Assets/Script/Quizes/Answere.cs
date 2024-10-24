using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answere : MonoBehaviour
{
    public Outline[] outline;
    private int clickcount;

    public void outlineManager()
    {
        clickcount++;
        if (clickcount >= 1)
        {
            for (int i = 0; i < outline.Length; i++)
            {
                outline[i].enabled = true;
            }

            clickcount = 0;
        }
        else
        {
            for (int i = 0; i < outline.Length; i++)
            {
                outline[i].enabled = false;
            }
        }
    }

    public void RetryOutline()
    {
        for (int i = 0; i < outline.Length; i++)
        {
            outline[i].enabled = false;
        }
    }
}
