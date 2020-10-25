using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int hp; //hp stat
    public float speed; //speed 
    public float rotateSpeed;

    private bool inShell; //boolean true if retracted in shell, false if not

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            //QUIT GAME
            Application.Quit();
        }

        //MOVEMENT
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detect and consume water droplet
        if(other.GetComponent<Pickup>()){
            other.enabled = false;
            Debug.Log("Drank Water!");
        }
    }
}
