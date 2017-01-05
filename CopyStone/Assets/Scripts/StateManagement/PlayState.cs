using UnityEngine;

namespace Assets.Scripts.StateManagement
{
    class PlayState : IStateBase
    {
        public PlayState(Controller firstPlayer)
        {
            firstPlayer.StartTurn();
        }

        public void BeginState()
        {
           
        }

        public void DestroyState()
        {
            
        }

        public void OnSceneLoaded()
        {
            
        }

        public void UpdateState()
        {
            
        }

        public string GetName()
        {
            return "Play";
        }
    }
}
