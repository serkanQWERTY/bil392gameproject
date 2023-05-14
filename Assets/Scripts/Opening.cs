﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public GameObject player;
    public GameObject fadeScreen;
    public GameObject textBox;

    void Start()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine(ScenePlayer());
    }

   
    void Update()
    {
        
    }

    IEnumerator ScenePlayer()
    {
        yield return new WaitForSeconds(1.5f);
        fadeScreen.SetActive(false);
        textBox.GetComponent<Text>().text = "Nerdeyim ben... Burası Neresi?";
        yield return new WaitForSeconds(2f);
        textBox.GetComponent<Text>().text = "";
        player.GetComponent<FirstPersonController>().enabled = true;
    }

}
