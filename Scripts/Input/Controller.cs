using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Controller : MonoBehaviour {

    [SerializeField] private float drawSpeed;
    [SerializeField] private RuntimeAnimatorController animatorOverride;

    private List<GameObject> cardsInHand = new List<GameObject>();
    private DeckComponent deck;
    private CardLayout hand;

	public void StartTurn(){
        DrawCard();
	}
		
	public void PlayCard(){
	}

    public void DrawCard()
    {
        //TODO FIX THIS
        //GameObject card = GameManager.Instance.GetCard(deck.GetTop());
        GameObject cardPrefab = GameManager.Instance.GetCard(2);
        GameObject card = Instantiate(cardPrefab);
        CardComponent cardComponent = card.GetComponent<CardComponent>();
        cardComponent.Player = this;

        if (animatorOverride != null)
        {
            Animator cardAnimator = card.GetComponent<Animator>();
            cardAnimator.runtimeAnimatorController = animatorOverride;
        }
    }

    private IEnumerator DrawCards(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(drawSpeed);
        }
    }

    public void RequestCards(int ammount)
    {
        StartCoroutine(DrawCards(ammount));
    }

    public DeckComponent Deck
    {
        get { return deck; }
        set { deck = value; }
    }

    public CardLayout Hand
    {
        get { return hand; }
        set { hand = value; }
    }
}
