using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static int MAX_DRAW_COUNT = 5;

    List<Card> cards = new List<Card>();

    List<Card> drawPile = new List<Card>();

    List<Card> hand = new List<Card>();

    List<Card> discardPile = new List<Card>();

    public Deck()
    {
        SetDeck();
        SetDrawPile();
    }

    // Adding some default cards to the deck
    void SetDeck()
    {
        for (int i = 0; i < 10; i++)
        {
            cards.Add(new WarriorCard());
            cards.Add(new MageCard());
            cards.Add(new StrengthCard());
        }
    }

    public void SetHand()
    {
        drawPile = drawPile.OrderBy(a => Random.value).ToList();
        DrawCards();
    }

    void SetDrawPile()
    {
        // Copy the cards to the Draw pile
        drawPile = new List<Card>(cards);
    }

    void DrawCards()
    {
        while (hand.Count < MAX_DRAW_COUNT)
        {
            DrawCard();
        }

        for (int i = 0; i < hand.Count; i++)
        {
            Debug.Log("Drawn Card: " + hand[i]);
        }
    }

    void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            Debug.Log("Shuffling discard pile back into the draw pile.");

            // shuffle the discard pile back into the draw pile
            drawPile = discardPile.OrderBy(a => Random.value).ToList();
            discardPile.Clear();
        }

        // Take the first card, remove it from the draw pile, and add it to the hand
        Card card = drawPile[0];
        drawPile = drawPile.GetRange(1, drawPile.Count - 1);
        hand.Add (card);
    }

    public List<Card> GetHand()
    {
        return hand;
    }
}
