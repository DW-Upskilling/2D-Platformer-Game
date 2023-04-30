using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed, sprintSpeed, jumpForce, crouchForce;
    public LayerMask EarthLayer;
    public GameObject Void;
    public ScoreController scoreController;

    private Rigidbody2D rb2d;

    private bool isGrounded;

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {

        playerMovementController();

    }
    void LateUpdate()
    {
        if (playerIsDead())
        {
            Debug.Log("Noob Died!");
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Earth"))
        {
            Debug.Log("Touch the grass");
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Earth"))
        {
            Debug.Log("Shoulder touch");
            isGrounded = false;
        }
    }


    void playerMovementController()
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

        playerMove(horizontal, sprint);
        playerJump(jump || vertical > 0);

        // animator.SetBool("IsCrouch", crouch || vertical < 0);
        // animator.SetBool("IsJump", jump || vertical > 0);

    }
    void playerMove(float horizontal, bool sprint)
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

    void playerJump(bool isJump)
    {

        //bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, EarthLayer);
        // Debug.Log(isJump + "\t" + isGrounded);
        // Set the Player Jump Animation
        animator.SetBool("IsJump", isJump && isGrounded);

        // Set the Player Position
        if (isJump && isGrounded)
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        // rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce), ForceMode2D.Force);
    }

    public bool playerIsDead()
    {
        // Debug.Log(Void.GetComponent<Transform>().position.y + "\t" + transform.position.y);
        if (Void.GetComponent<Transform>().position.y >= transform.position.y)
        {
            return true;
        }

        return false;
    }

    public void pickUpKey(GameObject key)
    {
        scoreController.incrementScore(1);
    }

}


