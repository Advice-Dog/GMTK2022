using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    List<Card> cards = new List<Card>();


    // Start is called before the first frame update
    void Start()
    {
     SetDeck();   
    }

    // Adding some default cards to the deck
    void SetDeck() {
        for (int i = 0; i < 10; i++)
        {
            cards.Add(new WarriorCard());
            cards.Add(new MageCard());    
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
