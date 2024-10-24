using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colorswitch : MonoBehaviour
{
    private float rCorrect = 0;
    private float gCorrect = 128;
    private float bCorrect = 0;

    private float rIncorrect = 128;
    private float gIncorrect = 0;
    private float bIncorrect = 0;
    public Outline outline;

    public void red()
    {
        var color = new Color(rIncorrect / 255f, gIncorrect / 255f, bIncorrect / 255f);
        outline.effectColor = color;
    }
    public void green()
    {
        var color = new Color(rCorrect / 255f, gCorrect / 255f, bCorrect / 255f);
        outline.effectColor = color;
    }
}
