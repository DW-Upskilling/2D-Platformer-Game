using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private long score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0L;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }

    public void incrementScore(int value)
    {
        score += value;
    }
}
