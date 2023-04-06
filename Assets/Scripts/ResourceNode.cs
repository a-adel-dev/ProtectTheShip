using com.Artillery.UI;
using System;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField]
    private int resourceValue = 100;

    private bool _isResourceExhusted = false;
    

    public GameObject GameObject => gameObject;

    public bool IsResourceExhusted { get => _isResourceExhusted; set => _isResourceExhusted = value; }

    public int HarvestNode(int value)
    {
        if (value < resourceValue)
        {
            resourceValue -= value;
            return value;

        }
        else
        {
            IsResourceExhusted = true;
            return resourceValue;
        }
    }

    public void DestroyNode()
    {
        Destroy(gameObject, 0.5f);   
    }

    public void Destroy()
    {
        DestroyNode();
    }
}
