using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGenerator : MonoBehaviour
{
    // for Weapons
    public Text weaponMana;
    public Text weaponAttack;
    public Text weaponHealth;
    public Text weaponHeroname;
    public Text weaponDescription;

    void Start()
    {
        weaponMana.text = "7";
        weaponAttack.text = "4";
        weaponHealth.text = "9";
        weaponHeroname.text = "flickwrist boom";
        weaponDescription.text = "U did get rekt bruh";
    }



}
