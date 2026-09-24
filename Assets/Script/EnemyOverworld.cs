using UnityEngine;

public class EnemyRNGMovement3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float walkRange = 5f;     // How far the enemy can walk from its starting point
    public float idleTime = 1.5f;    // How long the enemy waits before moving again

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;
    private bool isWaiting = false;

    void Start()
    {
        // Store the original position to keep the enemy in a specific zone
        startPosition = transform.position;
        GetNewRandomPosition();
    }

    void Update()
    {
        if (isWaiting)
        {
            // Count down the idle timer
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isWaiting = false;
                GetNewRandomPosition();
            }
        }
        else
        {
            // Move toward the random target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Rotate smoothly to face the moving direction (Optional)
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Check if the enemy reached the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartWaiting();
            }
        }
    }

    void GetNewRandomPosition()
    {
        // Pick a random X and Z coordinate within the walk range
        float randomX = Random.Range(-walkRange, walkRange);
        float randomZ = Random.Range(-walkRange, walkRange);

        // Calculate target relative to the start position
        targetPosition = new Vector3(startPosition.x + randomX, transform.position.y, startPosition.z + randomZ);
    }

    void StartWaiting()
    {
        isWaiting = true;
        timer = idleTime;
    }

    // Visualizes the wander zone in the Unity Editor Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? startPosition : transform.position;
        Gizmos.DrawWireCube(center, new Vector3(walkRange * 2, 0.5f, walkRange * 2));
    }
}

