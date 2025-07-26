using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/NPCDialog")]
public class Dialog : ScriptableObject
{
    public string npcName;
    public Sprite npcPotrait;
    [TextArea(2, 5)] public string[] dialogueLines;
    public float typingSpeed = 0.02f;
}
