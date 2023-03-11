using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ARTillery.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;

        [SerializeField]
        private float _smoothingValue;

        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _player.position, _smoothingValue * Time.deltaTime);
        }
    }
}
