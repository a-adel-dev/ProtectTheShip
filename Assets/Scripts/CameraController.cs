using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _smoothingValue;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position, _smoothingValue * Time.deltaTime);
    }
}
