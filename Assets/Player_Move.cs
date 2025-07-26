using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Player_Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public AudioClip footstepClip;

    private Vector2 movement;
    private bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null) animator = GetComponent<Animator>();
        if (footstepClip != null)
        {
            audioSource.clip = footstepClip;
            audioSource.loop = true;
        }
    }

    void Update()
    {
        // Ambil input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        isMoving = movement.magnitude > 0;

        // Kirim parameter ke Animator
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.Play("WalkHorizontal");
            spriteRenderer.flipX = movement.x < 0;
        }
        else if (movement.y > 0)
        {
            animator.Play("WalkUp");
        }
        else if (movement.y < 0)
        {
            animator.Play("WalkDown");
        }

        // Play/Stop footstep sound
        if (isMoving)
        {
            if (!audioSource.isPlaying && footstepClip != null)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
