using UnityEngine;

public class DialogueOnStart : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        GetComponent<DialogueManager>().dialogueBox.SetActive(false);
        Invoke("Begin", 2f);
    }

    void Begin()
    {
        GetComponent<DialogueManager>().dialogueBox.SetActive(true);
        GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
}
