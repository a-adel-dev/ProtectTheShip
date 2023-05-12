using UnityEngine;

namespace com.ARTillery.Control
{
    public interface ICursorTarget
    {
        GameObject GetGameObject();

        void SetSelectedVisual();
        void ClearSelectedVisual();
    }
}