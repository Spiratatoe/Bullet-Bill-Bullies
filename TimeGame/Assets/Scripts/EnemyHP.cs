using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float hp, maxHP = 3;
    private Animator mAnimator;

    [Header ("XP Components")]
    [SerializeField] private float XPValue = 10;

    public GameObject bottlePrefab;
    public Boolean isInvincible = false;
    
    [Header ("HP Bar Components")]
    [SerializeField] private Image bar_fill;
    public Color32 high_hp_colour;
    public Color32 half_hp_colour;
    public Color32 low_hp_colour;
    public Boolean isDead = false;

    [Header ("Damage Ticks")]
    [SerializeField] private TickSpawner tickSpawner;

    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        hp = maxHP;

        // hp bar stuff
        high_hp_colour = new Color32(157,255,140,255); // RGBA
        half_hp_colour = new Color32(255,255,140,255); // RGBA
        low_hp_colour = new Color32(255,140,140,255); // RGBA

        float fillAmount = hp / maxHP;
        bar_fill.fillAmount = fillAmount;
        bar_fill.color = high_hp_colour;

        // set camera for the hp bar
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject.Find("HPBarCanvas").GetComponent<Canvas>().worldCamera = mainCamera;
    }

    private void Update() {
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible == false)
        {
            hp -= damage;
            if (damage > 0 ) { tickSpawner.SpawnMessage(damage.ToString()); }
            mAnimator.SetBool("isTakingDamage", true);
            StartCoroutine(TookDamage());
        }
    }

    public Boolean getPercentage()
    {
        //returns if the enemy hp is below 60%
        if (hp < 0.4 * maxHP)
        {
            return true;
        }

        return false;
    }
    public void toggleInvincibleOn()
    {
        isInvincible = true;
    }

    public void toggleInvincibleOff()
    {
        isInvincible = false;
    }

    private IEnumerator TookDamage()
    {

        if (hp > 0) //check if hase died
        {
            if (isInvincible) //set it faster for king
            { mAnimator.SetBool("isTakingDamage", false);
            }
            yield return new WaitForSeconds(1f);
            mAnimator.SetBool("isTakingDamage", false);
        }
        else
        {
            mAnimator.SetBool("isDying", true);
            isDead = true;
            yield return new WaitForSeconds(1.5f);

            //create an XP bottle
            GameObject newBottle = Instantiate(bottlePrefab, transform.parent.parent);
            newBottle.transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
            newBottle.GetComponent<XPBottle>().value = XPValue; 
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = hp / maxHP;
        bar_fill.fillAmount = fillAmount;

        Color32 healthColor;

        if (fillAmount >= 0.7f) { healthColor = high_hp_colour; }
        else if (fillAmount < 0.7f && fillAmount >= 0.4f) { healthColor = half_hp_colour; }
        else { healthColor = low_hp_colour; }
        bar_fill.color = healthColor;
    }

}
