using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoPlayer : MonoBehaviour
{
    public GameObject audio;
    public GameObject video;

    public void Mute()
    {
        audio.SetActive(!audio.activeSelf);
    }

    public void Video()
    {
        video.SetActive(!video.activeSelf);
    }
}
