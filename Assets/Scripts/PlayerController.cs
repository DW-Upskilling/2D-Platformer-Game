using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Get the Player Input
        float direction = Input.GetAxisRaw("Horizontal");

        // Set the Player Movement
        animator.SetFloat("Speed", Mathf.Abs(direction));
    }
}
