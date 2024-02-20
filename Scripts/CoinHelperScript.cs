using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinHelperScript : MonoBehaviour
{


    public float time = 3;
    private float reTime;
    GameManiger gameManiger;

    public Image timeLeftImage;


    // Start is called before the first frame update
    void Start()
    {
        reTime = time;
        gameManiger = GameObject.Find("GameManiger").GetComponent<GameManiger>();
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
                Debug.Log("CoinHelper is destroy by time");
                Destroy(gameObject);
                gameManiger.isCoinHelperSpawned = false;
                time = reTime;
            }

            if (gameManiger.lives == 0) {
                Destroy(gameObject);
                Debug.Log("CoinHelper is destroi by daing");
                gameManiger.isCoinHelperSpawned = false;
            }
        }


        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);

            if (hit.collider != null) {
                Debug.Log("Detect with touch");

                gameManiger.coins += 5;
                PlayerPrefs.SetFloat("coins", gameManiger.coins);


                if (!gameManiger.isOnPause) {
                    if (time > 0) {
                        Debug.Log("Yes");
                        Debug.Log("CoinHelper is destroy by touch");
                        Destroy(gameObject);
                        gameManiger.isCoinHelperSpawned=false;
                    }
                }

            }
        }
    }
}
