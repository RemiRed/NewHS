using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    /*By Björn Andersson*/

    public List<CardData> /*deck, */hand, discardPile;
    public List<MinionCard> minionsInPlay;
    public WeaponCard equippedWeapon;

    [SerializeField]
    protected Button abilityButton;

    [SerializeField]
    protected Image heroPortrait, abilityImage, equippedCardImage;

    [SerializeField]
    protected Text heroAbilityName, heroAbilityText, healthText, attackPowerText, equippedWeaponAttackText, equippedWeaponUsesText;

    protected int health, attackPower, manaCrystals, maxManaCrystals;
    protected string heroName;
    protected bool hasAttacked, usedAbility = false;
    protected HeroScript opponent;
    protected AbilityManager abilityManager;
    protected ClickStateManager clickStateManager;


    public int AvailableMana
    {
        get { return this.manaCrystals; }
    }

    public bool HeroCanAttack
    {
        get
        {
            if (this.attackPower > 0 && !hasAttacked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool HasAttacked
    {
        get { return this.hasAttacked; }
        set { this.hasAttacked = value; }
    }

    public int SpellDamage
    {
        get;
        set;
    }

    public List<CardData> Hand
    {
        get { return this.hand; }
    }

    public HeroScript Opponent
    {
        get { return this.opponent; }
        set { this.opponent = value; }
    }

    protected virtual void Start() //Hittar abilitymanager och opponent
    {
        manaCrystals = 100;
        maxManaCrystals = 100;
        clickStateManager = GameManager.Instance.GetComponent<ClickStateManager>();
        abilityManager = GameManager.Instance.GetComponent<AbilityManager>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player != this && player.GetComponent<HeroScript>())
            {
                opponent = player.GetComponent<HeroScript>();
                break;
            }
        }
    }

    public void PlayCard(CardData card) //Ser till att kort spelas korrekt beroende på sin typ
    {
        if (card is WeaponCard)
        {
            equippedWeapon = (WeaponCard)card;
        }
        else if (card is MinionCard)
        {
            this.minionsInPlay.Add((MinionCard)card);

            foreach (Ability abilityEffect in (card.abilities))
            {
                if (abilityEffect == Ability.Battlecry)
                {
                    abilityManager.Battlecry((MinionCard)card);
                }
                else if (abilityEffect == Ability.Secret)
                {
                    ((Hunter)this).Secrets.Add(card.CardName);
                }
            }
        }
        else if (card is SpellCard)
        {
            this.discardPile.Add(card);
        }
        this.hand.Remove(card);
    }

    public void TooManyMinionsCheck(MinionCard minion) //Tar bort en minion om det är för många minions i spel
    {
        if (this.minionsInPlay.Count > 7)
        {
            this.discardPile.Add(minion);
            minionsInPlay.Remove(minion);
        }
    }

    public void StartTurn() //Kallas när spelaren påbörjar sin tur
    {
        clickStateManager.ActivePlayer = this;
        if (maxManaCrystals < 10)
        {
            maxManaCrystals++;
        }
        manaCrystals = maxManaCrystals;
        foreach (MinionCard minion in this.minionsInPlay)
        {
            minion.CanAttack = true;

            foreach (Ability abilityEffect in minion.abilities)
            {
                if (abilityEffect == Ability.Windfury)
                {
                    minion.WindfuryActive = true;
                }
            }
        }
    }

    public virtual void HeroAbility() //Aktiverar spelarens ability och triggar eventuella Inspiretriggers
    {
        this.usedAbility = true;
        foreach (MinionCard minion in minionsInPlay)
        {
            foreach (Ability abilityEffect in minion.abilities)
            {
                if (abilityEffect == Ability.Inspire)
                {
                    abilityManager.Inspire(minion);
                }
            }
        }
    }

    public void Combat(GameObject target) //Anfaller en minion eller spelare
    {
        if (target.GetComponent<MinionCard>())
        {
            if (this.equippedWeapon)
            {
                target.GetComponent<MinionCard>().HealthChange((this.attackPower + this.equippedWeapon.WeaponAttack));
            }
            else
            {
                target.GetComponent<MinionCard>().HealthChange(this.attackPower);
            }
        }
        else if (target.CompareTag("Player"))
        {
            target.GetComponent<HeroScript>().LifeChange(this.attackPower);

            if (Opponent.GetComponent<Hunter>())
            {
                foreach (string secretName in Opponent.GetComponent<Hunter>().Secrets)
                {
                    if (secretName.ToUpper() == "ANDVARI'S RING")
                    {
                        abilityManager.Secret(secretName);
                        (Opponent as Hunter).Secrets.Remove(secretName);
                        LifeChange(5);
                    }
                }
            }
        }
        if (equippedWeapon)
        {
            this.equippedWeapon.UseWeapon();
        }
    }

    public virtual void LifeChange(int value) //Skadar och healar spelaren
    {
        if (value < 0 && this.health - value > 30)
        {
            this.health = 30;
        }
        else
        {
            this.health -= value;
        }
    }

    protected bool DeathCheck() //Kollar om spelaren fortfarande lever
    {
        if (this.health <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /*
    protected void DrawCard() //Drar kort
    {
        int deckIndex = Random.Range(0, this.deck.Count);
        Card drawnCard = this.deck[deckIndex];
        this.hand.Add(drawnCard);
        this.deck.Remove(drawnCard);
    }
    */
}

public class Hunter : HeroScript
{
    public List<string> Secrets
    {
        get;
        set;
    }

    protected override void Start()
    {
        base.Start();
        heroAbilityName.text = "Steady Shot";
        heroAbilityText.text = "Deal 2 damage to the enemy hero.";
    }

    public override void HeroAbility() //Gör 2 skada på motståndaren
    {
        base.HeroAbility();
        opponent.LifeChange(2);
    }
}

public class Shaman : HeroScript
{
    protected GameObject[] totems;

    public int OverloadToPay
    {
        get;
        set;
    }

    protected override void Start()
    {
        base.Start();
        OverloadToPay = 0;
        heroAbilityName.text = "Totemic Call";
        heroAbilityText.text = "Summon a random totem";
    }

    public override void HeroAbility() //Summonar ett random totem
    {
        base.HeroAbility();
        MinionCard totem = Instantiate(totems[Random.Range(0, totems.Length)]).GetComponent<MinionCard>();
        minionsInPlay.Add(totem);
        totem.Summon();
        TooManyMinionsCheck(totem);
    }
}

public class Warrior : HeroScript
{
    int armor;
    public int Armor
    {
        get { return this.armor; }
        set { armor += value; }
    }
    protected override void Start()
    {
        base.Start();
        heroAbilityName.text = "Armor Up!";
        heroAbilityText.text = "Gain 2 Armor";
    }

    public override void LifeChange(int value)
    {
        if (value < 0 && this.health - value > 30)
        {
            this.health = 30;
        }
        else if (armor > 0)
        {
            armor -= value;
            if (armor < 0)
            {
                health -= armor;
                armor = 0;
            }
        }
        else
        {
            this.health -= value;
        }
    }

    public override void HeroAbility() //Ger spelaren 2 armor
    {
        base.HeroAbility();
        this.Armor = 2;
    }
}
