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
    
    void Start()
    {
        pauseBtn = GameObject.Find("PauseButton").GetComponent<Button>();
        controls = GameObject.Find("Controls");
        DialogueTriggerArea = GetComponent<BoxCollider2D>();
    }
    
    // private void Awake() {
    //     if (pauseBtn != null && DialogueTriggerArea != null)
    //     {   
    //         pauseBtn = GameObject.Find("PauseButton").AddComponent<Button>() as Button;
    //         DialogueTriggerArea = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
    //         return;
    //     }
    // }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        
        if (other.tag == "Player")
        {
            if (ConversationManager.Instance != null)
            {
                ConversationManager.Instance.StartConversation(myConversation);
                ConversationManager.OnConversationStarted += ConversationStart;
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
