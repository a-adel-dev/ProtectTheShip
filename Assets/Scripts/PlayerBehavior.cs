
using TMPro;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private InteractionCollider _interactionCollider;

    [SerializeField]
    private int _harvestValue = 10;

    [SerializeField]
    private int weaponPower = 10;

    [SerializeField]
    private float timer = 2f;

    private int _metalValue;

    [SerializeField]
    TextMeshProUGUI metalValueText;

    private IInteractive _interactiveObject;
    float _timePassed;



    private void Awake()
    {
        _interactionCollider.OnInteractiveObjectEncountered += AcquireInteractiveObject;
        _interactionCollider.OnInteractiveObjectCleared += ClearInteractiveObject;
        metalValueText.SetText("O");
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed >= timer)
        {
            Interact();
            _timePassed = 0;
        }
    }

    private void AcquireInteractiveObject(IInteractive interactiveObject)
    {
        _interactiveObject = interactiveObject;
        Debug.Log($"acquired object");
    }


    private void Interact()
    {
        if (_interactiveObject == null) return;
        if (_interactiveObject.Type == InteractiveObjectType.ResourceNode)
        {
            var harvestableNode = (ResourceNode)_interactiveObject;
            int collected = harvestableNode.HarvestNode(_harvestValue);
            if ( collected > 0)
            {
                _metalValue += collected;
            }
            metalValueText.SetText(_metalValue.ToString());
        }
    }

    private void ClearInteractiveObject()
    {
        _interactiveObject = null;
        Debug.Log($"released Object");
    }

    private void OnDisable()
    {
        _interactionCollider.OnInteractiveObjectEncountered -= AcquireInteractiveObject;
        _interactionCollider.OnInteractiveObjectCleared -= ClearInteractiveObject;
    }
}
