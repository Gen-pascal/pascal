using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed;// 카메라 이동 속도

    public Vector2 center;
    public Vector2 size; // 카메라가 따라갈 영역의 중심과 크기
    float height;
    float width;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        height = Camera.main.orthographicSize; // 카메라의 높이 계산
        width = height * Screen.width / Screen.height; // 카메라의 너비 계산
    }

    private void OnDrawGizmos()
    {
        // 카메라가 따라갈 영역을 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //같이 움직인다
        //transform.position = new Vector3(target.position.x, target.position.y, -10);

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.x * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, lx + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
