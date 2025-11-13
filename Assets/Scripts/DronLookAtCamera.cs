using UnityEngine;

public class DronLookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float rotationSpeed = 5f;
    void Update()
    {
        if (cameraPivot == null) return;

        Vector3 lookDirection = -cameraPivot.forward;
        lookDirection.Normalize();

        // Rotación objetivo
        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
