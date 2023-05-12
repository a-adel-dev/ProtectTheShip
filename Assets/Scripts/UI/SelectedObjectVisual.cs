using UnityEngine;

namespace com.ARTillery.UI
{
    public class SelectedObjectVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _objectMesh;
        [SerializeField] string _outlineLayerName;
        
        
        public void SetSelectedVisual()
        {
            _objectMesh.layer = LayerMask.NameToLayer(_outlineLayerName);
        }

        public void ClearSelectedVisual()
        {
            _objectMesh.layer = 0;
        }
    }
}