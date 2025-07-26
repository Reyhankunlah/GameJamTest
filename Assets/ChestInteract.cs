using UnityEngine;

public class ChestInteract : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public GameObject tandaSeru; // Objek tanda seru di atas chest
    public Animator chestAnimator; // Animator peti
    private SpriteRenderer tandaSeruRenderer;

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
}
