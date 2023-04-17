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
        float speed = Input.GetAxisRaw("Horizontal");

        // Set the Player Movement
        animator.SetFloat("Speed", Mathf.Abs(speed));

        // Set the Player Direction - Flips the Player Sprite
        Vector3 scale = transform.localScale;
        if(speed < 0 && scale.x > 0)
            scale.x = -1f * scale.x;
        else if(speed > 0 && scale.x < 0)
            scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
