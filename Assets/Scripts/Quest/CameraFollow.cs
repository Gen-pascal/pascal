using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("추적할 대상")]
    public Transform target;        // 플레이어 Transform

    [Header("딜레이 속도 (0~1)")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;  // 작을수록 부드럽고 느림

    [Header("카메라 오프셋")]
    public Vector3 offset = new Vector3(0, 0, -10); // Z는 카메라 거리

    void LateUpdate()
    {
        if (target == null) return;

        // 1) 목표 위치 계산 (플레이어 위치 + 오프셋)
        Vector3 desiredPosition = target.position + offset;

        // 2) 현재 카메라 위치에서 목표 위치로 Lerp (부드럽게 이동)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 3) 최종 위치 적용
        transform.position = smoothedPosition;
    }
}
