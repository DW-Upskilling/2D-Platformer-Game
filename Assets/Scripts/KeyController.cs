using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().pickUpKey(gameObject);

            animator.Play("Collected");
            AudioManager.Instance.Play("keyPickup");

            // Check if the animation has finished playing
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // Start the coroutine to destroy the game object
                StartCoroutine(DestroyObject());
            }
        }
    }

    IEnumerator DestroyObject()
    {
        // Wait for a short period of time before destroying the object
        yield return new WaitForSeconds(1.0f);

        // Destroy the game object
        Destroy(gameObject);
    }
}
