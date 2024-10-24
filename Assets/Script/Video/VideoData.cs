using UnityEngine;

[CreateAssetMenu(fileName = "NewVideoData", menuName = "ScriptableObjects/VideoData", order = 1)]
public class VideoData : ScriptableObject
{
    public string videoName;
    public string videoUrl;
}