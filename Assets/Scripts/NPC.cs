using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public Dialog dialogueData;
    public GameObject dialoguePanel;
    public Image avatarNPC;
    public TMP_Text txtNPCName;
    public TMP_Text txtDialogue;

    private int dialogueIndex = 0;
    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;
    private bool isTyping = false;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isDialogueActive)
        {
            StartDialogue();
        }

        if (!isPlayerInRange && isDialogueActive)
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueData == null)
            return;

        dialogueIndex = 0;
        isDialogueActive = true;

        avatarNPC.sprite = dialogueData.npcPotrait;
        txtNPCName.SetText(dialogueData.npcName);
        dialoguePanel.SetActive(true);

        StartCoroutine(TypeLine());
    }

    public void OnPanelClick()
    {
        if (!isDialogueActive || isTyping)
            return;

        dialogueIndex++;

        if (dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        isTyping = false;
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        txtDialogue.SetText("");

        string line = dialogueData.dialogueLines[dialogueIndex];

        foreach (char letter in line)
        {
            txtDialogue.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
