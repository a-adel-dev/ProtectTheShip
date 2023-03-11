
using TMPro;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField]
    private int _harvestValue = 10;

    [SerializeField]
    private int weaponPower = 10;

    [SerializeField]
    private float _harvestingTimer = 2f;





    private void Awake()
    {
    }

    private void Update()
    {
        Interact();
    }


    private void Interact()
    {

    }

}
