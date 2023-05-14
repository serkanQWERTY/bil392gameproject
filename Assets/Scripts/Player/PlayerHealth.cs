using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerHealth : MonoBehaviour
{

    public FirstPersonController playerScript;

    public float currentHealth; //suanki canım
    public float maxHealth=100f; //max canım
    public static PlayerHealth PH;

    public Slider healthBarSlider;
    public Text healthText;

    public bool isDead=false;

    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    bool isTakingDamage = false;
    public float colorSpeed=5f;

    void Awake() //starttan önce calisir
    {
        PH = this;
    }
    void Start() //updateden once calisir
    {
        healthText.text = maxHealth.ToString();
        currentHealth = maxHealth;
        healthBarSlider.value = maxHealth;


    }

    
    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        if (isTakingDamage)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSpeed*Time.deltaTime);
        }
        isTakingDamage = false;
    }

    public void DamagePlayer(float damage)
    {

        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                isTakingDamage = true;
                Dead();
            }
            else
            {
                isTakingDamage = true;
                currentHealth -= damage;
                healthBarSlider.value -= damage;
                UpdateText();
            }
            
        }
    }
    public void UpdateText()
    {
        healthText.text = currentHealth.ToString();
    }
    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        healthBarSlider.value = 0;
        UpdateText();
        SceneManager.LoadScene(2);
        playerScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       

    }
}
