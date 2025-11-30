using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public ConversationEventPair[] eventPairs;
    public ConversationController controller;  // kéo ConversationController vào đây

    void OnEnable()
    {
        controller.gameObject.SetActive(false);
        foreach (var pair in eventPairs)
            pair.triggerEvent.Register(() => StartConversation(pair.conversation));
    }

    void OnDisable()
    {
        foreach (var pair in eventPairs)
            pair.triggerEvent.Unregister(() => StartConversation(pair.conversation));
    }

    void StartConversation(Conversation convo)
    {
        Debug.Log("Starting conversation");
        if (controller != null)
        {
            controller.gameObject.SetActive(true);
            controller.StartConversation(convo);
        }
    }
}