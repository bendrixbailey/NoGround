using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public CollisionNotifier largeCollider;
    public CollisionNotifier smallCollider;

    private int score = 0;
    private int scoreMultiplier = 1;


    // Update is called once per frame
    void Update()
    {
        if (largeCollider.getTriggered()) {
            score += 1* scoreMultiplier + Mathf.RoundToInt(Time.deltaTime * 5);
            Debug.Log("Score increasing!");
        }

        if (smallCollider.getTriggered())
        {
            scoreMultiplier = 10;
        }
        else {
            scoreMultiplier = 1;
        }

        scoreText.text = score.ToString();
        
    }
}
