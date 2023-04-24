using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float speed, sprintSpeed;
    public float jumpForce;
    public float crouchSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        playerMovementController();

    }

    public void playerMovementController()
    {
        // Get the Player Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Get the Player Jump Input
        float vertical = Input.GetAxisRaw("Vertical");
        // Get the Player Crouch Input
        bool crouch = Input.GetButtonUp("Crouch");
        // Get the Player Crouch Input
        bool jump = Input.GetButtonUp("Jump");
        // Get the Player Sprint Input
        bool sprint = Input.GetButton("Sprint");

        movePlayer(horizontal, sprint);

        animator.SetBool("IsCrouch", crouch || vertical < 0);
        animator.SetBool("IsJump", jump || vertical > 0);

    }
    private void movePlayer(float horizontal, bool sprint)
    {
        // Set the Player Movement Animation
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsSprint", sprint);

        // Set the Player Direction - Flips the Player Sprite
        Vector3 scale = transform.localScale;
        if (horizontal < 0 && scale.x > 0)
            scale.x = -1f * scale.x;
        else if (horizontal > 0 && scale.x < 0)
            scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;

        // Set the Player Movement
        Vector3 currentPosition = transform.position;
        float currentSpeed = speed * (sprint ? sprintSpeed : 1);
        currentPosition.x += horizontal * currentSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }
}
