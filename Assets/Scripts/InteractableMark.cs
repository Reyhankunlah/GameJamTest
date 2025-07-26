using UnityEngine;

public class InteractableMark : MonoBehaviour
{

    private bool isPlayerInRange = false;
    public GameObject tandaSeru;
    private SpriteRenderer tandaSeruRenderer;
    void Start()
    {
        if (tandaSeru != null)
        {
            tandaSeruRenderer = tandaSeru.GetComponent<SpriteRenderer>();
            if (tandaSeruRenderer != null)
                tandaSeruRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
                Interact();
            
        }
    }

    private void Interact()
    {
        if (tandaSeruRenderer != null)
            tandaSeruRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

                tandaSeruRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            
                tandaSeruRenderer.enabled = false;
        }
    }
}
