using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleScript : MonoBehaviour
{

    int scoreAdder;

    public string playMode;

    public float time = 3;
    private float reTime;
    GameManiger gameManiger;

    public Image timeLeftImage;

    // Start is called before the first frame update
    void Start()
    {
        gameManiger = GameObject.Find("GameManiger").GetComponent<GameManiger>();
        scoreAdder = PlayerPrefs.GetInt("scoreAdd");
        reTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManiger.isOnPause) {
            if (time > 0) {

                timeLeftImage.fillAmount -= 1.0f / reTime * Time.deltaTime;
                time -= Time.deltaTime;
                //timeLeftImage.fillAmount = time;
            } else {
                gameManiger.lives--;
                gameManiger.livesText.SetText("Lives: " + gameManiger.lives);
               
                Destroy(gameObject);
                time = 3;
            }

            if (gameManiger.lives == 0) {
                Destroy(gameObject);
            }
        }

    }

   

    private void OnMouseDown() {

        if (!gameManiger.isOnPause) {
            if (time > 0) {
                Debug.Log("Yes");
                Destroy(gameObject);
                Debug.Log("Circle destroyed by touch");

                if (playMode == "Easy") {
                    gameManiger.score += 10 + scoreAdder;
                } else if (playMode == "Medium") {
                    gameManiger.score += 15 + scoreAdder;
                } else if (playMode == "Hard") {
                    gameManiger.score += 25 + scoreAdder;
                }

            }
        }

        gameManiger.scoreText.SetText("Score: " + gameManiger.score);
    }

    
}
