using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoopManager : MonoBehaviour
{
    public GameObject prefab;

    public GameObject pawnPrefab;

    public GameObject enemyPrefab;

    public GameObject smokePrefab;

    public GameObject arenaEnemyPrefab;

    public GameObject player;

    public Camera mainCamera;

    public GameObject mainCameraObj;

    public AudioListener audioListener;

    public GameObject battleRoom;

    public Animator deathAnimator;

    public GameObject death;

    AudioSource backgroundMusic;

    public TMPro.TextMeshProUGUI subtitles;

    private int roomIndex = 0;

    private static int MAX_ROOM_COUNT = 3;

    private Deck deck;

    private static int GAME_STATE_BLANK = -999;

    private static int GAME_STATE_SLIDING_IN = -9;

    private static int GAME_STATE_SPAWN_ENEMIES = -8;

    private static int GAME_STATE_SUMMON_PAWN = 0;

    private static int GAME_STATE_PLAYS_SPELL = 1;

    private static int GAME_STATE_BATTLE = 69;

    private static int GAME_STATE_SLIDING_OUT = 9;

    private static int GAME_STATE_ANIMATION = -665;

    private static int GAME_STATE_WAITING = -666;

    private static int ADVANCED_CARDS_ROOM = 5;

    private static float ROOM_SLIDE_SPEED = 3.0f;

    private int gameState = GAME_STATE_BLANK;

    private Pawn activePawn = null;

    private List<Enemy> enemyList = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!");

        // just to allow us to keep battle room visible in editor
        //EndEncouter(true);
        deathAnimator = death.GetComponent<Animator>();

        //audioListener = mainCameraObj.GetComponant<AudioListener>();
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.Play();

        GameObject obj = new GameObject("Deck");
        deck = obj.AddComponent<Deck>();
        Debug.Log("Created the players deck!");

        SetSubtitles("Shall we play a game?");
    }

    void StartTurn()
    {
        Debug.Log(">>> Starting new turn <<<");
        activePawn = null;
        deck.DrawPawns();
        SpawnHand();
        SetSubtitles("Please, pick your Champion.");

        // waiting for player's action
        gameState = GameLoopManager.GAME_STATE_WAITING;
    }

    void DrawSpells()
    {
        if (roomIndex == ADVANCED_CARDS_ROOM - 1)
        {
            deck.SetAdvancedDeck();
            SetSubtitles("Why don't we make this more interesting.");
        }
        else
        {
            SetSubtitles("Now, pick your disease.");
        }

        // let the user play their spells and end their turn
        deck.DrawSpells();
        SpawnHand();

        // waiting for player's action
        gameState = GameLoopManager.GAME_STATE_WAITING;
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
        else if (gameState == GAME_STATE_SPAWN_ENEMIES)
        {
            int enemyCount = UnityEngine.Random.Range(1, 1 + roomIndex);
            enemyList = new List<Enemy>();
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemyPawn (i);
            }
            if (enemyCount == 1)
            {
                SetSubtitles("Lucky you, the Dice has decided you'll only fight one enemy.");
            }
            else
            {
                SetSubtitles("Oh, the Dice has decided you shall fight " +
                enemyCount.ToString() +
                " enemies. How unlucky.");
            }
            gameState = GameLoopManager.GAME_STATE_WAITING;

            Invoke("StartTurn", 2);
        }
        else if (gameState == GameLoopManager.GAME_STATE_SUMMON_PAWN)
        {
            StartTurn();
        }
        else if (gameState == GameLoopManager.GAME_STATE_PLAYS_SPELL)
        {
            DrawSpells();
        }
        else if (gameState == GameLoopManager.GAME_STATE_SLIDING_OUT)
        {
            MoveRoom("Room Despawner");
        }
        else if (gameState == GameLoopManager.GAME_STATE_BATTLE)
        {
            CheckBattleState();
        }

        //Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }

    void SpawnHand()
    {
        GameObject plane = GameObject.Find("Hand Plane");
        Vector3 position = plane.transform.position;

        float cardWidth = 0.12f;
        float cardSpacer = 0.02f;

        List<Card> hand = deck.GetHand();

        int cardCount = hand.Count;

        // for centering the 2 cards
        position.x -= (cardWidth + cardSpacer) * 0.5f;

        // move cards, move it a little more to the left
        if (cardCount == 3)
        {
            position.x -= (cardWidth + cardSpacer) * 0.5f;
        }

        for (int i = 0; i < cardCount; i++)
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
        int index = roomIndex++ % MAX_ROOM_COUNT;

        GameObject roomPrefab =
            (GameObject)
            Resources.Load("prefabs/Room " + index, typeof (GameObject));

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
                gameState = GameLoopManager.GAME_STATE_SPAWN_ENEMIES;
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
        if (gameState == GameLoopManager.GAME_STATE_ANIMATION)
        {
            Debug.Log("Cannot use any cards while the game is animating.");
            return;
        }

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

                    ClearHand (card);

                    // clears the full hand in 1 second
                    Invoke("ClearHand", 1);
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

        activePawn = new Pawn((PawnCard) card);

        SpawnSmokeBomb (position);
        SetSubtitles("The " + card.GetName() + "? What an interesting choice.");
        Invoke("DrawSpells", 3);
    }

    void SpawnEnemyPawn(int index)
    {
        Debug.Log("DM is spawning an enemy.");
        GameObject HeroSpawns = GameObject.Find("EnemySpawns");

        Transform child = HeroSpawns.transform.GetChild(index);

        Vector3 position = child.position;
        position.y += 0.1f;

        GameObject obj =
            Instantiate(enemyPrefab,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0)));

        obj.transform.parent = HeroSpawns.transform;

        // todo: add enemy details
        enemyList.Add(new Enemy());

        SpawnSmokeBomb (position);
    }

    void SpawnSmokeBomb(Vector3 position)
    {
        GameObject obj =
            Instantiate(smokePrefab,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0)));

        obj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        obj.transform.parent = GameObject.Find("Room").transform;
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

        SetSubtitles("Ah, yes. " + card.GetName() + ", what a wonderful card.");
        Invoke("StartEncounter", 3);
    }

    void ClearHand()
    {
        ClearHand(null);
    }

    void ClearHand(Card keepCard)
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cards)
        {
            if (
                keepCard == null ||
                card.GetComponent<CardContainer>().GetCard().GetUniqueId() !=
                keepCard.GetUniqueId()
            )
            {
                Destroy (card);
            }
        }
    }

    void StartEncounter()
    {
        Debug.Log(">>>>>>>>>>>>STARTING ENCOUNTER<<<<<<<<<<<<");
        gameState = GameLoopManager.GAME_STATE_BATTLE;

        Debug.Log("pawn: " + activePawn.ToString());

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //mainCamera.enabled = false;
        //audioListener.enabled = false;
        battleRoom.SetActive(true);

        for (int i = 0; i < enemyList.Count; i++)
        {
            SpawnArenaEnemy(i, enemyList[i]);
        }

        player.GetComponent<pc1>().SetPlayerStats(activePawn);

        // Handling Blind
        UpdateArenaLights();

        SetSubtitles("And now... you fight.");
    }

    void SpawnArenaEnemy(int index, Enemy enemy)
    {
        Debug.Log("Spawning enemy into Arena.");
        GameObject enemySpawns = GameObject.Find("ArenaEnemySpawn");
        Debug.Log("enemySpawns " + enemySpawns);

        Transform child = enemySpawns.transform.GetChild(index);

        Vector3 position = child.position;
        position.y += 0.5f;

        GameObject obj =
            Instantiate(arenaEnemyPrefab,
            position,
            Quaternion.Euler(new Vector3(0, 0, 0)));

        obj.GetComponent<DogControl>().target = player.transform;
    }

    void UpdateArenaLights()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("ArenaLight");
        for (int i = 0; i < lights.Length; i++)
        {
            ParticleSystem system = lights[i].GetComponent<ParticleSystem>();
            if (system != null)
            {
                if (activePawn.isBlind)
                {
                    system.Stop();
                }
                else
                {
                    system.Play();
                }
            }
            else
            {
                lights[i].SetActive(!activePawn.isBlind);
            }
        }
    }

    void CheckBattleState()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool isComplete = true;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<DogControl>().currentHealth > 0)
            {
                isComplete = false;
                break;
            }
        }
        if (isComplete)
        {
            EndEncouter(true);
        }
    }

    public void EndEncouter(bool isAlive)
    {
        // unLock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (isAlive)
        {
            //mainCamera.enabled = true;
            mainCameraObj.SetActive(true);
            battleRoom.SetActive(false);
            deathAnimator.SetBool("isTilt", true);

            gameState = GameLoopManager.GAME_STATE_WAITING;

            Invoke("PostEndEncounter", 2);
        }
        else
        {
            ShowGameOver();
        }
    }

    void PostEndEncounter()
    {
        gameState = GameLoopManager.GAME_STATE_SLIDING_OUT;
    }

    void ShowGameOver()
    {
        SceneManager.LoadScene("game_over");
        SceneManager.UnloadSceneAsync("SampleScene");
    }

    void SetSubtitles(string message)
    {
        // prefix with name
        message = "Death: " + message;
        Debug.Log("Subtitles: " + message);
        subtitles.text = message;
        Invoke("ClearSubtitles", Math.Max(1, message.Length / 20));
    }

    void ClearSubtitles()
    {
        subtitles.text = "";
    }
}
