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
        print("Trigger Entered");
        
        pauseBtn = GameObject.Find("PauseButton").GetComponent<Button>();
        controls = GameObject.Find("Controls");
        DialogueTriggerArea = GetComponent<BoxCollider2D>();
        
        if (other.tag == "Player")
        {
            if (ConversationManager.Instance != null)
            {
                ConversationManager.OnConversationStarted += ConversationStart;
                ConversationManager.Instance.StartConversation(myConversation);
                ConversationManager.OnConversationEnded += ConversationEnd;
            }
        }
    }
    
    private void ConversationStart()
    {
        print("Trigger Disabled");
        print("Buttons Disabled");
        
        if (pauseBtn != null)
        {
            pauseBtn.interactable = false;
        }
        
        if (controls != null)
        {
            controls.SetActive(false);
        }
        
        if (DialogueTriggerArea != null)
        {
            DialogueTriggerArea.enabled = false;
        }
        
    }
    
    private void ConversationEnd()
    {
        print("Buttons Enabled");
        
        if (pauseBtn != null)
        {
            pauseBtn.interactable = true;
        }
        
        if (controls != null)
        {
            controls.SetActive(true);
        }
    }
}
