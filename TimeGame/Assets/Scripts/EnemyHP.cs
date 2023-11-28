using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float hp, maxHP = 3;
    private Animator mAnimator;
    [Header ("XP Components")]
    [SerializeField] private float XPValue = 10;
    public GameObject bottlePrefab;

    public Boolean isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        hp = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible == false)
        {
            hp -= damage;
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
            yield return new WaitForSeconds(1f);

            //create an XP bottle
            // GameObject newBottle = Instantiate(bottlePrefab, transform.parent.parent);
            // newBottle.transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
            // newBottle.GetComponent<XPBottle>().value = XPValue; 
            // Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
