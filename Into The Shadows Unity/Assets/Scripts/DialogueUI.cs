using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
    }
    
    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return))//if dialogue is active we can use esc to hide it
        {
            HideDialogue();
        }
    }


    public void ShowDialogue(string message)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
