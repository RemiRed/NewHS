using UnityEngine;
using Assets.Scripts.Data;

namespace Assets.Scripts.StateManagement
{
    class GameSetup : IStateBase
    {
        private GameManager gameManager;

        private Controller[] players;

        public GameSetup(Controller[] players)
        {
            this.players = players;
        }

        public void BeginState()
        {
            gameManager = GameManager.Instance;

            GameMode gameMode = gameManager.GameMode;
            GameSettings settings = gameManager.Settings;

            foreach(Controller player in players)
            {
                player.RequestCards(5);
            }
            Controller firstPlayer = RandomFirst(players);

            StateManager.Instance.SwitchState(new PlayState(firstPlayer));
        }

        private Controller RandomFirst(Controller[] players)
        {
            int first = Random.Range(0, players.Length - 1);
            return players[first];
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
            return "Setup";
        }
    }
}
