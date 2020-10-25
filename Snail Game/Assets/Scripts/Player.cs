using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int hp; //hp stat
    public float speed; //speed 
    public float rotateSpeed;

    private bool inShell; //boolean true if retracted in shell, false if not
    private AudioSource retractsound; //shell retract sound effect

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

        if(Input.GetKey(KeyCode.Escape)){
            //QUIT GAME
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            //SHELL RETRACTION
            if(!inShell){
                //go into shell
                animator.SetBool("Retract", true);
                animator.SetBool("Crawl", false);
                animator.SetBool("Idle", false);
            }
            else{
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
            // Rotate around y - axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            controller.SimpleMove(forward * curSpeed);

            //Animations
            if(System.Math.Abs(curSpeed) > Mathf.Epsilon)
            {
                animator.SetBool("Crawl", true);
                animator.SetBool("Idle", false);
            }
            else{
                animator.SetBool("Crawl", false);
                animator.SetBool("Idle", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detect and consume water droplet
        if(other.GetComponent<Pickup>()){
            other.enabled = false;
            hp += 5;
            Debug.Log("Drank Water!");
        }
    }


}
