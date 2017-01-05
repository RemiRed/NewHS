using UnityEngine;
/*By Axel Ohrås*/
public class PlayerController : Controller {
    
	void Start () {
        BoardManager board = GameManager.Instance.Board;
        Deck = board.playerDeck;
        Hand = board.playerHand.GetComponent<CardLayout>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
	}

}
