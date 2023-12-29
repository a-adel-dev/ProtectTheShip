using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IWeapon
    {
        void ActivateRay(Vector3 target);
        void DeactivateRay();

        void UpdateRay(Vector3 target);
    }
}
