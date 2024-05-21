using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator trasnsition;

    

    public float trasnsitionTime = 1f;
    void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(currentIndex !=0 )
        {
            trasnsition.SetBool("NotMainMenu", true);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LoadLevel(1));
    }

        public void LoadThisLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        trasnsition.SetTrigger("Start");

        yield return new WaitForSeconds(trasnsitionTime);

        SceneManager.LoadScene(levelIndex);
    }

        public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
