using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class TriggerDialogue : MonoBehaviour
{
    public NPCConversation myConversation;
    private Button pauseBtn;
    private GameObject controls;
    private BoxCollider2D DialogueTriggerArea;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        pauseBtn = GameObject.Find("PauseButton").GetComponent<Button>();
        controls = GameObject.Find("Controls");
        DialogueTriggerArea = GetComponent<BoxCollider2D>();
        
        print("Trigger Entered");
        
        if (other.tag == "Player")
        {
            ConversationManager.OnConversationStarted += ConversationStart;
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.OnConversationEnded += ConversationEnd;
        }
    }
    
    private void ConversationStart()
    {
        print("Trigger Destroyed");
        print("Buttons Disabled");
        pauseBtn.interactable = false;
        controls.SetActive(false);
        Destroy(DialogueTriggerArea);
    }
    
    private void ConversationEnd()
    {
        print("Buttons Enabled");
        pauseBtn.interactable = true;
        controls.SetActive(true);
    }
}
