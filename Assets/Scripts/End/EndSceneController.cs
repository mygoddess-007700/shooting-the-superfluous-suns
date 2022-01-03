using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneController : MonoBehaviour
{
    public Text nameT;
    public Text studentNumberT;

    public TMP_Text correct;
    public TMP_Text wrong;
    public TMP_Text grade;

    public Button retryBtn;
    public Button quitBtn;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            nameT.text = PlayerPrefs.GetString("Name");
        }

        if (PlayerPrefs.HasKey("StudentNumber"))
        {
            studentNumberT.text = PlayerPrefs.GetString("StudentNumber");
        }

        retryBtn.onClick.AddListener(RetryGame);
        quitBtn.onClick.AddListener(QuitGame);
    }

    void Start()
    {
        correct.text = "Correct: " + StaticData.correct.ToString();
        wrong.text = "Wrong: " + StaticData.error.ToString();
        int tGrade = (int)(((double)StaticData.correct / ((double)StaticData.correct + (double)StaticData.error))*100);
        grade.text = "Grade: " + tGrade.ToString() + "%";
    }

    void RetryGame()
    {
        FadeIntoNextScene.instance.LoadBeginScene();
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
