using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject loadText;

    public GameObject loadButton;
    public int loadInt;

    void Start()
    {
        loadInt = PlayerPrefs.GetInt("AutoSave");
        if (loadInt > 0)
        {
            loadButton.SetActive(true);
        }
    }

    public void NewGameButton()
    {
        StartCoroutine(NewGame());

    }

   IEnumerator NewGame()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(5);
        loadText.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void LoadGameButton()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(5);
        loadText.SetActive(true);
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();

    }
}

