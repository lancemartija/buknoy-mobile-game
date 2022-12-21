using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour
{
    [SerializeField] public List<Notes> notes;

    [SerializeField] public List<Image> NoteImage;
    [SerializeField] public Image ZoomImage;
    public int noteset;
    // Start is called before the first frame update
    public void NotesArrange(int index)
    {
        noteset = index;
        for (int i = 0; i < notes[index].notesImage.Count; i++)
        {
            NoteImage[i].sprite = notes[index].notesImage[i];
            NoteImage[i].gameObject.SetActive(true);
        }
    }
    public void NotesErase()
    {
        for (int i = 0; i < 6; i++)
        {
            NoteImage[i].gameObject.SetActive(false);
        }
    }
    public void NotesZoom(int index)
    {
        ZoomImage.sprite = notes[noteset].notesImage[index];
    }
}

[System.Serializable]
public class Notes
{

    public List<Sprite> notesImage;

}

