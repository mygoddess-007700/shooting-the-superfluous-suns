using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int number = 1;
    public int score = 100;
    
    [Header("Reference")]
    public TextAsset Questions;
    public TextAsset Options;
    public TextAsset Answers;

    public TMP_Text numT;
    public TMP_Text scoreT;
    public TMP_Text questionT;
    public TMP_Text answerT;
    
    public Image rightI;
    public Image errorI;

    public SpriteRenderer answer1;
    public SpriteRenderer answer2;
    public SpriteRenderer answer3;

    public AudioSource rightA;
    public AudioSource errorA;

    [Header("问题")]
    public string [] questions;
    [Header("选项答案")]
    public string [] options;
    [Header("答案")]
    public string [] answers;
    [Header("题目数量")]
    public int maxNum = 10;
    [Header("题目分数")]
    public int shootRightScore = 0;
    public int shootErrorScore = 5;
    [Header("答案贴图存储")]
    public Sprite [] ASun;
    public Sprite [] BSun;
    public Sprite [] CSun;
    [Header("太阳下落速度")]
    public float fallSpeed;

    private int shootSunNum = 0;
    
    void Awake()
    {
        instance = this;

        questions = Questions.text.Split('\n');
        options = Options.text.Split(' ');
        answers = Answers.text.Split('\n');
        maxNum = questions.Length;

        ASun = new Sprite[maxNum];
        BSun = new Sprite[maxNum];
        CSun = new Sprite[maxNum];

        for (int i = 0; i < maxNum; i++)
        {
            ASun[i] = Resources.Load<Sprite>("Sun/ASun" + (i+1).ToString());
            BSun[i] = Resources.Load<Sprite>("Sun/BSun" + (i+1).ToString());
            CSun[i] = Resources.Load<Sprite>("Sun/CSun" + (i+1).ToString());
        }
    }

    void Start()
    {
        Color tColor = rightI.color;
        tColor.a = 0;
        rightI.color = tColor;
        errorI.color = tColor;
        
        questionT.text = questions[0];
        answerT.text = answers[0];
        answer1.sprite = ASun[0];
        answer2.sprite = BSun[0];
        answer3.sprite = CSun[0];
    }

    void Update()
    {
        numT.text = "Number: " + number.ToString();
        scoreT.text = "Score: " + score.ToString();    
    }

    public void ShootRight()
    {
        StartCoroutine(FadeIn(errorI));

        StartCoroutine(ShowAnswer());

        //播放音效
        errorA.Play();


        if (shootSunNum == 1)
        {
            scoreT.text = "Score: " + score.ToString();
            StaticData.error++;
            shootSunNum = 0;
        }
        else
        {
            scoreT.text = "Score: " + score.ToString();
            StaticData.error++;
        }

        StartCoroutine(NextQuestion(number));
        StartCoroutine(NextSuns(number));

        if (number < maxNum)
        {
            StartCoroutine(AddNumber());
        }
    }

    public void ShootError(int index)
    {
        switch (index)
        {
            case 1:
                StartCoroutine(FallSun(answer1.gameObject));
                break;
            case 2:
                StartCoroutine(FallSun(answer2.gameObject));
                break;
            case 3:
                StartCoroutine(FallSun(answer3.gameObject));
                break;
        }

        if (shootSunNum == 0)
        {
            shootSunNum++;
            return;
        }

        if (shootSunNum == 1)
        {
            StartCoroutine(FadeIn(rightI));

            StartCoroutine(ShowAnswer());

            //播放音效
            rightA.Play();

            shootSunNum = 0;
            score += shootErrorScore * 2;
            scoreT.text = "Score: " + score.ToString();
            StaticData.correct++;

            StartCoroutine(NextQuestion(number));
            StartCoroutine(NextSuns(number));

            if (number < maxNum)
            {
                StartCoroutine(AddNumber());
            }
        }
    }

    public IEnumerator NextQuestion(int num)
    {
        yield return new WaitForSeconds(1f);

        if (num >= maxNum)
        {
            FadeIntoNextScene.instance.LoadNextScene();
        }
        else
        {
            questionT.text = questions[num];
        }  
    }

    public IEnumerator NextSuns(int num)
    {
        yield return new WaitForSeconds(1f);

        if (num >= maxNum)
        {
            FadeIntoNextScene.instance.LoadNextScene();
        }
        else
        {
            answer1.gameObject.SetActive(true);
            answer2.gameObject.SetActive(true);
            answer3.gameObject.SetActive(true);

            Color tColor = answer1.color;
            tColor.a = 1;
            answer1.sprite = ASun[num];
            answer1.color = tColor;
            answer2.sprite = BSun[num];
            answer2.color = tColor;
            answer3.sprite = CSun[num];
            answer3.color = tColor;
        }
    }

    public IEnumerator FadeIn(Image i)
    {
        Color tColor = i.color;
        float fadeOutDuration = 0.8f;
        float fadeOutDone = Time.time + fadeOutDuration;
        while (Time.time < fadeOutDone)
        {
            tColor.a = 1 - (fadeOutDone - Time.time) / fadeOutDone;
            i.color = tColor;
            yield return null;
        }
        tColor.a = 1;
        i.color = tColor;

        StartCoroutine(FadeOut(i));
    }

    public IEnumerator FadeOut(Image i)
    {
        Color tColor = i.color;
        float fadeOutDuration = 0.2f;
        float fadeOutDone = Time.time + fadeOutDuration;
        while (Time.time < fadeOutDone)
        {
            tColor.a = (fadeOutDone - Time.time) / fadeOutDone;
            i.color = tColor;
            yield return null;
        }
        tColor.a = 0;
        i.color = tColor;
    }

    public IEnumerator ShowAnswer()
    {
        answerT.text = answers[number-1];
        float fadeDuration = 0.7f;
        float fadeDone = Time.time + fadeDuration;
        Color tColor = answerT.color;

        while (Time.time < fadeDone)
        {
            tColor.a = 1 - (fadeDone - Time.time) / fadeDuration;
            answerT.color = tColor;

            yield return null;
        }

        tColor.a = 1;
        answerT.color = tColor;

        StartCoroutine(LaterFadeAnswer());
    }

    public IEnumerator LaterFadeAnswer()
    {
        yield return new WaitForSeconds(0.3f);

        Color tColor = answerT.color;
        tColor.a = 0;
        answerT.color = tColor;
    }

    public IEnumerator FallSun(GameObject sun)
    {
        SpriteRenderer sunRender = sun.GetComponent<SpriteRenderer>();

        float fallDuration = 0.9f;
        float fallDone = Time.time + fallDuration;
        Vector3 tSunPos = sun.transform.position;
        Vector3 tPos = sun.transform.position;
        Color tColor = sunRender.color;

        while (Time.time < fallDone)
        {
            tPos.y -= fallSpeed * Time.deltaTime;
            sun.transform.position = tPos;

            tColor.a = (fallDone - Time.time) / fallDuration;
            sunRender.color = tColor;

            yield return null;
        }

        tColor.a = 0;
        sunRender.color = tColor;

        sun.transform.position = tSunPos;
        sun.SetActive(false);
    }

    public IEnumerator AddNumber()
    {
        yield return new WaitForSeconds(1f);

        number++;
    }
}