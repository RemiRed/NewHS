using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*By Björn Andersson*/

public enum Ability
{
    None, MindControl, Charge, Battlecry, Windfury, Stealth, Taunt, Deathrattle, Overload, Enrage, Inspire, DivineShield, Secret, Silence
}

public class AbilityManager : MonoBehaviour
{
    //AbilityManager hanterar abilities med olika funktioner beroende på vilket kort abilityn sitter på

    public GameObject draug, blackCat, chickenLeggedHut;
    public GameObject[] babaYagaCopies;

    public void Battlecry(MinionCard minion)
    {
        switch (minion.CardName.ToUpper())
        {
            case "BACKWOOD CRONE":
                GameObject thisBlackCat = GameObject.Instantiate(blackCat);
                minion.Hero.minionsInPlay.Add(thisBlackCat.GetComponent<MinionCard>());
                thisBlackCat.GetComponent<MinionCard>().Summon();
                //heal en ally && silence en enemy
                break;

            case "KRAKEN":
                List<MinionCard> allMinionsInPlay = minion.Hero.minionsInPlay;

                foreach (MinionCard otherMinion in minion.Hero.Opponent.minionsInPlay)
                {
                    allMinionsInPlay.Add(otherMinion);
                }

                foreach (MinionCard otherMinion in allMinionsInPlay)
                {
                    if (otherMinion != minion)
                    {
                        otherMinion.Hero.discardPile.Add(otherMinion);
                        otherMinion.LeaveBattlefield();
                        otherMinion.Hero.minionsInPlay.Remove(otherMinion);
                    }
                }
                break;

            case "BABA YAGA":
                for (int i = 0; i < babaYagaCopies.Length; i++)
                {
                    GameObject thisCopy = GameObject.Instantiate(babaYagaCopies[i]);
                    thisCopy.GetComponent<MinionCard>().Summon();
                }
                foreach (CardData card in minion.Hero.Hand)
                {
                    minion.Hero.discardPile.Add(card);
                    minion.Hero.Hand.Remove(card);
                }
                break;

            case "BABA YAGA COPY 1":
                GameObject firstChickenLeggedHut = GameObject.Instantiate(chickenLeggedHut);
                minion.Hero.minionsInPlay.Add(firstChickenLeggedHut.GetComponent<MinionCard>());
                firstChickenLeggedHut.GetComponent<MinionCard>().Summon();
                minion.Hero.Opponent.LifeChange(5);
                break;

            case "BABA YAGA COPY 2":
                GameObject secondChickenLeggedHut = GameObject.Instantiate(chickenLeggedHut);
                minion.Hero.minionsInPlay.Add(secondChickenLeggedHut.GetComponent<MinionCard>());
                secondChickenLeggedHut.GetComponent<MinionCard>().Summon();
                //silence en enemy
                break;

            case "SPHINX OF A THOUSAND RIDDLES":
                //silence en enemy minion
                break;

            case "SCYLLA":
                List<MinionCard> allMinions = minion.Hero.minionsInPlay;

                foreach (MinionCard otherMinion in minion.Hero.Opponent.minionsInPlay)
                {
                    allMinions.Add(otherMinion);
                }

                foreach (MinionCard otherMinion in allMinions)
                {
                    if (otherMinion != minion && otherMinion.Attack <= 3)
                    {
                        otherMinion.Hero.discardPile.Add(otherMinion);
                        otherMinion.LeaveBattlefield();
                        otherMinion.Hero.minionsInPlay.Remove(otherMinion);
                    }
                }
                break;

            case "CYCLOPS VANGUARD":
                foreach (MinionCard otherMinion in minion.Hero.minionsInPlay)
                {
                    otherMinion.Attack += 2;
                    //ska tas bort i slutet av turen
                }
                break;

            case "MINOTAUR DEFENDER":
                foreach (MinionCard otherMinion in minion.Hero.minionsInPlay)
                {
                    foreach (Ability thisAbility in otherMinion.abilities)
                    {
                        if (thisAbility == Ability.Taunt)
                        {
                            minion.Hero.gameObject.GetComponent<Warrior>().Armor += 1;
                        }
                    }
                }
                break;
        }
    }

    public void Deathrattle(MinionCard minion)
    {
        switch (minion.CardName.ToUpper())
        {
            case "ANGERED NISSE":
                MinionCard minionToShuffle;
                minionToShuffle = minion.Hero.minionsInPlay[Random.Range(0, minion.Hero.minionsInPlay.Count)];
                //minion.Hero.deck.Add(minionToShuffle);
                minion.Hero.minionsInPlay.Remove(minionToShuffle);
                break;

            case "RECKLESS SAILOR":
                GameObject.Instantiate(draug);
                draug.GetComponent<MinionCard>().Hero = minion.Hero;
                minion.Hero.minionsInPlay.Add(draug.GetComponent<MinionCard>());
                draug.GetComponent<MinionCard>().Summon();
                break;

            case "DRYAD FOREST":
                foreach (MinionCard otherMinion in minion.Hero.minionsInPlay)
                {
                    if (otherMinion.name.ToUpper() == "DRYAD")
                    {
                        otherMinion.Hero.discardPile.Add(otherMinion);
                        otherMinion.LeaveBattlefield();
                        otherMinion.Hero.minionsInPlay.Remove(otherMinion);
                    }
                }
                break;
        }
    }

    public void Secret(string cardName)
    {
        switch (cardName.ToUpper())
        {
            case "ANDVARI'S RING":
                
                break;
        }
    }

    public void Inspire(MinionCard minion)
    {
        switch (minion.name.ToUpper())
        {
            case "":

                break;
        }
    }

    public void Enrage(MinionCard minion)
    {
        switch (minion.CardName.ToUpper())
        {
            case "ANGRY TROLL":
                minion.Attack *= 2;
                break;
        }
    }

    public void Overload(CardData card)
    {
        int overloadCost = 0;
        switch (card.CardName.ToUpper())
        {
            case "BABA YAGA":
                overloadCost = 10;
                break;
        }
        card.Hero.gameObject.GetComponent<Shaman>().OverloadToPay += overloadCost;
    }
}