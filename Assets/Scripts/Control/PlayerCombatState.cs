using com.ARTillery.Combat;
using UnityEngine;

namespace com.ARTillery.Control
{
    public class PlayerCombatState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private Fighter _fighter;
        public PlayerCombatState(PlayerBehavior player)
        {
            _player = player;
            
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log("Entering Combat State");
            if (_fighter is null)
            {
                _fighter = _player.Fighter;
            }
            
        }

        public override void UpdateState()
        {

        }

        public override void ExitState()
        {

        }

        public override string ToString()
        {
            return "Combat";
        }
    }
}