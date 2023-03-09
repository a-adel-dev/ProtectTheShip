using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{
    public Action<IInteractive> OnInteractiveObjectEncountered;
    public Action OnInteractiveObjectCleared;
    private IInteractive _interactiveObject;

    public IInteractive InteractiveObject 
    {
        get => _interactiveObject;
        set => _interactiveObject = value; 
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactibleObject = other.GetComponent<IInteractive>();
        if (interactibleObject != null)
        {
            InteractiveObject = interactibleObject;
            OnInteractiveObjectEncountered?.Invoke(InteractiveObject);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        var interactibleObject = other.GetComponent<IInteractive>();
        if (interactibleObject != null)
        {
            InteractiveObject = null;
            OnInteractiveObjectCleared?.Invoke();
        }
    }
}
