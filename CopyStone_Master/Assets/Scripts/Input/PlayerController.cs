using UnityEngine;
/*By Axel Ohrås*/
public class PlayerController : Controller {

    private BoardManager board;

	void Start () {
        board = GameManager.Instance.Board;
        Deck = board.playerDeck;
        Hand = board.playerHand.GetComponent<CardLayout>();
        Dropzone = board.playerDropzone;
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (objectHit.tag == "Card")
                {
                    if(objectHit != selectedCard && selectedCard != null)
                    {
                        Attack(selectedCard.GetComponent<CardComponent>(), objectHit.GetComponent<CardComponent>());
                    }
                    else
                    {
                        PlayCard(objectHit.GetComponent<CardComponent>());
                    }
                    
                }
            }
        }
    }
}
