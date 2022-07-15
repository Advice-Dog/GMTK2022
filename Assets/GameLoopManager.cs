using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    private Deck deck;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject("Deck");
        deck = obj.AddComponent<Deck>();
        Debug.Log("Created the players deck!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
