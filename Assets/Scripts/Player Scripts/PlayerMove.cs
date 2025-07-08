using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // �÷��̾��� �ִ� �̵� �ӵ�
    public float jumpPower; // �÷��̾��� ���� ��
    Rigidbody2D rigid; // Rigidbody2D ������Ʈ ����
    SpriteRenderer spriteRenderer; // SpriteRenderer ������Ʈ ����
    Animator anim; // Animator ������Ʈ ����

    void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueManager.isTalking) return; //��ȭ���̸� ���� ���Ѵ�

        // �÷��̾� ���� ���� �ƴҶ� ���� �Է��� ������ ����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))//false ���·� �ٲ�� if�� ����
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // �������� ���� ���� ����
            anim.SetBool("isJumping", true); // ���� �ִϸ��̼� Ȱ��ȭ
        }
        
        // ���� �̵� Ű�� ���� �� �ӵ��� ����
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocity.y);
        }

        // �÷��̾��� ���⿡ ���� ��������Ʈ�� ������
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; // �������� �̵� �� ��������Ʈ�� ������
        }

        // �÷��̾ �Ȱ� �ִ��� ���θ� �ִϸ��̼ǿ� �ݿ�
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false); // ���� ����
        }
        else
        {
            anim.SetBool("isWalking", true); // �ȴ� ��
        }
    }

    void FixedUpdate()
    {
        // ���� �Է� �� ��������
        float h = Input.GetAxisRaw("Horizontal");

        // �Է� �������� ���� ���� �̵�
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // �ִ� �ӵ� ����
        if (rigid.linearVelocity.x > maxSpeed) // ������ �ִ� �ӵ� �ʰ� ��
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        }
        else if (rigid.linearVelocity.x < maxSpeed * (-1)) // ���� �ִ� �ӵ� �ʰ� ��
        {
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
        }

        //landing platform
        
        if (rigid.linearVelocity.y < 0) // �÷��̾ �Ʒ��� �������� �ִ��� Ȯ��
        {
            // ����׿����� �Ʒ� �������� ����(����)�� �׷� �ð������� Ȯ��
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            
            // �Ʒ� �������� ���̸� ���� "Platform" ���̾ �浹�ϴ��� Ȯ��
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null) // ���̰� �÷����� �浹�ߴ��� Ȯ��
            {
                if (rayHit.distance < 0.5f) // �÷��̾�� �÷��� ���� �Ÿ��� 0.5 �������� Ȯ��
                {
                    anim.SetBool("isJumping", false); // ���� ���¸� �����Ͽ� ���� �ִϸ��̼����� ��ȯ
                }
            }
        }
    }
}
