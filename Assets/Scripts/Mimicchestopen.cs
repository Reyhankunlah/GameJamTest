using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mimicchestopen : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public GameObject tandaSeru; // Objek tanda seru di atas chest
    public Animator chestAnimator; // Animator peti
    private SpriteRenderer tandaSeruRenderer;
    public Dialog dialogueData;
    public GameObject dialoguePanel;
    public Image avatarNPC;
    public TMP_Text txtNPCName;
    public TMP_Text txtDialogue;

    private int dialogueIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    [SerializeField] private AudioSource audioSc;

    

    private readonly string openedParam = "Opened"; // Nama parameter animator

    void Start()
    {
        if (tandaSeru != null)
        {
            tandaSeruRenderer = tandaSeru.GetComponent<SpriteRenderer>();
            if (tandaSeruRenderer != null)
                tandaSeruRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!chestAnimator.GetBool(openedParam)) // Jika belum terbuka
            {
                Interact();
            }
        }
    }
    private void Interact()
    {
        if (chestAnimator != null)
        {
            chestAnimator.SetTrigger("Open");
            chestAnimator.SetBool(openedParam, true); // Tandai sudah terbuka
        }

        if (tandaSeruRenderer != null)
            tandaSeruRenderer.enabled = false; // Hilangkan tanda seru

        if (audioSc != null && !audioSc.isPlaying)
        {
            audioSc.Play();
        }
        StartCoroutine(DelayedDialogueStart());
    }

    IEnumerator DelayedDialogueStart()
    {
        yield return new WaitForSeconds(1f);
        StartDialogue();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            if (!chestAnimator.GetBool(openedParam) && tandaSeruRenderer != null)
                tandaSeruRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (!chestAnimator.GetBool(openedParam) && tandaSeruRenderer != null)
                tandaSeruRenderer.enabled = false;
        }
    }
    
    private void StartDialogue()
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

    public void ShowText()
    {
        StartDialogue();
    }

}
