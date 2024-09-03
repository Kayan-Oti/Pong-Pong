using UnityEngine;

[CreateAssetMenu(menuName = "DATA/DialogueData")]
public class SO_Dialogue : ScriptableObject
{
    public string Name;
    [TextArea(5,10)]
    public string[] Paragraphs;
}
