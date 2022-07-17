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
        SetBaseDeck();
    }

    // Adding some default cards to the deck
    void SetBaseDeck()
    {
        // Hero Cards
        heros.Add(new Tank());
        heros.Add(new Rogue());
        heros.Add(new Hunter());
        heros.Add(new Scout());
        heros.Add(new Necromancer());
        heros.Add(new Mage());

        // Easy Spell Cards
        spells.Add(new Cripple(-25));
        spells.Add(new WingsClipped());

        spells.Add(new Feeble(-10));
        spells.Add(new Fragile(-5));
    }

    public void SetAdvancedDeck()
    {
        spells = new List<Card>();

        // Hard Spell Cards
        spells.Add(new Cripple(-50));
        spells.Add(new WingsClipped());

        spells.Add(new Feeble(-50));
        spells.Add(new Fragile(-99));
        spells.Add(new Blind());
        spells.Add(new PlagueCarrier(2));
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
