using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTarget;

    [SerializeField]
    private float _smoothingValue;

    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = _cameraTarget.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraTarget.position - _cameraOffset, _smoothingValue * Time.deltaTime);
    }
}
