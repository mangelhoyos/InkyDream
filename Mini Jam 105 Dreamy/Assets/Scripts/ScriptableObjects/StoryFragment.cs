using UnityEngine;

[CreateAssetMenu(fileName = "StoryFragment", menuName = "Story/Story Fragment")]
public class StoryFragment : ScriptableObject
{
    public string storyString;
    public bool hasDrawingAfter;
    public string drawIndicatorString;
}
