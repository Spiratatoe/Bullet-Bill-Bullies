using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkHit : MonoBehaviour
{
	// Start is called before the first frame update
	private static float damage = 1f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			EnemyHP enemyHPComponent = collision.gameObject.GetComponent<EnemyHP>();
			enemyHPComponent.TakeDamage(damage);
		}
	}

	public void IncrementDamage()
	{
		damage += 0.5f;
		Debug.Log("Damage: " + damage);
	}
}
