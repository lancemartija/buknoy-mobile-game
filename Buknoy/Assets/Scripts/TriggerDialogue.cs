using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class TriggerDialogue : MonoBehaviour
{
    public NPCConversation myConversation;
    private Button pauseBtn;
    private Button skipBtn;
    private GameObject controls;
    private GameObject skipBtnComponent;
    private BoxCollider2D DialogueTriggerArea;
    
    void Start() {
        pauseBtn = GameObject.Find("PauseButton").GetComponent<Button>();
        skipBtn = GameObject.Find("SkipButton").GetComponent<Button>();
        controls = GameObject.Find("Controls");
        DialogueTriggerArea = GetComponent<BoxCollider2D>();
        skipBtnComponent = GameObject.Find("SkipButton");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        
        if (other.tag == "Player")
        {
            if (ConversationManager.Instance != null)
            {
                if (skipBtnComponent != null)
                {
                    skipBtnComponent.SetActive(true);
                }
                
                ConversationManager.OnConversationStarted += ConversationStart;
                ConversationManager.Instance.StartConversation(myConversation);
                ConversationManager.OnConversationEnded += ConversationEnd;
            }
        }
    }
    
    public void SkipButtonOnClick()
    {
        print("Skip Dialogue");
        
        ConversationManager.Instance.EndConversation();
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
        
        if (skipBtnComponent != null)
        {
            skipBtnComponent.SetActive(true);
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
        
        if (skipBtnComponent != null)
        {
            skipBtnComponent.SetActive(false);
        }
    }
}
