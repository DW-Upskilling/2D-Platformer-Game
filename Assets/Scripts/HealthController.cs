using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Health: " + health;
    }

    public void decrementHealth(int value)
    {
        health -= value;
    }

    public int getHealth()
    {
        return health;
    }
}
