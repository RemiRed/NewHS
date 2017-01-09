using UnityEngine;
using Assets.Scripts.Data;
using System.Linq;

namespace Assets.Scripts.StateManagement
{
    class GameSetup : IStateBase
    {
        private GameManager gameManager;

        private Controller[] players;

        private int playersDone = 0;
        private Controller firstPlayer;

        public GameSetup(Controller[] players)
        {
            this.players = players;
        }

        public void BeginState()
        {
            gameManager = GameManager.Instance;

            GameMode gameMode = gameManager.GameMode;
            GameSettings settings = gameManager.Settings;


            int rnd = Random.Range(0, 1);
            firstPlayer = players[0];

            if(rnd == 0)
            {
                players[0].RequestCards(3);
                players[1].RequestCards(4);
                players[1].DrawCard(0); // Draw Coin
            }
            else
            {
                players[1].RequestCards(3);
                players[0].RequestCards(4);
                players[0].DrawCard(0); // Draw Coin
            }   
        }

        public void DestroyState()
        {
            
        }

        public void OnSceneLoaded()
        {
            
        }

        public void UpdateState()
        {
            if(playersDone == 2)
            {
                StateManager.Instance.SwitchState(new PlayState(firstPlayer));
            }
        }

        public void DrawNewCards()
        {
            playersDone++;
        }

        public string GetName()
        {
            return "Setup";
        }
    }
}
