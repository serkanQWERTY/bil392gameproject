using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    public GameObject closedDoorText;
    public AudioSource doorSound;
    public float waitTime;


    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    void OnMouseOver()
    {
        if (theDistance <= 2)
        {
            actionKey.SetActive(true);
       
        }
        else
        {
            actionKey.SetActive(false);
           
        }

        if (Input.GetButton("Action"))
        {
            if (theDistance <= 2)
            {
                
                actionKey.SetActive(false);
                closedDoorText.SetActive(true);
                StartCoroutine(ClosedDoors());

                doorSound.Play();

            }
        }
    }

    void OnMouseExit()
    {
        actionKey.SetActive(false);
       
    }

    IEnumerator ClosedDoors()
    {
        yield return new WaitForSeconds(waitTime);
        closedDoorText.SetActive(false);
    }
}
