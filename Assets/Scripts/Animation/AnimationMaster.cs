using com.ARTillery.Interfaces;

using UnityEngine;

namespace com.ARTillery.Animation
{
    public class AnimationMaster : MonoBehaviour, IAnimationMaster
    {
        private Animator _animator;

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetCombatMotion(bool state)
        {
            _animator.SetBool("isShooting", state);
        }

        public void SetCharacterSpeed(float speed)
        {
            _animator.SetFloat("speed", speed);
        }

        public void SetGatheringMotion(bool state)
        {
            _animator.SetBool("isHarvesting", state);
        }

        public void DisableBooleanParameters()
        {
            if (_animator != null) { return; }
            AnimatorControllerParameter[] parameters = _animator.parameters;

            foreach (AnimatorControllerParameter parameter in parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    _animator.SetBool(parameter.name, false);
                }
            }
        }

    }
}