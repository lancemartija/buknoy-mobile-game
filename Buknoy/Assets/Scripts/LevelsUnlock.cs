using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsUnlock : MonoBehaviour
{
   public Button[] levelButtons;

    void Start()
    {
       int levelAt = PlayerPrefs.GetInt("levelAt", 2);

       for (int i = 0; i < levelButtons.Length; i++)
       {
            if (i + 2 > levelAt)
                levelButtons[i].interactable = false;
       }

    }
}
