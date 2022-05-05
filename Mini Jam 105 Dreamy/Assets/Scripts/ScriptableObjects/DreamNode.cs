using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainNode", menuName = "Story/MainNode")]
public class DreamNode : ScriptableObject
{
    public StoryFragment[] fragments;
}
