using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    /*
    float time = 30f;
    [SerializeField] TextMeshProUGUI text;
    static GameManager gameManager;

    void Start()
    {
        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
        }    
    }
    void Update()
    {
       // float tempTime = gameManager.time;

        // decrease timer by Time.deltaTime
        if (tempTime > 0) {
            time -= Time.deltaTime;
        }
        else {
            tempTime += 0f;
        }

        // Change the color of the text from white to red, when timer <= 10
        // change the color of the text from red to white, when timer > 10
        if (tempTime < 11) { text.color = Color.red; }
        else { text.color = Color.white; }

        if (tempTime == 0)
        {
            // call Gameover() from GameManager
        }

        // Working
        //if (Input.GetKeyDown(KeyCode.F)) { time += 10; }

        DisplayTime(tempTime);
    }
    void DisplayTime(float t)
    {
        if (t < 0) {
            t = 0f;
        }

        float minute = Mathf.FloorToInt(t / 60);
        float second = Mathf.FloorToInt(t % 60);
            
        text.text = string.Format("TIME: {0:00}:{1:00}", minute, second);
    }
    */
}
