using UnityEngine;

public class CardMovement : MonoBehaviour {

    [SerializeField] private float speed;

    private Animator animator;

    private BoardManager board;

    private CardComponent card;
    private DeckComponent deck;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        board = GameManager.Instance.Board;

        card = GetComponent<CardComponent>();
        deck = card.Player.Deck;

        DrawCard();
	}
	
    public void DrawCard()
    {
        transform.position = deck.transform.position;
        animator.SetTrigger("DrawCard");
    }

	// Update is called once per frame
	void Update () {
		if(animator.GetBool("Moving"))
        {
            //TODO implement find spot logic
            Vector3 target = transform.parent.position;
            target.y = transform.position.y;

            Vector3 step = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            step.y = transform.position.y;
            transform.position = step;

            if(Vector3.Distance(transform.position, target) <= 0.1f)
            {
                animator.SetBool("Moving", false);
            }
        }

	}

    //When parent changes make the card move to new parent
    void OnTransformParentChanged()
    {
        animator.SetBool("Moving", true);
    }
}
