using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent unityEvent;
    public virtual void Interact()
    {
        unityEvent?.Invoke();
    }
}