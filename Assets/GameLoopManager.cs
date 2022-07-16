using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public GameObject prefab;

    public GameObject pawnPrefab;

    private Deck deck;

    private bool isFirstUpdate = true;

    private int pawnsSpawned = 0;

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

            float cardWidth = 0.12f;
            float cardSpacer = 0.02f;

            position.x -= (cardWidth + cardSpacer) * 2f;

            List<Card> hand = deck.GetHand();
            for (int i = 0; i < hand.Count; i++)
            {
                GameObject obj =
                    Instantiate(prefab,
                    position,
                    Quaternion.Euler(new Vector3(0, -270, 30)));

                obj.name = hand[i].ToString();
                obj.transform.parent = plane.transform;

                position.x += cardWidth + cardSpacer;
            }
        }

        //Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    string name =
                        raycastHit.transform.gameObject.transform.parent.name;

                    //Our custom method.
                    Debug
                        .Log("Name: " +
                        name +
                        ", Tag: " +
                        raycastHit.collider.tag);

                    GameObject target = GameObject.Find(name);

                    //CurrentClickedGameObject(raycastHit.transform.gameObject);
                    if (target.tag == "Card")
                    {
                        List<Card> hand = deck.GetHand();

                        int index = 0;

                        // find which card they selected
                        Card card = hand[index];
                        if (card is SpellCard)
                        {
                            Debug.Log("Player is playing a spell card.");
                            // todo: allow the player to select the target
                        }
                        else if (card is PawnCard)
                        {
                            Debug.Log("Player is playing a pawn card.");

                            // todo: spawn a pawn on the board
                            SpawnPawn();
                        }
                        deck.RemoveCard (index);

                        Destroy (target);
                    }
                }
            }
        }
    }

    void SpawnPawn()
    {
        GameObject HeroSpawns = GameObject.Find("HeroSpawns");
        Transform child = HeroSpawns.transform.GetChild(pawnsSpawned);

        Vector3 position = child.position;
        position.y += 0.1f;

        GameObject obj =
            Instantiate(pawnPrefab,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0)));

        pawnsSpawned++;
    }
}
