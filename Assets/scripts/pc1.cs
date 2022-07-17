using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]
public class pc1 : MonoBehaviour
{
    private static float DEFAULT_WALKING_SPEED = 7.5f;

    private static float DEFAULT_RUNNING_SPEED = 11.5f;

    private static float DEFAULT_JUMP_SPEED = 8.0f;

    private static float DEFAULT_ATTACK_DELAY = 0.5f;

    public int currentHealth;

    public TMPro.TextMeshProUGUI healthBar;

    public int attackDamage;

    public float walkingSpeed;

    public float runningSpeed;

    public bool canJump;

    public float jumpSpeed;

    public float gravity = 20.0f;

    public Camera playerCamera;

    public float lookSpeed = 2.0f;

    public float lookXLimit = 45.0f;

    public Transform weapon;

    [SerializeField] private Vector3 wepRot;

    public float fireDelta;

    private float nextFire;

    private float myTime;

    public float damageDelta;

    private float nextDamage;

    private float myTimeDamage;

    CharacterController characterController;

    Vector3 moveDirection = Vector3.zero;

    float rotationX = 0;

    public GameLoopManager m_someOtherScriptOnAnotherGameObject;

    public Animator deathAnimator;
    
    public GameObject death;


    [HideInInspector]
    public bool canMove = true;

    public pc1()
    {
        SetDefaultValues();
    }

    void SetDefaultValues()
    {
        walkingSpeed = DEFAULT_WALKING_SPEED;
        runningSpeed = DEFAULT_RUNNING_SPEED;
        jumpSpeed = DEFAULT_JUMP_SPEED;
        fireDelta = DEFAULT_ATTACK_DELAY;
        nextFire = fireDelta;
        myTime = 0.0f;
        canJump = true;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        deathAnimator = death.GetComponent<Animator>();
        deathAnimator.SetBool("ArenaDeath", true);
    }

    void Update()
    {
        if (currentHealth <= 0) //if low health you die!
        {
            //GameLoopManager.EndEncouter();
            healthBar.text = "";
            // unLock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            deathAnimator.SetBool("DeathArena", false);
            deathAnimator.SetBool("isThrow", true);
            m_someOtherScriptOnAnotherGameObject.EndEncouter();
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX =
            canMove
                ? (isRunning ? runningSpeed : walkingSpeed) *
                Input.GetAxis("Vertical")
                : 0;
        float curSpeedY =
            canMove
                ? (isRunning ? runningSpeed : walkingSpeed) *
                Input.GetAxis("Horizontal")
                : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (
            Input.GetButton("Jump") &&
            canMove &&
            canJump &&
            characterController.isGrounded
        )
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation =
                Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *=
                Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        myTime = myTime + Time.deltaTime;
        myTimeDamage = myTimeDamage + Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Debug.Log("you attacked");
            weapon.Rotate(wepRot * Time.deltaTime);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }

    // Copy over the pawn stats to our player
    public void SetPlayerStats(Pawn pawn)
    {
        Debug.Log(pawn.ToString());

        // reset to default
        SetDefaultValues();
        currentHealth = pawn.maxHealthPoints;
        attackDamage = pawn.attackDamage;
        fireDelta *= (100f / pawn.attackSpeed);
        walkingSpeed *= (pawn.movementSpeed / 100f);
        runningSpeed *= (pawn.movementSpeed / 100f);
        canJump = pawn.canJump;

        healthBar.text = "Health:" + currentHealth;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "EnemyAttack" && myTimeDamage > nextDamage)
        {
            currentHealth = currentHealth - 1;
            nextDamage = myTimeDamage + damageDelta;
            Debug.Log("you were hit");
            nextDamage = nextDamage - myTimeDamage;
            myTimeDamage = 0.0F;
            healthBar.text = "Health:" + currentHealth;
        }
    }
}
