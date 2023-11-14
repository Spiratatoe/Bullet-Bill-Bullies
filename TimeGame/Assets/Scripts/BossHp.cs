using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    [SerializeField] int hp, maxHP = 3;
    private Animator mAnimator;
    private bool mDying = false;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        hp = maxHP;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        mAnimator.SetBool("isTakingDamage", true);
        StartCoroutine(TookDamage());
    }

    private IEnumerator TookDamage()
    {

        if (hp > 0) //check if hase died
        {
            yield return new WaitForSeconds(1f);
            mAnimator.SetBool("isTakingDamage", false);
        }
        else
        {
            mAnimator.SetBool("isDying", true);
            mDying = true;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
