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

        Walk.volume = Random.Range(0.3f, 0.5f);
        Walk.pitch = Random.Range(0.2f, 0.3f);
        Walk.Play();

        enemy_Animator.SetBool("isWalk", true);
        enemy_Animator.SetBool("isCast", false);
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(target.position, castLocation.position);
        //Debug.Log(distance);
        agent.SetDestination(target.position);
        if (cast)
        {
            Debug.Log("attack");
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
}