using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        transform.Translate(new Vector3(horizontalMovement, 0, verticalMovement));
    }


}
