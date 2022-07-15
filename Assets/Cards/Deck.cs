using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<Card> cards = new List<Card>();

    public Deck()
    {
        SetDeck();
    }

    // Adding some default cards to the deck
    void SetDeck()
    {
        for (int i = 0; i < 10; i++)
        {
            cards.Add(new WarriorCard());
            cards.Add(new MageCard());
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Debug.Log("Card: " + cards[i]);
        }
    }
}
