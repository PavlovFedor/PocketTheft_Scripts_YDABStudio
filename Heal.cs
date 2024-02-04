using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
	public Rigidbody2D player;
	public EnemyScript escript;
				 
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerMover>().Heal();
			Destroy(gameObject);
		}
		if(collision.gameObject.tag == "Bullet"){
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}