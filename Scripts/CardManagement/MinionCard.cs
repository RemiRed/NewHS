using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinionCard : CardData
{
    /*By Björn Andersson*/

    [SerializeField]
    protected int maxAttack, maxHealth;

    [SerializeField]
    protected float deathTimer;

    [SerializeField]
    protected string minionType;

    [SerializeField]
    protected Text attackText, healthText, typeText;

    [SerializeField]
    protected Image typeFrame;

    protected int attack, health;

    protected bool canAttack, stealth, taunt, divineShield = false;

    protected bool canBeAttacked, windfuryActive = true;

    public bool WindfuryActive
    {
        get { return this.windfuryActive; }
        set { this.windfuryActive = value; }
    }

    public bool CanAttack
    {
        get { return this.canAttack; }
        set { this.canAttack = value; }
    }

    public bool CanBeAttacked
    {
        get { return this.canBeAttacked; }
        set { this.canBeAttacked = value; }
    }

    public bool Taunt
    {
        get { return this.taunt; }
    }

    public bool DivineShield
    {
        get { return this.divineShield; }
        set { this.divineShield = value; }
    }

    public int Attack
    {
        get { return this.attack; }
        set
        {
            this.attack = value;
            if (attack < 0)
            {
                attack = 0;
            }
        }
    }

    public int Health
    {
        get { return this.health; }
    }

    protected override void Start()
    {
        base.Start();
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
        if (minionType != "")
        {
            typeFrame.enabled = true;
            typeText.text = minionType;
        }
    }

    public void Summon() //Ser till att allt stämmer när minionen kommer in i spel
    {
        attack = maxAttack;
        health = maxHealth;

        foreach (Ability abilityEffect in this.abilities)
        {
            switch (abilityEffect)
            {
                case Ability.Charge:
                    this.canAttack = true;
                    break;

                case Ability.Stealth:
                    this.canBeAttacked = false;
                    this.stealth = true;
                    break;

                case Ability.Taunt:
                    this.taunt = true;
                    break;

                case Ability.Windfury:
                    this.windfuryActive = true;
                    break;

                default:
                    break;
            }
        }
        hero.TooManyMinionsCheck(this);
    }

    public void LeaveBattlefield() //Gör saker när minionen dör
    {
        foreach (Ability abilityEffect in this.abilities)
        {
            if (abilityEffect == Ability.Deathrattle)
            {
                abilityManager.Deathrattle(this);
            }
        }
    }

    public void HealthChange(int damage)
    {
        if (damage >= 0)
        {
            if (!divineShield)
            {
                this.health -= damage;
                healthText.text = this.health.ToString();
                AliveCheck();
                return;
            }
            else
            {
                this.divineShield = false;
            }
        }
        else
        {
            if (health - damage > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health -= damage;
            }
        }
    }

    void AliveCheck() //Kollar om minionen dör
    {
        if (this.health <= 0)
        {
            StartCoroutine("Death");
        }
    }

    public void Combat(GameObject enemy) //Kallas när minionen anfaller antingen en minion eller spelare
    {
        if (stealth)
        {
            stealth = false;
        }
        if (enemy.GetComponent<CardData>() && enemy.GetComponent<CardData>() is MinionCard)
        {
            MinionCard currentEnemy = enemy.GetComponent<MinionCard>();
            currentEnemy.HealthChange(attack);
            HealthChange(currentEnemy.Attack);
        }
        else if (enemy.CompareTag("Player"))
        {
            //enemy.GetComponent<PlayerScript>().LifeChange(attack);
        }
        if (!windfuryActive)
        {
            this.canAttack = false;
        }
        foreach (Ability abilityEffect in this.abilities)
        {
            if (windfuryActive && abilityEffect == Ability.Windfury)
            {
                this.canAttack = true;
                windfuryActive = false;
            }
        }
        if (Hero.Opponent.GetComponent<Hunter>())
        {
            foreach (string secretName in Hero.Opponent.GetComponent<Hunter>().Secrets)
            {
                if (secretName.ToUpper() == "ANDVARI'S RING")
                {
                    abilityManager.Secret(secretName);
                    (Hero.Opponent as Hunter).Secrets.Remove(secretName);
                    HealthChange(5);
                }
            }
        }
    }

    public virtual void ReturnToHand()
    {
        hero.hand.Add(this);
        hero.minionsInPlay.Remove(this);
    }

    protected override void PlayThis()
    {
        base.PlayThis();
        Summon();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTimer);
        //Move to graveyard
    }

    public void Silence()
    {
        for (int i = 0; i < this.abilities.Length; i++)
        {
            abilities[i] = Ability.None;
        }
    }
}