using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{

    [TextArea(3,10)]
    public List<string> textList;

    public TextMeshProUGUI infoText;
    public Image heroImage;

    bool isBtnPress = false;
    public bool isTutEnd = false;
    int e = -1;

    public GameObject Tutorial;
    public GameObject GameCanvis;

    public Sprite idle;
    public Sprite ginus;
    public Sprite amazing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("isTutorialEnd") == "true") {

            isTutEnd = true;
            Tutorial.SetActive(false);
            GameCanvis.SetActive(true);
        }
        
    }

    public void BtnPress() {
        if (!isTutEnd) {

            isBtnPress = true;

            if (isBtnPress) {
                e++;
            }
            for (int i = e; i < textList.Count; i++) {


                if (e == 0) {
                    heroImage.sprite = idle;
                }

                if ( e == 1) {
                    heroImage.sprite = ginus;
                }

                if (e == 5) {
                    heroImage.sprite = idle;
                }

                if (e == 6) {
                    heroImage.sprite = ginus;
                }

                if (e == 7) {
                    heroImage.sprite = amazing;
                }

                if (e == 9) {
                    heroImage.sprite = ginus;
                }

                if (e == 12) {
                    heroImage.sprite = amazing;
                }

                infoText.SetText(textList[e]);



                if (textList.Count - 1 == e) {
                    PlayerPrefs.SetString("isTutorialEnd", "true");
                }
            }
            isBtnPress = false;
        }
    }
}
