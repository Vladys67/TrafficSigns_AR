using UnityEngine;

[CreateAssetMenu(fileName = "NewTrafficSign",
    menuName = "Traffic Signs/Sign Data")]
public class TrafficSignData : ScriptableObject
{
    public string signName;
    public string category;

    [TextArea(3, 6)]
    public string description;

    public AudioClip audioClip;
}