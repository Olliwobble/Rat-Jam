using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DroppedItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    bool autoStart;
    
    [SerializeField]
    float enabledPickupDelay = 3.0f;

    [Header("Settings")]
    public Item item;
    public bool pickedUp = false;

    private void Start()
    {
        if (autoStart && item != null)
        {
            I
        }
     
}