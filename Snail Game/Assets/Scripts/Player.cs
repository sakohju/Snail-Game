using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float hp; //hp stat
    public float speed; //speed 
    public float rotateSpeed;
    public int defense; //defense
    public float sunDamage; //amount of damage taken from sun
    public float enemyDamage; //amount of damage taken from birds

    private bool groundedPlayer; //boolean if the snail is on the ground or climbing
    private bool inShell; //boolean true if retracted in shell, false if not
    private AudioSource retractsound; //shell retract sound effect
    private Vector3 playerVelocity;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private Animator animator;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        retractsound = this.GetComponent<AudioSource>();
        inShell = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            //QUIT GAME
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //SHELL RETRACTION
            if (!inShell)
            {
                //go into shell
                animator.SetBool("Retract", true);
                animator.SetBool("Crawl", false);
                animator.SetBool("Idle", false);
            }
            else
            {
                //come out of shell
                animator.SetBool("Retract", false);
                animator.SetBool("Idle", true);
            }
            inShell = !inShell;
            retractsound.Play();

        }

        if (!inShell)
        {
            //MOVEMENT
            //Rotate around y - axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Move forward / backward
            /*    Vector3 forward = transform.TransformDirection(Vector3.forward);
                float curSpeed = speed * Input.GetAxis("Vertical");
                controller.SimpleMove(forward * curSpeed); */

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * speed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (Input.GetKey(KeyCode.Space) && groundedPlayer)
            {
                playerVelocity.y += jumpHeight;
            }

            //playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


            //Animations
            if (System.Math.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
            {
                animator.SetBool("Crawl", true);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Crawl", false);
                animator.SetBool("Idle", true);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Detect and consume water droplet
        if(other.GetComponent<Pickup>() != null){
            hp += other.GetComponent<Pickup>().healAmount;
            other.gameObject.SetActive(false);
            retractsound.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Light>() != null){
            if (!inShell)
            {
                hp -= sunDamage;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

}
