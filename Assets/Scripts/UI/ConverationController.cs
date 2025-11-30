using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour
{
    public Conversation conversation;
    // public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public MonoBehaviour playerController; // script điều khiển player

    [System.Serializable]
    public class CharacterAvatar
    {
        public string speaker;     // giống với speaker trong Conversation
        public Image avatarImage;  // hình UI
    }
    public List<CharacterAvatar> avatars = new List<CharacterAvatar>();

    int index = -1;
    bool isRunning = false;

    void Update()
    {
        if (!isRunning) return;
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            NextLine();
            Debug.Log("Next line");
        }
    }

    public void StartConversation(Conversation convo)
    {
        if (playerController != null)
            playerController.enabled = false;

        conversation = convo;
        dialoguePanel.SetActive(true);
        index = -1;
        isRunning = true;

        // bật tất cả avatar
        foreach (var av in avatars)
            av.avatarImage.gameObject.SetActive(true);

        NextLine();
    }

    void NextLine()
    {
        index++;

        if (index >= conversation.lines.Count)
        {
            EndConversation();
            return;
        }

        var line = conversation.lines[index];
        // speakerNameText.text = line.speaker;
        dialogueText.text = line.content;

        UpdateAvatars(line.speaker);
    }

    void UpdateAvatars(string activeSpeaker)
    {
        foreach (var av in avatars)
        {
            bool talking = av.speaker == activeSpeaker;

            // sáng / mờ
            av.avatarImage.color = talking ? Color.white : new Color(0.5f, 0.5f, 0.5f);

            // to / nhỏ
            av.avatarImage.transform.localScale = talking
                ? Vector3.one * 1.2f
                : Vector3.one * 0.9f;
        }
        Debug.Log("Active speaker: " + activeSpeaker);
    }

    void EndConversation()
    {
        isRunning = false;
        dialoguePanel.SetActive(false);

        // ẩn tất cả avatar
        foreach (var av in avatars)
        {
            av.avatarImage.gameObject.SetActive(false);
        }

        if (playerController != null)
            playerController.enabled = true;
    }
}
