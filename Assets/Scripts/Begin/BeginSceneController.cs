using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginSceneController : MonoBehaviour
{
    [Header("Reference: ")]
    public Button beginGameBtn;

    public InputField nameT;
    public InputField studentNumberT;

    public Image nameI;
    public Image studentNumberI;

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

        StaticData.correct = 0;
        StaticData.error = 0;
    }

    void Start()
    {
        beginGameBtn.onClick.AddListener(BeginGame);
    }

    public void BeginGame()
    {
        if (nameT.text.Trim() == "")
        {
            StartCoroutine(ColorHint(nameI));
            return;
        }
        else if (studentNumberT.text.Trim() == "")
        {
            StartCoroutine(ColorHint(studentNumberI));
            return;
        }
        else
        {
            PlayerPrefs.SetString("Name", nameT.text);
            PlayerPrefs.SetString("StudentNumber", studentNumberT.text);
            FadeIntoNextScene.instance.LoadNextScene();
        }
    }

    public IEnumerator ColorHint(Image image)
    {
        Color tColor = image.color;
        image.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        image.color = tColor;
    }
}