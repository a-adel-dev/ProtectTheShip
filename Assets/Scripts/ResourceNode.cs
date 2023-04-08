using UnityEngine;
using com.ARTillery.Inventory;
using com.Artillery.UI;
using com.ARTillery.UI;

public class ResourceNode : MonoBehaviour
{
    [SerializeField]
    private int resourceValue = 100;

    [SerializeField] private ResourceType resourceType;

    [SerializeField] private FloatingText gatheredResourceText;


    public GameObject GameObject => gameObject;

    public bool IsResourceExhausted { get; private set; } = false;

    public int HarvestNode(int value, out ResourceType type)
    {
        if (value < resourceValue)
        {
            resourceValue -= value;
            type = resourceType;
            UpdateFloatingText(value);
            return value;

        }
        else
        {
            IsResourceExhausted = true;
            type = resourceType;
            UpdateFloatingText(resourceValue);
            return resourceValue;
        }
    }

    public void InitializeNode()
    {
        gatheredResourceText.gameObject.SetActive(true);
        gatheredResourceText.ResetAnimation();
    }

    public void DisableNode()
    {
        gatheredResourceText.gameObject.SetActive(false);
    }
    

    private void UpdateFloatingText(int value)
    {
        
        gatheredResourceText.DisplayFloatingText(value, FloatingTextType.Ore);
    }

    public void DestroyNode()
    {
        Destroy(gameObject, 0.5f);   
    }

    public void Destroy()
    {
        DestroyNode();
    }

    public void DestroySourceNode()
    {
        Destroy(gameObject);
    }
}