using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardData : MonoBehaviour
{
    /*By Björn Andersson*/

    public Ability[] abilities;

    [SerializeField]
    protected int manaCost, rarity;

    [SerializeField]
    protected string cardName;

    [SerializeField]
    protected Text manaCostText, cardText, nameText;

    [SerializeField]
    protected Image frame, portrait, dragonImage;

    [SerializeField]
    protected Image[] gems;

    protected Image gem;

    protected AbilityManager abilityManager;

    protected HeroScript hero;

    protected ClickStateManager clickStateManager;

    public HeroScript Hero
    {
        get { return this.hero; }
        set { this.hero = value; }
    }

    public int ManaCost
    {
        get { return manaCost; }
        set { this.manaCost = value; }
    }

    public int Rarity
    {
        get { return this.rarity; }
    }

    public string CardName
    {
        get { return this.cardName; }
    }

    protected virtual void Start()
    {
        clickStateManager = GameManager.Instance.GetComponent<ClickStateManager>();
        abilityManager = GameManager.Instance.GetComponent<AbilityManager>();
        switch (rarity) //Ser till att kortet har rätt gem för sin rarity
        {
            case 0:
                gem = null;
                break;

            case 1:
                gem = gems[0];
                break;

            case 2:
                gem = gems[1];
                break;

            case 3:
                gem = gems[2];
                break;

            case 4:
                gem = gems[3];
                dragonImage.enabled = true;
                break;

            default:
                break;
        }
        if (gem)
        {
            gem.enabled = true;
        }        
    }

    protected virtual void PlayThis()
    {
        this.Hero.PlayCard(this);
    }

    protected void OnClick()
    {
        clickStateManager.CardClicked(this);
    }
}