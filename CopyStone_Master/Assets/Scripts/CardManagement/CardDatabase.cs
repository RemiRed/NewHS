using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private GameObject weaponPrefab;

    private Dictionary<int, GameObject> cardCache;

    public void Load()
    {
        cardCache = new Dictionary<int, GameObject>();

        XElement root = XElement.Load(Application.streamingAssetsPath + path);

        XElement weapons = root.Element("weapons");
        XElement spells = root.Element("spells");
        XElement minions = root.Element("minions");

        foreach (XElement element in weapons.Elements())
        {
            int id = int.Parse(element.Attribute("id").Value);
            GameObject card = CreateWeapon(element);
            cardCache.Add(id, card);
        }

        foreach (XElement element in spells.Elements())
        {
            int id = int.Parse(element.Attribute("id").Value);
            GameObject card = CreateSpell(element);
            cardCache.Add(id, card);
        }

        foreach (XElement element in minions.Elements())
        {
            int id = int.Parse(element.Attribute("id").Value);
            GameObject card = CreateMinion(element);
            cardCache.Add(id, card);
        }

    }

    private GameObject CreateMinion(XElement element)
    {
        GameObject card = minionPrefab;
        MinionCard data = card.GetComponent<MinionCard>();  
        data.name = element.Element("name").Value;
        data.ManaCost = int.Parse(element.Element("manaCost").Value);
        //TODO ADD MORE SUPPORT
        return card;
    }
    private GameObject CreateSpell(XElement element)
    {
        GameObject card = spellPrefab;
        SpellCard data = card.GetComponent<SpellCard>();
        data.name = element.Element("name").Value;
        data.ManaCost = int.Parse(element.Element("manaCost").Value);
        //TODO ADD MORE SUPPORT
        return card;
    }
    private GameObject CreateWeapon(XElement element)
    {
        GameObject card = weaponPrefab;
        WeaponCard data = card.GetComponent<WeaponCard>();
        data.name = element.Element("name").Value;
        data.ManaCost = int.Parse(element.Element("manaCost").Value);
        data.WeaponAttack = int.Parse(element.Element("attack").Value);
        //TODO ADD MORE SUPPORT
        return card;
    }

    /// <summary>
    /// Creates and returns a card
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject GetCard(int id)
    {
        if (cardCache.ContainsKey(id))
        {       
            return cardCache[id];
        }
        return null;
    }
}

