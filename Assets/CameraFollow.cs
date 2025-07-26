using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;      // Objek yang akan diikuti, biasanya pemain
    [SerializeField] private Vector3 offset;        // Offset kamera dari target
    [SerializeField] private float smoothSpeed = 0.125f;  // Kecepatan smoothing kamera

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}
