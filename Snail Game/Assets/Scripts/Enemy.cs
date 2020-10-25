using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private AudioSource caw;
    private BoxCollider hitZone;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        caw = this.GetComponent<AudioSource>();
        hitZone = this.GetComponent<BoxCollider>();
        hitZone.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            animator.SetTrigger("Attack");
            caw.Play();
            hitZone.enabled = true;
        }
    }
}
