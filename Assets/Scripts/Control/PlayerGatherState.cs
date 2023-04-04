using UnityEngine;

namespace com.ARTillery.Control
{
    public class PlayerGatherState : PlayerBaseState
    {
        private PlayerBehavior _player;
        public PlayerGatherState(PlayerBehavior player)
        {
            _player = player;
        }

        public void EnterState()
        {
            Debug.Log($"Entering Gather State");
            _player.SetCurrentState(this);
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {

        }
    }
}