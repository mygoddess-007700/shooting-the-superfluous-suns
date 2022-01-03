using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIntoNextScene : MonoBehaviour
{
    public static FadeIntoNextScene instance;
    public Animator anim;

    void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject); 
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex+1));
    }

    public void LoadBeginScene()
    {
        StartCoroutine(LoadScene(0));
    }

    IEnumerator LoadScene(int index)
    {
        anim.CrossFade("FadeOut", 0);

        yield return new WaitForSeconds(1.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation obj)
    {
        anim.CrossFade("FadeIn", 0);
    }
}
