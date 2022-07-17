using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DogControl : MonoBehaviour
{
    public float lookRadius = 30.0f;
    public float jumpRadius = 15.0f;
    public Transform target;
    public Transform castLocation;
    NavMeshAgent agent;

    public int currentHealth;

    public float fireDelta;
    private float nextFire;
    private float myTime;

    public float damageDelta;
    private float nextDamage;
    private float myTimeDamage;

    //animation variables
    public Animator enemy_Animator;
    bool cast = false;

    public AudioSource Attack;
    public AudioSource Walk;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //NavSpeed = GetComponent<NavMeshAgent>().speed;
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.

        Walk.volume = Random.Range(3.9f, 7.5f);
        Walk.pitch = Random.Range(0.2f, 0.3f);
        Walk.Play();

        enemy_Animator.SetBool("isWalk", true);
        enemy_Animator.SetBool("isCast", false);
    }

    // Update is called once per frame
    void Update()
    {
        myTimeDamage = myTimeDamage + Time.deltaTime;


        //float distance = Vector3.Distance(target.position, castLocation.position);
        //Debug.Log(distance);
        agent.SetDestination(target.position);
        if (cast)
        {
            Attack.volume = Random.Range(0.3f, 0.5f);
            Attack.pitch = Random.Range(0.6f, 0.8f);
            //Debug.Log("attack");
            enemy_Animator.SetBool("isWalk", false);
            enemy_Animator.SetBool("isCast", true);
            Attack.Play();
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
        Debug.Log("FUCKKK");
        if (other.gameObject.tag == "PlayerAttack")// && myTimeDamage > nextDamage)
        {
            currentHealth = currentHealth - 1;
            nextDamage = myTimeDamage + damageDelta;
            Debug.Log("you hit an enemy!");
            nextDamage = nextDamage - myTimeDamage;
            myTimeDamage = 0.0F; 
        }
    }
}