using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellGenerator : MonoBehaviour
{
    //for spells
    public Text spellMana;
    public Text spellName;
    public Text spellDescription;

    void Start()
    {
        spellMana.text = "2";
        spellName.text = "Jooooooe";
        spellDescription.text = "Hejhejhejhejhej";
    }

	
}
