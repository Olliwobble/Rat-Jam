using UnityEngine;

public class DestroyOnTrigger3D : MonoBehaviour
{
    [Header("Filter Settings")]
    [Tooltip("If checked, it will only destroy itself if hit by an object tagged 'Player'.")]
    public bool onlyDestroyOnPlayer = true;

    // Notice: Used Collider (3D) instead of Collider2D
    private void OnTriggerEnter(Collider other)
    {
        if (onlyDestroyOnPlayer)
        {
            // Only destroy if the object entering the trigger zone is tagged "Player"
            if (other.CompareTag("Player"))
            {
                DestroyObject();
            }
        }
        else
        {
            // Destroy no matter what touches it
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // Instantly deletes this object from the 3D game world memory
        Destroy(gameObject);
    }
}
