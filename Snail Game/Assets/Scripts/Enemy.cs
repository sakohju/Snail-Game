using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private AudioSource caw;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        caw = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("Player detected");
            animator.SetTrigger("Attack");
            caw.Play();
        }
    }
}
