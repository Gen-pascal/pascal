using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("이동 속도 / 점프 힘")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    Rigidbody2D rb;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (DialogueManager.isTalking || DialogueManager.justEndedDialogue) return;
        Move();
    }

    void Update()
    {
        if (DialogueManager.isTalking || DialogueManager.justEndedDialogue) return;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
