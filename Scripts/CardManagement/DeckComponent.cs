using UnityEngine;
using System.Collections.Generic;


public class DeckComponent : MonoBehaviour
{
    private Queue<int> cards;

    public int GetTop()
    {
        //Assign card id to fatigue card
        int card_id = 0;

        if(cards.Count > 0)
        {
            card_id = cards.Dequeue();
        }

        return card_id;
    }
}

