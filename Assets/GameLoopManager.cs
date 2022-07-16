using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoopManager : MonoBehaviour
{
    public GameObject prefab;

    public GameObject pawnPrefab;

    public GameObject roomPrefab;

    private Deck deck;

    private static int GAME_STATE_BLANK = -1;

    private static int GAME_STATE_SLIDING_IN = 0;

    private static int GAME_STATE_PLAYING = 1;

    private static int GAME_STATE_SLIDING_OUT = 2;

    private static float ROOM_SLIDE_SPEED = 3.0f;

    private int gameState = GAME_STATE_BLANK;

    private Pawn activePawn = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!");

        GameObject obj = new GameObject("Deck");
        deck = obj.AddComponent<Deck>();
        Debug.Log("Created the players deck!");
    }

    void Init()
    {
        if (deck.IsHandEmpty())
        {
            activePawn = null;
            SpawnHand();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameLoopManager.GAME_STATE_BLANK:
                gameState = GameLoopManager.GAME_STATE_SLIDING_IN;
                SpawnRoom();
                break;
            case GameLoopManager.GAME_STATE_SLIDING_IN:
                MoveRoom("Room Center");
                break;
            case GameLoopManager.GAME_STATE_PLAYING:
                Init();
                break;
            case GameLoopManager.GAME_STATE_SLIDING_OUT:
                MoveRoom("Room Despawner");
                break;
        }

        //Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }

    void SpawnHand()
    {
        deck.SetHand();

        GameObject plane = GameObject.Find("Hand Plane");
        Vector3 position = plane.transform.position;

        float cardWidth = 0.12f;
        float cardSpacer = 0.02f;

        position.x -= (cardWidth + cardSpacer) * 2f;

        List<Card> hand = deck.GetHand();
        for (int i = 0; i < hand.Count; i++)
        {
            Card card = hand[i];

            GameObject obj =
                Instantiate(prefab,
                position,
                Quaternion.Euler(new Vector3(0, -270, 30)));

            obj.name = card.ToString();
            obj.transform.parent = plane.transform;

            obj.GetComponent<CardContainer>().SetCard(card);

            // Grab the first Text Component
            Text text = obj.GetComponentsInChildren<Text>()[0];
            text.text = card.ToString();

            // for debugging
            obj.GetComponentsInChildren<Text>()[1].text =
                card.GetUniqueId().ToString();

            if (card is SpellCard)
            {
                SpellCard spellCard = (SpellCard) card;

                // no power
                obj.GetComponentsInChildren<Text>()[1].text = "";

                // no power
                obj.GetComponentsInChildren<Text>()[2].text = "";

                // description
                obj.GetComponentsInChildren<Text>()[3].text =
                    spellCard.GetDescription();
            }
            else if (card is PawnCard)
            {
                PawnCard pawnCard = (PawnCard) card;

                // power
                obj.GetComponentsInChildren<Text>()[1].text =
                    pawnCard.attackDamage.ToString();

                // power
                obj.GetComponentsInChildren<Text>()[2].text =
                    pawnCard.maxHealthPoints.ToString();

                // description
                obj.GetComponentsInChildren<Text>()[3].text =
                    pawnCard.GetDescription();
            }

            position.x += cardWidth + cardSpacer;
        }
    }

    void SpawnRoom()
    {
        // Starting point to spawn the room at
        GameObject roomSpawner = GameObject.Find("Room Spawner");
        GameObject obj =
            Instantiate(roomPrefab,
            roomSpawner.transform.position,
            Quaternion.Euler(new Vector3(0, 0, 0)));
        obj.name = "Room";
    }

    void DestroyRoom()
    {
        Destroy(GameObject.Find("Room"));
    }

    void MoveRoom(string target)
    {
        GameObject room = GameObject.Find("Room");
        GameObject roomCenter = GameObject.Find(target);

        if (room.transform.position == roomCenter.transform.position)
        {
            if (target == "Room Center")
            {
                gameState = GameLoopManager.GAME_STATE_PLAYING;
            }
            else
            {
                DestroyRoom();
                gameState = GameLoopManager.GAME_STATE_BLANK;
            }

            return;
        }

        float movement = ROOM_SLIDE_SPEED * Time.deltaTime;

        Transform transform = room.transform;
        transform.position =
            Vector3
                .MoveTowards(transform.position,
                roomCenter.transform.position,
                movement);
    }

    void OnMouseClick()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null)
            {
                GameObject target =
                    raycastHit.transform.gameObject.transform.parent.gameObject;
                if (target.tag == "Card")
                {
                    Card card = target.GetComponent<CardContainer>().GetCard();
                    if (card is SpellCard)
                    {
                        CastSpell((SpellCard) card);
                    }
                    else if (card is PawnCard)
                    {
                        SpawnPawn (card);
                    }
                    deck.RemoveCard (card);

                    Destroy (target);

                    // todo: remove, just for testing, allow the user to end their turn on their own.
                    if (deck.IsHandEmpty())
                    {
                        gameState = GameLoopManager.GAME_STATE_SLIDING_OUT;
                    }
                }
            }
        }
    }

    void SpawnPawn(Card card)
    {
        Debug.Log("Player is playing a pawn card.");
        if (activePawn != null)
        {
            Debug
                .Log("Cannot spawn another pawn when there is an active pawn.");
            return;
        }
        GameObject HeroSpawns = GameObject.Find("HeroSpawns");

        // just use the 1st element
        Transform child = HeroSpawns.transform.GetChild(0);

        Vector3 position = child.position;
        position.y += 0.1f;

        GameObject obj =
            Instantiate(pawnPrefab,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0)));

        obj.transform.parent = HeroSpawns.transform;

        obj.GetComponent<CardContainer>().SetCard(card);

        // todo: should create based on the card
        activePawn = new Pawn();
    }

    void CastSpell(SpellCard card)
    {
        Debug.Log("Player is playing a spell card.");
        if (activePawn == null)
        {
            Debug.Log("Cannot cast a spell when there is no active pawn.");
            return;
        }

        card.ApplyEffect (activePawn);
    }
}
