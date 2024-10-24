using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoData videoData; // Ссылка на ваш ScriptableObject
    public Text videoTitleText; // Ссылка на Text объект
    public Text videoNameText; // Ссылка на Text объект
    public VideoPlayer videoPlayer; // Ссылка на Video объект

    private void OnEnable()
    {
        videoTitleText.text = videoData.videoName;
    }

    public void OnClick()
    {
        // Обновляем имя видео
        videoNameText.text = videoData.videoName;
        
        // Обновляем ссылку на видео
        // videoPlayer.url = videoData.videoUrl;
    }
}