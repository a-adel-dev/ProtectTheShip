using System.Collections;
using UnityEngine;

namespace com.ARTillery.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        [SerializeField]
        private Material _targetHighlightMaterial;

        private Material _originalMaterial;

        public void SetTargetVisual()
        {
            _originalMaterial = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = _targetHighlightMaterial;
        }

        public void ClearTargetVisual()
        {
            GetComponent<Renderer>().material = _originalMaterial;
        }

    }

   
}