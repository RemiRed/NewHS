using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : CardData
{
    /*By Björn Andersson*/

    [SerializeField]
    int maxStrength, maxUses;

    [SerializeField]
    Text strengthText, usesText;

    int weaponAttack, uses;

    public int Uses
    {
        get { return uses; }
        set { uses -= value; }
    }

    public int WeaponAttack
    {
        get { return this.weaponAttack; }
        set { this.weaponAttack = value; }
    }

    public GameObject GetWeapon()
    {
        return this.gameObject;
    }

    public void UseWeapon() //När man slåss med sitt vapen
    {
        uses--;
        usesText.text = uses.ToString();
        if (uses <= 0)
        {
            hero.discardPile.Add(this);
            hero.equippedWeapon = null;
            //Move to graveyard
        }
    }
}