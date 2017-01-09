using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Controller : MonoBehaviour {

    [SerializeField] private float drawSpeed;
    [SerializeField] private RuntimeAnimatorController animatorOverride;

    private List<GameObject> cardsInHand = new List<GameObject>();
    private DeckComponent deck;
    private CardLayout hand;
    private CardLayout dropzone;

    protected Transform selectedCard;

	public void StartTurn(){
        DrawCard();
	}
		
	public void PlayCard(){
	}

    public void DrawCard()
    {
        //TODO FIX THIS
        //GameObject card = GameManager.Instance.GetCard(deck.GetTop());
        GameObject card = Instantiate(GameManager.Instance.GetCard(2));
        CardComponent cardComponent = card.GetComponent<CardComponent>();
        cardComponent.Player = this;

        if (animatorOverride != null)
        {
            Animator cardAnimator = card.GetComponent<Animator>();
            cardAnimator.runtimeAnimatorController = animatorOverride;
        }
    }

    public void DrawCard(int id)
    {

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

    public void Attack(CardComponent selected, CardComponent target)
    {
        if(selected.Player != target.Player)
        {
            
            CombatManager combat = GameManager.Instance.GameMode.GetComponent<CombatManager>();
            StartCoroutine(combat.Attack(selected.transform, target.transform));
            
        }
    }
    
    public void PlayCard(CardComponent card)
    {
        if (card.Player == this)
        {
            MinionCard data = card.transform.GetComponent<MinionCard>();

            if (data != null)
            {
                CardLayout minions = dropzone;
                if (minions.Contains(card.transform))
                {
                    selectedCard = card.transform;

                }
                else
                {
                    dropzone.AddToLayout(card.transform);
                }
            }
        }
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

    public CardLayout Dropzone
    {
        get { return dropzone; }
        set { dropzone = value; }
    }
}
