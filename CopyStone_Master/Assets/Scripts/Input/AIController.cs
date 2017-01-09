using UnityEngine;

public class AIController : Controller {
    

    // Use this for initialization
    void Start () {
        BoardManager board = GameManager.Instance.Board;
        Deck = board.opponentDeck;
        Hand = board.opponentHand.GetComponent<CardLayout>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
