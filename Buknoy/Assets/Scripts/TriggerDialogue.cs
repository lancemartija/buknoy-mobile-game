using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerDialogue : MonoBehaviour
{
    public NPCConversation myConversation;
    //private AudioSource Audio;
    private BoxCollider2D DialogueTriggerArea;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Audio = ConversationManager.Instance.GetComponent<AudioSource>();
        DialogueTriggerArea = GetComponent<BoxCollider2D>();
        print("Trigger Entered");
        
        if (other.tag == "Player")
        {
            ConversationManager.OnConversationStarted += ConversationStart;
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
    
    private void ConversationStart()
    {
        print("Trigger Destroyed");
        Destroy(DialogueTriggerArea);
    }
}
