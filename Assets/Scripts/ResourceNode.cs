using System;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField]
    private int resourceValue = 100;
    public Action OnDestroy;
    

    public GameObject GameObject => gameObject;


    public int HarvestNode(int value)
    {
        if (value < resourceValue)
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
