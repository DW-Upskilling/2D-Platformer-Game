using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerController>() != null
           )
        {
            // Debug.Log("Khatam Ho Gayaaa. Only once faskkk");
            // Debug.Log("Level Complete. Start the next level");

            collision.gameObject.GetComponent<PlayerController>().ResetLevel();
        }
    }

}
