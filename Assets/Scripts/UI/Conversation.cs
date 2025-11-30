using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}

[System.Serializable]
public class DialogueLine
{
    public string speaker;      // tên người nói
    [TextArea(2, 5)]
    public string content;      // nội dung lời thoại
}