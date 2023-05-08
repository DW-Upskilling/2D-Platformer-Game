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
    public HealthController healthController;

    public GameObject Spawn;

    private Rigidbody2D rb2d;

    private int isGrounded;
    private float currentSprintSpeed;

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = -1;

        AudioManager audioManager = AudioManager.Instance;
        audioManager.Play("playerFootstep");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsHurt", false);

        playerMovementController();

    }
    void LateUpdate()
    {
        if (playerIsDead())
        {
            // Debug.Log("Noob Died!");
            ResetLevel();
        }
    }

    public void ResetLevel()
    {

        AudioManager.Instance.Play("playerFootstep", false);

        // Save the current scene index as the previous scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("previousSceneIndex", currentSceneIndex);
        PlayerPrefs.Save();

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Earth"))
        {
            // Debug.Log("Touch the grass");
            isGrounded = -1;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Earth"))
        {
            // Debug.Log("Shoulder touch");
            isGrounded += 1;
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
        if (horizontal == 0)
            AudioManager.Instance.Play("playerFootstep", false);
        else
            AudioManager.Instance.Play("playerFootstep", true);

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
        if (sprint && currentSprintSpeed < sprintSpeed)
        {
            currentSprintSpeed += 1;
        }
        else
        {
            currentSprintSpeed = sprintSpeed;
        }

        float currentSpeed = speed * (sprint ? currentSprintSpeed : 1);
        currentPosition.x += horizontal * currentSpeed * Time.deltaTime;
        transform.position = currentPosition;

    }

    void playerJump(bool isJump)
    {

        // Player can double jump now
        bool canJump = isJump && isGrounded <= 1;

        //bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, EarthLayer);
        // Debug.Log(isJump + "\t" + isGrounded);
        // Set the Player Jump Animation
        animator.SetBool("IsJump", canJump);

        // Set the Player Position
        if (canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            // rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce), ForceMode2D.Force);

            // AudioManager.Instance.Play("playerJump");
            isGrounded += 1;
        }

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

    public void takeDamage(GameObject enemy)
    {

        healthController.decrementHealth(1);
        if (healthController.getHealth() <= 0)
        {
            animator.Play("Death");
            ResetLevel();
        }
        else
        {
            animator.SetBool("IsHurt", true);
            transform.position = Spawn.transform.position;
        }
    }

}


