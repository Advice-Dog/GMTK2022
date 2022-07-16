using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    private Card card;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }

    public Card GetCard()
    {
        return card;
    }
}
