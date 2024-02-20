using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManiger : MonoBehaviour
{

    private RewardedAdsButton rewardAdd;

    int lives;
    float coins;
    float sumForLives;

    public TextMeshProUGUI coinText;

    // for score adder
    int scoreAdd;
    int scoreAdderCaunt;
    public TextMeshProUGUI scoreAdderHaveText;
    public TextMeshProUGUI scoreAddedText;
    public TextMeshProUGUI scoreAdderPriceText;
    int sumForScoreAdder;

    //for time adder
    float timeAdd;
    int timeAdderCaunt;
    int sumForTimeAdder;
    public TextMeshProUGUI timeAdderHaveText;
    public TextMeshProUGUI timeAdderPriceText;
    public TextMeshProUGUI timeAddedText;

    // more of lives
    public TextMeshProUGUI livesPriceText;
    public TextMeshProUGUI hartHaveText;

    //coinHelper
    int sumForCoinHelper = 300;
    public TextMeshProUGUI coinHelperPrice;

    //btns
    public GameObject buyLivesBtn;
    public GameObject buyAddScoreBtn;
    public GameObject buyTimeAddBtn;
    public GameObject buyCoinHelperBtn;

    public AudioSource audioSorce;

    private void Start() {

        rewardAdd = GameObject.Find("AdBtn").GetComponent<RewardedAdsButton>();

        lives = PlayerPrefs.GetInt("lives");

        // for score
        scoreAdd = PlayerPrefs.GetInt("scoreAdd");
        scoreAdderCaunt = PlayerPrefs.GetInt("scoreAdderCaunt");
        sumForScoreAdder = PlayerPrefs.GetInt("sumForScoreAdder");

        if (PlayerPrefs.GetInt("lives") > 3) {
            lives = PlayerPrefs.GetInt("lives");
        } else {
            lives = 3;
            PlayerPrefs.SetInt("lives", lives);
        }

        // coin text
        coins = PlayerPrefs.GetFloat("coins");
        coinText.SetText("Coins: " + coins);

        // more lives
        sumForLives = PlayerPrefs.GetFloat("sumForLives");
        livesPriceText.SetText("Buy: " + sumForLives + " coins");

        //for time adder
        timeAdderCaunt = PlayerPrefs.GetInt("timeAdderCaunt");
        timeAdd = PlayerPrefs.GetFloat("timeAdd");
        sumForTimeAdder = PlayerPrefs.GetInt("sumForTimeAdder");

        // checkers
        if (lives <= 3) {
            sumForLives = 100;
            PlayerPrefs.SetFloat("sumForLives", sumForLives);
        }

        if (scoreAdd <= 0) {
            sumForScoreAdder = 50;
            PlayerPrefs.SetInt("sumForScoreAdder", sumForScoreAdder);
        }

        if(timeAdd <= 0) {
            sumForTimeAdder = 150;
            PlayerPrefs.SetInt("sumForTimeAdder", sumForTimeAdder);
        }
    }

    private void Update() {

        if(PlayerPrefs.GetString("isMusicStop") == "true") {
            audioSorce.volume = 0;
        } else {
            audioSorce.volume = 1;
        }

       // coinText.SetText("Coins: " + coins);
       AdReward();

        //for lives
        hartHaveText.SetText("You have: " + lives + "/5");
        livesPriceText.SetText("Buy: " + sumForLives + " coins");

        // for score
        scoreAdderHaveText.SetText("You have: " + scoreAdderCaunt + "/6");
        scoreAddedText.SetText("ScoreAdded: " + scoreAdd);
        scoreAdderPriceText.SetText("Buy: " + sumForScoreAdder + " coins");

        //for timeAdder
        timeAdderHaveText.SetText("You have: " + timeAdderCaunt + "/2");
        timeAdderPriceText.SetText("Buy: " + sumForTimeAdder + " coins");
        timeAddedText.SetText("TimeAdded: " + timeAdd + "s");

        // for coinHelper
        coinHelperPrice.SetText("Buy: " + sumForCoinHelper + " coins");

        //chekers
        if (lives >= 5) {
            buyLivesBtn.SetActive(false);
        }

        if(scoreAdd >= 20) {
            buyAddScoreBtn.SetActive(false);
        }

        if(timeAdderCaunt >= 2) {
            buyTimeAddBtn.SetActive(false);
        }

        if(PlayerPrefs.GetString("isCoinHelperBuyed") == "true") {
            buyCoinHelperBtn.SetActive(false);
        }
    }
    public void ShopOpen() {

        SceneManager.LoadScene("ShopScene");
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void BuyLives() {



        if (lives == 3) {

            if (coins >= sumForLives) {
                lives++;
                coins -= sumForLives;
                sumForLives += 130;
                PlayerPrefs.SetFloat("sumForLives", sumForLives);
                livesPriceText.SetText("Buy: " + sumForLives + " coins");

                coinText.SetText("Coins: " + coins);
                PlayerPrefs.SetFloat("coins", coins);
            }
        } else if (lives == 4 && lives > 3) {
            if (coins >= sumForLives) {
                lives++;
                coins -= sumForLives;
                sumForLives += 200;
                PlayerPrefs.SetFloat("sumForLives", sumForLives);
                livesPriceText.SetText("Buy: " + sumForLives + " coins");

                coinText.SetText("Coins: " + coins);
                PlayerPrefs.SetFloat("coins", coins);
            }
        } else {
            Debug.Log("Can`t more!"); 
        }

        PlayerPrefs.SetInt("lives", lives);
        Debug.Log(PlayerPrefs.GetInt("lives"));
    }


    public void BuyScoreAdder() {

        if (scoreAdderCaunt == 0) {
            if (coins >= sumForScoreAdder) { 

                coins -= sumForScoreAdder;
                sumForScoreAdder += 10;
                scoreAdd += 2;
                scoreAdderCaunt++;

            }
        } else if (scoreAdderCaunt == 1) {
            if (coins >= sumForScoreAdder) {
                coins -= sumForScoreAdder;
                sumForScoreAdder += 25;
                scoreAdd *= 2;
                scoreAdderCaunt++;

            }
        }else if (scoreAdderCaunt == 2) {
            if (coins >= sumForScoreAdder) {

                coins -= sumForScoreAdder;
                sumForScoreAdder += 35;
                scoreAdd *= 2;
                scoreAdderCaunt++;
            }
        }else if(scoreAdderCaunt == 3) {
            if (coins >= sumForScoreAdder) {

                coins -= sumForScoreAdder;
                sumForScoreAdder += 30;
                scoreAdd += 2;
                scoreAdderCaunt++;
            }
        } else if (scoreAdderCaunt == 4) {
            if (coins >= sumForScoreAdder) {

                coins -= sumForScoreAdder;
                sumForScoreAdder += 20;
                scoreAdd += 5;
                scoreAdderCaunt++;
            }
        }else if(scoreAdderCaunt == 5) {
            if (coins >= sumForScoreAdder) {

                coins -= sumForScoreAdder;
                sumForScoreAdder += 50;
                scoreAdd += 5;
                scoreAdderCaunt++;
            }
        } else {
            Debug.Log("EndOfScoreAdd");
        }

        PlayerPrefs.SetInt("scoreAdd", scoreAdd);
        PlayerPrefs.SetInt("scoreAdderCaunt", scoreAdderCaunt);
        PlayerPrefs.SetInt("sumForScoreAdder", sumForScoreAdder);
        PlayerPrefs.SetFloat("coins", coins);
        coinText.SetText("Coins: " + coins);

        Debug.Log("scoreAdd: " + scoreAdd);
        Debug.Log("scoreAddCaunt: " + scoreAdderCaunt);
    }


    public void BuyTimeAdder() {

        if(timeAdderCaunt == 0) {
            if(coins >= sumForTimeAdder) {
                coins -= sumForTimeAdder;
                sumForTimeAdder += 50;
                timeAdd += 0.10f;
                timeAdderCaunt++;
            }

        }else if(timeAdderCaunt == 1) {
            if (coins >= sumForScoreAdder) {
                coins -= sumForTimeAdder;
                sumForTimeAdder += 100;
                timeAdd += 0.10f;
                timeAdderCaunt++;
            }

        }

        PlayerPrefs.SetInt("timeAdderCaunt", timeAdderCaunt);
        PlayerPrefs.SetFloat("timeAdd", timeAdd);
        PlayerPrefs.SetInt("sumForTimeAdder", sumForTimeAdder);
        PlayerPrefs.SetFloat("coins", coins);
        coinText.SetText("Coins: " + coins);
    }


    public void BuyCoinHelper() {
        if(coins >= sumForCoinHelper) {
            coins -= sumForCoinHelper;
            PlayerPrefs.SetString("isCoinHelperBuyed", "true");
            PlayerPrefs.SetFloat("coins", coins);
            coinText.SetText("Coins: " + coins);
        }
    }

    void AdReward() {
        if(rewardAdd.isAddDone) {
            coins += 20;
            PlayerPrefs.SetFloat("coins", coins);
            coinText.SetText("Coins: " + coins);
            rewardAdd.isAddDone = false;
            Debug.Log("Receve 20 coins in shop");
        }
    }
}
