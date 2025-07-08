using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; // 플레이어의 최대 이동 속도
    public float jumpPower; // 플레이어의 점프 힘
    Rigidbody2D rigid; // Rigidbody2D 컴포넌트 참조
    SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트 참조
    Animator anim; // Animator 컴포넌트 참조

    void Awake()
    {
        // 컴포넌트 초기화
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueManager.isTalking) return; //대화중이면 점프 안한다

        // 플레이어 점프 중이 아닐때 점프 입력을 받으면 점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))//false 상태로 바뀌면 if문 실행
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // 위쪽으로 힘을 가해 점프
            anim.SetBool("isJumping", true); // 점프 애니메이션 활성화
        }
        
        // 수평 이동 키를 뗐을 때 속도를 줄임
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocity.y);
        }

        // 플레이어의 방향에 따라 스프라이트를 뒤집음
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; // 왼쪽으로 이동 시 스프라이트를 뒤집음
        }

        // 플레이어가 걷고 있는지 여부를 애니메이션에 반영
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false); // 걷지 않음
        }
        else
        {
            anim.SetBool("isWalking", true); // 걷는 중
        }
    }

    void FixedUpdate()
    {
        // 수평 입력 값 가져오기
        float h = Input.GetAxisRaw("Horizontal");

        // 입력 방향으로 힘을 가해 이동
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 최대 속도 제한
        if (rigid.linearVelocity.x > maxSpeed) // 오른쪽 최대 속도 초과 시
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        }
        else if (rigid.linearVelocity.x < maxSpeed * (-1)) // 왼쪽 최대 속도 초과 시
        {
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
        }

        //landing platform
        
        if (rigid.linearVelocity.y < 0) // 플레이어가 아래로 떨어지고 있는지 확인
        {
            // 디버그용으로 아래 방향으로 레이(광선)를 그려 시각적으로 확인
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            
            // 아래 방향으로 레이를 쏴서 "Platform" 레이어에 충돌하는지 확인
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null) // 레이가 플랫폼과 충돌했는지 확인
            {
                if (rayHit.distance < 0.5f) // 플레이어와 플랫폼 간의 거리가 0.5 이하인지 확인
                {
                    anim.SetBool("isJumping", false); // 점프 상태를 해제하여 착지 애니메이션으로 전환
                }
            }
        }
    }
}
