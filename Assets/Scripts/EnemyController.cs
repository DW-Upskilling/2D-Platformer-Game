using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float rayDistance;

    public GameObject groundDetector;

    private Animator animator;

    private bool moveRight;
    // Start is called before the first frame update
    void Start()
    {
        moveRight = true;
        animator = gameObject.GetComponent<Animator>();

        AudioManager audioManager = AudioManager.Instance;
        audioManager.Play("enemyFootstep");
    }

    // Update is called once per frame
    void Update()
    {
        patrolAI();
    }
    private void patrolAI()
    {
        animator.SetBool("isWalking", true);
        // Reference: https://www.youtube.com/watch?v=aRxuKoJH9Y0&t=75s

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D isGround = Physics2D.Raycast(groundDetector.GetComponent<Transform>().position, Vector2.down, rayDistance);

        if (!isGround.collider)
        {
            if (moveRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerController>() != null
           )
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.takeDamage(gameObject);
        }
    }
}
