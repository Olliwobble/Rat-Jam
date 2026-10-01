using UnityEngine;

public class EnemyAI3DSecond : MonoBehaviour
{
    [Header("Movement & Patrolling")]
    public float patrolSpeed = 2.5f;
    public float walkRange = 5f;
    public float idleTime = 1.5f;

    [Header("Chase Settings")]
    public float chaseSpeed = 4.5f;
    public float detectionRadius = 6f; // How close the player needs to be to start a chase

    [Header("Rotation Offset Settings")]
    [Tooltip("Adjust these sliders if your second enemy asset faces the wrong direction.")]
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;
    private bool isWaiting = false;
    private Transform playerTransform;
    private bool isChasing = false;

    void Start()
    {
        startPosition = transform.position;

        // Find the player automatically using their tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        GetNewRandomPosition();
    }

    void Update()
    {
        // 1. Check for the player
        CheckForPlayer();

        // 2. Handle State Behavior
        if (isChasing && playerTransform != null)
        {
            ChasePlayer();
        }
        else
        {
            PatrolRoutine();
        }
    }

    void CheckForPlayer()
    {
        if (playerTransform == null) return;

        // Calculate distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            // If we were chasing and the player got away, resume patrolling
            if (isChasing)
            {
                isChasing = false;
                startPosition = transform.position; // Reset patrol hub to current spot
                GetNewRandomPosition();
            }
        }
    }

    void ChasePlayer()
    {
        // Move towards player position
        Vector3 target = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);

        // Turn to look at player
        RotateTowards(target);
    }

    void PatrolRoutine()
    {
        if (isWaiting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isWaiting = false;
                GetNewRandomPosition();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);
            RotateTowards(targetPosition);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isWaiting = true;
                timer = idleTime;
            }
        }
    }

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // ADD AN OFFSET HERE: Change '90f' to 180f or -90f if it's facing the wrong way
            Quaternion offsetRotation = Quaternion.Euler(-90, 0, 270);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * offsetRotation, Time.deltaTime * 10f);
        }
    }

    void GetNewRandomPosition()
    {
        float randomX = Random.Range(-walkRange, walkRange);
        float randomZ = Random.Range(-walkRange, walkRange);
        targetPosition = new Vector3(startPosition.x + randomX, transform.position.y, startPosition.z + randomZ);
    }

    // Draw visualization wireframes in the Scene view
    private void OnDrawGizmosSelected()
    {
        // Draw the yellow walk zone bounds
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? startPosition : transform.position;
        Gizmos.DrawWireCube(center, new Vector3(walkRange * 2, 0.5f, walkRange * 2));

        // Draw the red vision detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}