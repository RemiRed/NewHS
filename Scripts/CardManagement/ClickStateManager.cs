using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*By Björn Andersson*/
public class ClickStateManager : MonoBehaviour
{
    CombatManager combatManager;
    WiggleWiggleWiggle currentCard;

    public static SpellCard SpellToResolve
    {
        get;
        set;
    }

    public static class AbilityToResolve
    {
        public static string CardName
        {
            get;
            set;
        }

        public static Ability Ability
        {
            get;
            set;
        }
    }

    public HeroScript ActivePlayer
    {
        get;
        set;
    }

    public HeroScript ClickedAndActivePlayer
    {
        get;
        set;
    }

    public CardData ActiveCard
    {
        get;
        set;
    }

    void Start()
    {
        //ActivePlayer = GameObject.Find("Hero").GetComponent<HeroScript>();  //temporary
        //combatManager = GameObject.Find("GameManager").GetComponent<CombatManager>();
        AbilityToResolve.Ability = Ability.None;
    }

    public void CardClicked(CardData clickedCard)
    {
        if (AbilityToResolve.Ability != Ability.None)
        {
            //checka om legalt target och isf deklarera clickedCard som target
        }
        else if (AbilityToResolve.Ability == Ability.None)
        {
            if (SpellToResolve)
            {
                //checka om legalt target och isf deklarera clickedCard som target
            }
            else if (!SpellToResolve)
            {
                if (!ActiveCard && clickedCard.Hero == ActivePlayer)
                {
                    print("tjena");
                    if (currentCard)
                    {
                        currentCard.ActiveCard = false;
                    }
                    ActiveCard = clickedCard;
                    ActiveCard.GetComponent<WiggleWiggleWiggle>().ActiveCard = true;
                    return;
                }
                else if (!ActiveCard && clickedCard.Hero != ActivePlayer && ActivePlayer == ClickedAndActivePlayer && ClickedAndActivePlayer.HeroCanAttack)
                {
                    ClickedAndActivePlayer.Combat(clickedCard.gameObject);
                    ClickedAndActivePlayer.HasAttacked = true;
                }
                else if (ActiveCard is MinionCard)
                {
                    if (ActiveCard.Hero != ActivePlayer)
                    {
                        if ((ActiveCard as MinionCard).CanAttack)
                        {
                            if ((clickedCard as MinionCard).CanBeAttacked)
                            {
                                bool tauntExists = false;
                                foreach (MinionCard minion in clickedCard.Hero.minionsInPlay)
                                {
                                    foreach (Ability ability in minion.abilities)
                                    {
                                        if (ability == Ability.Taunt)
                                        {
                                            tauntExists = true;
                                            break;
                                        }
                                    }
                                }
                                bool targetIsLegal = false;
                                if (!tauntExists)
                                {
                                    targetIsLegal = true;
                                }
                                else if (tauntExists)
                                {
                                    foreach (Ability ability in (clickedCard.abilities))
                                    {
                                        if (ability == Ability.Taunt)
                                        {
                                            targetIsLegal = true;
                                            break;
                                        }
                                    }
                                    if (!targetIsLegal)
                                    {
                                        //Error message: Minion with Taunt is in the way
                                    }
                                }
                                if (targetIsLegal)
                                {
                                    combatManager.Attack(ActiveCard.transform, clickedCard.transform);
                                }
                            }
                            else if (!(clickedCard as MinionCard).CanBeAttacked)
                            {
                                //Error Message: That minion can't be attacked
                            }
                        }
                        else if (!(ActiveCard as MinionCard).CanAttack)
                        {
                            //Error Message: That minion can't attack
                        }
                    }
                }
            }
        }
        currentCard.ActiveCard = false;
        currentCard = null;
        ActiveCard = null;
        ClickedAndActivePlayer = null;
    }

    public void PlayerClicked(HeroScript clickedPlayer)
    {
        if (AbilityToResolve.Ability != Ability.None)
        {
            //Checka om legalt target och isf deklarera clickedPlayer som target
        }
        else if (AbilityToResolve.Ability == Ability.None)
        {
            if (SpellToResolve)
            {
                //Checka om legalt target och isf deklarera clickedPlayer som target
            }
            else if (!SpellToResolve)
            {
                if (clickedPlayer == ActivePlayer && !ClickedAndActivePlayer)
                {
                    if (!ActiveCard || ActiveCard.Hero == clickedPlayer)
                    {
                        ClickedAndActivePlayer = clickedPlayer;
                        return;
                    }
                }
                else if (ActiveCard && ActiveCard.Hero != clickedPlayer)
                {
                    if ((ActiveCard as MinionCard).CanAttack)
                    {
                        bool tauntExists = false;
                        foreach (MinionCard minion in clickedPlayer.minionsInPlay)
                        {
                            foreach (Ability ability in minion.abilities)
                            {
                                if (ability == Ability.Taunt)
                                {
                                    tauntExists = true;
                                }
                            }
                        }
                        if (!tauntExists)
                        {
                            combatManager.Attack(ActiveCard.transform, clickedPlayer.transform);
                        }
                        else if (tauntExists)
                        {
                            //Error Message: Minion with Taunt is in the way
                        }
                    }
                    else if (!(ActiveCard as MinionCard).CanAttack)
                    {
                        //Error Message: Can't attack
                    }
                }
                else if (!ActiveCard && ClickedAndActivePlayer != clickedPlayer && ClickedAndActivePlayer.HeroCanAttack)
                {
                    combatManager.Attack(ClickedAndActivePlayer.transform, clickedPlayer.transform);
                    ClickedAndActivePlayer.HasAttacked = true;
                }
            }
        }
        currentCard.ActiveCard = false;
        currentCard = null;
        ActiveCard = null;
        ClickedAndActivePlayer = null;
    }
}