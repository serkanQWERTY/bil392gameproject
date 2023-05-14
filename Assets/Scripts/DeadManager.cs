using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeadManager : MonoBehaviour
{
    

    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject quitButton;

    public GameObject loadingText;
    public GameObject fadeOut;

    public int loadInt;
    void Start()
    {
        loadInt = PlayerPrefs.GetInt("AutoSave");

    }

 
    void Update()
    {
        
    }

    public void Restart()
    {
        StartCoroutine(RestartLevel()); 
    }
    IEnumerator RestartLevel()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(5);
        loadingText.SetActive(true);
        SceneManager.LoadScene(1);
       
    }
        
     public void ToMainMenu()
    {
        StartCoroutine(MainMenuBack());
       
    }   

    IEnumerator MainMenuBack()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        loadingText.SetActive(true);
        SceneManager.LoadScene(0);
      
    }

    public void ExitGame()
    {
        Application.Quit();

    }
}
