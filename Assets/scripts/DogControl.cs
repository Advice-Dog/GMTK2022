using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogControl : MonoBehaviour
{
    private static float DEFAULT_WALKING_SPEED = 7.5f;

    private static float DEFAULT_ATTACK_DELAY = 0.5f;

    public int attackDamage;

    public float lookRadius = 30.0f;

    public float jumpRadius = 15.0f;

    public Transform target;

    public Transform castLocation;

    NavMeshAgent agent;

    public int currentHealth;

    public GameObject weaponParticle;

    public GameObject player;

    public float fireDelta;

    private float myTime;

    public float fireWait;

    public float damageDelta;

    private float myTimeDamage;

    public int damageTaken;

    public TMPro.TextMeshProUGUI healthBar;

    //animation variables
    public Animator enemy_Animator;

    bool cast = false;

    public AudioSource Attack;

    public AudioSource Walk;

    public DogControl()
    {
        SetDefaultValues();
    }

    void SetDefaultValues()
    {
        GetComponent<NavMeshAgent>().speed = DEFAULT_WALKING_SPEED;
        fireDelta = DEFAULT_ATTACK_DELAY / 10;
        myTime = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        damageTaken = player.GetComponent<pc1>().attackDamage;
        weaponParticle.SetActive(false);
        agent = GetComponent<NavMeshAgent>();

        //NavSpeed = GetComponent<NavMeshAgent>().speed;
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        Walk.volume = Random.Range(3.9f, 7.5f);
        Walk.pitch = Random.Range(0.2f, 0.3f);
        Walk.Play();

        enemy_Animator.SetBool("isWalk", true);
        enemy_Animator.SetBool("isCast", false);

        if (fireDelta < 1)
        {
            fireDelta = 1.25f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            enemy_Animator.SetBool("isDie", true);
            agent.speed = 0;
            agent.SetDestination(transform.position);
            healthBar.text = "";
            return;
        }
        healthBar.text = currentHealth.ToString();

        myTimeDamage = myTimeDamage + Time.deltaTime;
        myTime = myTime + Time.deltaTime;

        //Debug.Log(myTime);
        agent.SetDestination(target.position);
        if (myTime <= fireDelta && myTime >= fireWait)
        {
            //Debug.Log("enemy particle animation off");
            weaponParticle.SetActive(false);
            enemy_Animator.SetBool("isWalk", true);
            enemy_Animator.SetBool("isCast", false);
        }
        else if (cast && myTime >= fireDelta)
        {
            myTime = 0.0F;

            Attack.volume = Random.Range(0.3f, 0.5f);
            Attack.pitch = Random.Range(0.6f, 0.8f);

            //Debug.Log("enemy attacks");
            enemy_Animator.SetBool("isWalk", false);
            enemy_Animator.SetBool("isCast", true);
            Attack.Play();
            weaponParticle.SetActive(true);
            cast = false;
        }
        else
        {
            enemy_Animator.SetBool("isWalk", true);
            enemy_Animator.SetBool("isCast", false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "attackRange")
        {
            cast = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "attackRange")
        {
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "PlayerAttack" && myTimeDamage > damageDelta
        )
        {
            currentHealth = currentHealth - damageTaken;
            Debug.Log("enemy health: " + currentHealth);
            myTimeDamage = 0.0F;
        }
    }

    public void SetEnemyStats(Pawn pawn)
    {
        Debug.Log(pawn.ToString());

        // reset to default
        SetDefaultValues();
        currentHealth = pawn.maxHealthPoints;
        attackDamage = pawn.attackDamage;
        fireDelta *= (100f / pawn.attackSpeed);
        GetComponent<NavMeshAgent>().speed *= (pawn.movementSpeed / 100f);
    }
}
