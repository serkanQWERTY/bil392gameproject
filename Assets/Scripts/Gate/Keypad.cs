using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
public class Keypad : MonoBehaviour
{

    public DoorController doorToOpen;
    public GameObject keypadUI;
    public Text passwordText;
    public string password;
    public GameObject dropText;

    public FirstPersonController playerScript;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerScript.enabled = true;
            keypadUI.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            keypadUI.SetActive(true);
            playerScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            dropText.SetActive(true);

        }
    }

    public void KeyButton(string key)
    {
        passwordText.text = passwordText.text + key;

    }

    public void ResetPassword()
    {
        passwordText.text = "";
    }

    public void CheckPassword()
    {
       if (passwordText.text == password)
        {
            doorToOpen.isLocked = false;
            doorToOpen.CheckDoor();
            keypadUI.SetActive(false);
            playerScript.enabled = true;
            SceneManager.LoadScene(3);
        }
        else
        {
            ResetPassword();
        }
    }
}
