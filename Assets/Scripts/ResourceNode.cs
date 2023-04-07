using UnityEngine;
using com.ARTillery.Inventory;

public class ResourceNode : MonoBehaviour
{
    [SerializeField]
    private int resourceValue = 100;

    [SerializeField] private ResourceType resourceType;


    public GameObject GameObject => gameObject;

    public bool IsResourceExhausted { get; private set; } = false;

    public int HarvestNode(int value, out ResourceType type)
    {
        if (value < resourceValue)
        {
            resourceValue -= value;
            type = resourceType;
            return value;

        }
        else
        {
            IsResourceExhausted = true;
            type = resourceType;
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
