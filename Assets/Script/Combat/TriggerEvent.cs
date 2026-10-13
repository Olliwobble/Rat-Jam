using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent uEvent;
    public GameObject TriggerObject;
    public GameObject bottleFight;

    public void Start()
    {
        bottleFight.gameObject.SetActive(false);
    }

    public void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Trigger"))

            bottleFight.gameObject.SetActive(true);
        uEvent.Invoke();
    }

}