using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed;// ī�޶� �̵� �ӵ�

    public Vector2 center;
    public Vector2 size; // ī�޶� ���� ������ �߽ɰ� ũ��
    float height;
    float width;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        height = Camera.main.orthographicSize; // ī�޶��� ���� ���
        width = height * Screen.width / Screen.height; // ī�޶��� �ʺ� ���
    }

    private void OnDrawGizmos()
    {
        // ī�޶� ���� ������ �ð������� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //���� �����δ�
        //transform.position = new Vector3(target.position.x, target.position.y, -10);

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.x * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, lx + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
