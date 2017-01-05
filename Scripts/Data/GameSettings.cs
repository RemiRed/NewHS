using UnityEngine;

namespace Assets.Scripts.Data
{
    [System.Serializable]
    public class GameSettings
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject opponentPrefab;

        [SerializeField] private int cardLimit; //Ammount of cards a player can have in hand
        [SerializeField] private int startCount; //Ammount of cards a player begins with 

        public int CardLimit
        {
            get { return cardLimit; }
        }

        public int StartCount
        {
            get { return startCount; }
        }

        public GameObject Player
        {
            get { return playerPrefab; }
        }

        public GameObject Opponent
        {
            get { return opponentPrefab; }
        }
    }
}
