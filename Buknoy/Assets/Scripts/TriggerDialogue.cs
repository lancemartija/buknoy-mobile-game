using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerDialogue : MonoBehaviour
{
    public NPCConversation myConversation;
    
    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        if (other.tag == "Player") {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
