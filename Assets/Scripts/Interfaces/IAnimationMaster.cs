using UnityEngine;

namespace com.ARTillery.Interfaces
{
    public interface IAnimationMaster
    {
        void SetCharacterSpeed(float speed);

        void SetGatheringMotion(bool state);
        void SetCombatMotion(bool state);

        void SetAnimator(Animator animator);
    }
}
