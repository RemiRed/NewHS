using UnityEngine;

//Component for hanling board related behaviour
public class BoardManager : MonoBehaviour {

    [SerializeField] private GameObject gameMode; //The gameMode prefab

    public DeckComponent playerDeck;
    public DeckComponent opponentDeck;

    public CardLayout playerHand;
    public CardLayout opponentHand;

    public CardLayout playerDropzone;
	void Start () {
        Instantiate(gameMode);
        GameManager.Instance.Board = this;
        GameManager.Instance.GameMode = gameMode.GetComponent<GameMode>();
	}
}
