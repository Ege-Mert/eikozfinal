using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool canMove = true;
    private bool isAlive = true;
    private bool isGrounded = true;

    private Rigidbody2D rb;

    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler OnPlayerDeath;
    public event PlayerDeathHandler OnPlayerFinish;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isAlive) return;

        float moveX = Input.GetAxis("Horizontal");

        if (canMove)
        {
            Vector2 move = new Vector2(moveX, 0);
            transform.Translate(move * moveSpeed * Time.deltaTime);

            if (move != Vector2.zero && EyeManager.Instance.IsEyeOpen)
            {
                Die();
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    private void Die()
    {
        isAlive = false;
        OnPlayerDeath?.Invoke();
        Debug.Log("Player has died.");
    }

    private void Jump()
    {
        if (EyeManager.Instance.IsEyeOpen)
        {
            Die();
            return;
        }

        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Player has reached the end of the level!");
            OnPlayerFinish?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
