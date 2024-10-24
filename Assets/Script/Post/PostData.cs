using UnityEngine;

[CreateAssetMenu(fileName = "New Post Data", menuName = "Post Data", order = 51)]
public class PostData : ScriptableObject
{
    public string RU_postTitle;
    public string ENG_postTitle;
    public string RU_postContent;
    public string ENG_postContent;
}