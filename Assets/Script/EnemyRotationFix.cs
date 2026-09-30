using UnityEngine;

public class EnemyRotationFix : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;

    [Tooltip("Adjust this offset angle if the enemy faces the wrong way (e.g., 0, 90, -90, 180).")]
    public float yOffsetAngle = 90f;

    // You can call this public function from other scripts to make the enemy rotate
    public void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            // 1. Calculate the moving direction (ignoring Y axis height)
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // 2. Create the turn offset using the value set in the Inspector
            Quaternion offsetRotation = Quaternion.Euler(0, yOffsetAngle, 0);

            // 3. Combine them together and smoothly rotate
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * offsetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}