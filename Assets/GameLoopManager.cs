using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public GameObject prefab;

    private Deck deck;

    private bool isFirstUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!");

        GameObject obj = new GameObject("Deck");
        deck = obj.AddComponent<Deck>();
        Debug.Log("Created the players deck!");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstUpdate)
        {
            Debug.Log("First update, setting player's hand.");
            isFirstUpdate = false;
            deck.SetHand();

            GameObject plane = GameObject.Find("Hand Plane");
            Vector3 position = plane.transform.position;

            position.x -= 0.09f * 2f;

            List<Card> hand = deck.GetHand();
            for (int i = 0; i < hand.Count; i++)
            {
                Instantiate(prefab,
                position,
                Quaternion.Euler(new Vector3(0, -90, 0)));
                position.x += 0.09f;
            }
        }
    }
}
