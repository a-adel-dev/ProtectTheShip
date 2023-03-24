using com.ARTillery.Movement;
using System.Collections;
using UnityEngine;

namespace com.ARTillery.Combat
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField]
        private float range = 2f;

        public float Range { get => range; set => range = value; }

        private void Update()
        {

        }

    }
}