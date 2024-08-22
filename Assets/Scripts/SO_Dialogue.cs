using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class SO_Dialogue : ScriptableObject
{
    public string Name;
    [TextArea(5,10)]
    public string[] Paragraphs;
}
