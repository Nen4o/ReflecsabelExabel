using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts :MonoBehaviour {

    public GameObject playerObj;
    public GameObject menuPerent;
    public GameObject settingsPerent;
    public GameObject resetProgressWarning;

    public GameObject stopMusicBtn;
    public GameObject playMusicBtn;

    public AudioSource audioSorce;

    private CircleScript circleScript;

    TutorialScript tutorialScript;

    float timeAdd;

    private void Start() {
        circleScript = playerObj.GetComponent<CircleScript>();
        tutorialScript = GameObject.Find("Tutorial").GetComponent<TutorialScript>();
        timeAdd = PlayerPrefs.GetFloat("timeAdd");
    }

    private void Update() {
        if (PlayerPrefs.GetString("isMusicStop") == "true") {

            audioSorce.volume = 0;

            playMusicBtn.SetActive(true);
            stopMusicBtn.SetActive(false);
        } else {
            audioSorce.volume = 1;

            playMusicBtn.SetActive(false);
            stopMusicBtn.SetActive(true);
        }
    }
    public void StartEasyGame() {
        if (tutorialScript.isTutEnd) {
            circleScript.time = 1.35f + timeAdd;
            circleScript.playMode = "Easy";
            SceneManager.LoadScene("GameScene");
            Debug.Log("time: " + circleScript.time);
        }
    }

    public void StartMediumMode() {
        if (tutorialScript.isTutEnd) {
            circleScript.time = 0.70f + timeAdd;
            circleScript.playMode = "Medium";
            SceneManager.LoadScene("GameScene");
            Debug.Log("time: " + circleScript.time);
        }
    }

    public void StartHardMode() {
        if (tutorialScript.isTutEnd) {
            circleScript.time = 0.40f + timeAdd;
            circleScript.playMode = "Hard";
            SceneManager.LoadScene("GameScene");
            Debug.Log("time: " + circleScript.time);
        }
    }

    public void QuitGame() {
        if (tutorialScript.isTutEnd) {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    public void ShopOpen() {
        if (tutorialScript.isTutEnd) {
            SceneManager.LoadScene("ShopScene");
        }
    }


    public void ResetProgresWarningBtn() {
        if (tutorialScript.isTutEnd) {

            resetProgressWarning.SetActive(true);
            settingsPerent.SetActive(false);
        }
    }

    public void ResetProgres() {
        PlayerPrefs.DeleteAll();
        GoBackToMainMenu();
        SceneManager.LoadScene("MenuScene");
    }

    public void SettingBtn() {
        if (tutorialScript.isTutEnd) {
            Debug.Log("GoToSettings");
            menuPerent.SetActive(false);
            settingsPerent.SetActive(true);
            resetProgressWarning.SetActive(false);
        }
    }

    public void GoBackToMainMenu() {
        menuPerent.SetActive(true);
        settingsPerent.SetActive(false);
    }

    public void StopMusic() {

        PlayerPrefs.SetString("isMusicStop", "true");
        playMusicBtn.SetActive(true);
        stopMusicBtn.SetActive(false);
    }

    public void StartMusic() {

        PlayerPrefs.SetString("isMusicStop", "false");

        playMusicBtn.SetActive(false);
        stopMusicBtn.SetActive(true);
    }

    public void SeeTutorialAgain() {
        SceneManager.LoadScene("MenuScene");
        PlayerPrefs.SetString("isTutorialEnd", "false");
    }
}
