using UnityEngine;

public class RotatingWall : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float openAngle = 90f; // Degrees to rotate
    public float speed = 2f;      // Rotation speed

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpening = false;

    void Start()
    {
        closedRotation = transform.rotation;
        // Calculate target rotation around the Y axis
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);
    }

    void Update()
    {
        // Smoothly rotate toward the target
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = false;
        }
    }
}
