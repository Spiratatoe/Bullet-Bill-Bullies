using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkHit : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();
			enemyComponent.TakeDamage((int)1);
		}
	}
}
