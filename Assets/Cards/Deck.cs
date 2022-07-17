using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static int MAX_PAWN_DRAW_COUNT = 2;

    public static int MAX_SPELLS_DRAW_COUNT = 3;

    List<Card> heros = new List<Card>();

    List<Card> spells = new List<Card>();

    List<Card> hand = new List<Card>();

    public Deck()
    {
        SetDeck();
    }

    // Adding some default cards to the deck
    void SetDeck()
    {
        // Hero Cards
        heros.Add(new HeroW1());
        heros.Add(new HeroR1());
        heros.Add(new HeroM1());
        heros.Add(new HeroW2());
        heros.Add(new HeroR2());
        heros.Add(new HeroM2());

        // Spell Cards
        spells.Add(new Cripple());
        spells.Add(new WingsClipped());
        spells.Add(new Feeble());
        spells.Add(new Fragile());
        spells.Add(new Blind());
        spells.Add(new PlagueCarrier());
    }

    public void DrawPawns()
    {
        hand.Clear();
        heros = heros.OrderBy(a => Random.value).ToList();
        for (int i = 0; i < MAX_PAWN_DRAW_COUNT; i++)
        {
            hand.Add(heros[i]);
        }
    }

    public void DrawSpells()
    {
        hand.Clear();
        spells = spells.OrderBy(a => Random.value).ToList();
        for (int i = 0; i < MAX_SPELLS_DRAW_COUNT; i++)
        {
            hand.Add(spells[i]);
        }
    }

    public void RemoveCard(int index)
    {
        Card card = hand[index];
        hand.RemoveAt (index);
    }

    public void RemoveCard(Card card)
    {
        Debug
            .Log("Removing card: " +
            card.ToString() +
            ", " +
            card.GetUniqueId().ToString());
        for (int i = 0; i < hand.Count; i++)
        {
            if (card.GetUniqueId() == hand[i].GetUniqueId())
            {
                RemoveCard (i);
                break;
            }
        }
    }

    public bool IsHandEmpty()
    {
        return hand.Count == 0;
    }

    public List<Card> GetHand()
    {
        return hand;
    }
}
