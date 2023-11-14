using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkHit : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			EnemyHP enemyHPComponent = collision.gameObject.GetComponent<EnemyHP>();
			enemyHPComponent.TakeDamage((int)1);
		}
	}
}
