using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class HeroGenerator : MonoBehaviour
{
    // for Minions
    public Text minionMana;
    public Text minionAttack;
    public Text minionHealth;
    public Text minionHeroname;
    public Text minionDescription;

    private string testhero = "Jupiter";


    // TO BE ADDED
    /*
     XML syncning med namn som variabler för att kunna byta ut mana/namn/hp etc smidigt

    */

    void Start()
    {
        minionMana.text = "8";
        minionAttack.text = "5";
        minionHealth.text = "3";
        minionHeroname.text = testhero;
        minionDescription.text = "its highnoon sit the fuck down";

    }

}
