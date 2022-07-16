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

    private int pawnsSpawned = 0;

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
            pawnsSpawned = 0;
            SpawnHand();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameLoopManager.GAME_STATE_BLANK)
        {
            gameState = GameLoopManager.GAME_STATE_SLIDING_IN;
            SpawnRoom();
        }
        else if (gameState == GameLoopManager.GAME_STATE_SLIDING_IN)
        {
            MoveRoom("Room Center");
        }
        else if (gameState == GameLoopManager.GAME_STATE_PLAYING)
        {
            Init();
        }
        else if (gameState == GameLoopManager.GAME_STATE_SLIDING_OUT)
        {
            MoveRoom("Room Despawner");
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
            GameObject obj =
                Instantiate(prefab,
                position,
                Quaternion.Euler(new Vector3(0, -270, 30)));

            obj.name = i.ToString() + ' ' + hand[i].ToString();
            obj.transform.parent = plane.transform;

            // Grab the first Text Component
            Text text = obj.GetComponentsInChildren<Text>()[0];
            text.text = hand[i].ToString();

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
                string name =
                    raycastHit.transform.gameObject.transform.parent.name;

                //Our custom method.
                Debug
                    .Log("Name: " + name + ", Tag: " + raycastHit.collider.tag);

                GameObject target = GameObject.Find(name);

                //CurrentClickedGameObject(raycastHit.transform.gameObject);
                if (target.tag == "Card")
                {
                    List<Card> hand = deck.GetHand();

                    // taking the positon from the name
                    int index = Int32.Parse(name.Split(' ')[0]);

                    // find which card they selected
                    Card card = hand[index];
                    if (card is SpellCard)
                    {
                        CastSpell((SpellCard) card);
                    }
                    else if (card is PawnCard)
                    {
                        SpawnPawn (card);
                    }
                    deck.RemoveCard (index);

                    Destroy (target);

                    if (deck.IsHandEmpty())
                    {
                        // todo: remove, just for testing, allow the user to end their turn on their own.
                        gameState = GameLoopManager.GAME_STATE_SLIDING_OUT;
                    }
                }
            }
        }
    }

    void SpawnPawn(Card card)
    {
        Debug.Log("Player is playing a pawn card.");
        GameObject HeroSpawns = GameObject.Find("HeroSpawns");
        Transform child = HeroSpawns.transform.GetChild(pawnsSpawned);

        Vector3 position = child.position;
        position.y += 0.1f;

        GameObject obj =
            Instantiate(pawnPrefab,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0)));

        obj.transform.parent = HeroSpawns.transform;

        // todo: should create based on the card
        activePawn = new Pawn();

        pawnsSpawned++;
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
