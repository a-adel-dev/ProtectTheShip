using com.ARTillery.Combat;
using UnityEngine;

namespace com.ARTillery.Control
{
    public class PlayerCombatState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private CombatTarget _target;
        public PlayerCombatState(PlayerBehavior player)
        {
            _player = player;
            
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log("Entering Combat State");
        }

        public override void UpdateState()
        {

        }

        public override void ExitState()
        {

        }

        public void AssignCombatTarget(CombatTarget target)
        {
            _target = target;
        }

        public override string ToString()
        {
            return "Combat";
        }
    }
}