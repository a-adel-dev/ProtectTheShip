using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IInteractive
{
    [SerializeField]
    private int resourceValue = 100;
    public Action OnDestroy;
    

    public GameObject GameObject => gameObject;

    public InteractiveObjectType Type => InteractiveObjectType.ResourceNode;

    public int HarvestNode(int value)
    {
        if (value > resourceValue)
        {
            resourceValue -= value;
            return value;

        }
        else
        {
            DestroyNode();
            return resourceValue;
        }
    }

    public void DestroyNode()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject, 0.5f);   
    }

    public void Destroy()
    {
        DestroyNode();
    }
}
