using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManiger :MonoBehaviour {

    public bool isCoinHelperSpawned;

    private bool callOnTime = false;
    public bool isOnPause;

    public GameObject circlePrefab;
    public GameObject coinHelpPrefab;

    public int lives;
    public int score;
    public float coins;

    private float maxPosX;
    private float maxPosY;

    private int hightScore;
    private int mediumHightScore;
    private int hardHightScore;


    public GameObject[] circleLeft;
    public GameObject[] coinHelperLeft;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hightscoreText;
    public TextMeshProUGUI coinText;

    public GameObject deadPanel;
    public GameObject pausePanel;

    public AudioSource audioSorce;

    public CircleScript circleScript;

    [SerializeField] RewardedAdsButtonForMainGame rewardedAdsButtonForMainGame;
    // Start is called before the first frame update
    void Start() {

        //rewardAdd = GameObject.Find("AddBtn").GetComponent<RewardAddForMainGame>();

        hightScore = PlayerPrefs.GetInt("hightScore");
        mediumHightScore = PlayerPrefs.GetInt("mediumHightScore");
        hardHightScore = PlayerPrefs.GetInt("hardHightScore");

        coins = PlayerPrefs.GetFloat("coins");
        Time.timeScale = 1f;

        Debug.Log("S" + hightScore);

        maxPosX = 1.5f;
        maxPosY = 3f;

        if (PlayerPrefs.GetInt("lives") > 3) {
            lives = PlayerPrefs.GetInt("lives");
        } else {
            lives = 3;
            PlayerPrefs.SetInt("lives", lives);
        }

        livesText.SetText("Lives: " + lives);

        //PlayerPrefs.SetInt("hightScore", hightScore);
    }

    // Update is called once per frame
    void Update() {


        if (PlayerPrefs.GetString("isMusicStop") == "true") {
            audioSorce.volume = 0;
        } else {
            audioSorce.volume = 1;
        }

        circleLeft = GameObject.FindGameObjectsWithTag("Player");
        coinHelperLeft = GameObject.FindGameObjectsWithTag("CoinHelper");

        if (circleLeft.Length == 0) {
            SpawnCircle();
        }

        if (!callOnTime) {
            if (lives == 0) {
                GameOver();
                callOnTime = true;
            }
        }

        if (PlayerPrefs.GetString("isCoinHelperBuyed") == "true") {


            if (coinHelperLeft.Length == 0 && circleLeft.Length == 0) {
                Debug.Log("StartSpawning CoinHelper");
                SpwanCoinHelper();
            }

        }

        RewardFromAd();
    }

    void SpwanCoinHelper() {

        if (lives > 0) {
            int num = Random.Range(1, 60);
            int chosenNum = Random.Range(1, 60);

            if (num == chosenNum) {
                Debug.Log("CoinHelper is spawned");
                float randomPosX = Random.Range(-maxPosX, maxPosX);
                float randomPosY = Random.Range(-maxPosY, maxPosY);

                Instantiate(coinHelpPrefab, new Vector3(randomPosX, randomPosY, 0f), coinHelpPrefab.transform.rotation);

                isCoinHelperSpawned = true;
            }
        }
    }

    private void GameOver() {

        coins += score / 100;
        PlayerPrefs.SetFloat("coins", coins);
        Debug.Log("Coins: " + coins);

        livesText.SetText("");


        if (circleScript.playMode == "Easy") {

            if (PlayerPrefs.GetInt("hightScore") < score) {
                hightScore = score;
                PlayerPrefs.SetInt("hightScore", hightScore);
                PlayerPrefs.Save();
            }

            hightscoreText.SetText("Easy Hightscore: " + hightScore);

        }

        if (circleScript.playMode == "Medium") {

            if (PlayerPrefs.GetInt("mediumHightScore") < score) {
                mediumHightScore = score;
                PlayerPrefs.SetInt("mediumHightScore", mediumHightScore);
                PlayerPrefs.Save();
            }

            hightscoreText.SetText("Medium Hightscore: " + mediumHightScore);
        }

        if (circleScript.playMode == "Hard") {

            if (PlayerPrefs.GetInt("hardHightScore") < score) {
                hardHightScore = score;
                PlayerPrefs.SetInt("hardHightScore", hardHightScore);
                PlayerPrefs.Save();
            }

            hightscoreText.SetText("Hard Hightscore: " + hardHightScore);
        }

        coinText.SetText("Coins: " + coins);

        deadPanel.SetActive(true);
    }
    void SpawnCircle() {

        if (lives > 0) {

            float randomPosX = Random.Range(-maxPosX, maxPosX);
            float randomPosY = Random.Range(-maxPosY, maxPosY);

            Instantiate(circlePrefab, new Vector3(randomPosX, randomPosY, 0f), circlePrefab.transform.rotation);

            Debug.Log(lives);
        }
    }

    public void GoToMainMenu() {

        SceneManager.LoadScene("MenuScene");
    }

    public void PlayAgain() {

        PlayerPrefs.SetInt("hightScore", hightScore);
        Debug.Log("B" + hightScore);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("A" + hightScore);
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnPauseClick() {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isOnPause = true;
    }

    public void OnResumeClick() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isOnPause = false;
    }

    void RewardFromAd() {
        if (rewardedAdsButtonForMainGame.isAddDone) {
            score *= 2;
            scoreText.SetText("Score: " + score);
            GameOver();
            rewardedAdsButtonForMainGame.isAddDone = false;
        }
    }
}
