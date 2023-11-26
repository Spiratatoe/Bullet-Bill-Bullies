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
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        hp = maxHP;
    }

    public void TakeDamage(float damage)
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
            yield return new WaitForSeconds(1f);

            //create an XP bottle
            GameObject newBottle = Instantiate(bottlePrefab, transform.parent.parent);
            newBottle.transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
            newBottle.GetComponent<XPBottle>().value = XPValue; 
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
