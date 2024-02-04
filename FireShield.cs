using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
	Vector3 offset = new Vector3(0f,0f,0f);
	public Rigidbody2D player;
	public GameObject playerGO;
	public float xOffset = 2.61f;
	public float yyy = 0f;
	public float zzz = 0f;
	//public EnemyScript escript;
    public float reloadTimeAttack = 0.5f;
    public bool isL;

    private float elapsedTimeAttack = 0f;
    
	void Start()
	{
		//Record the initial position offset
	}

	//Late update runs after all of the normal updates
	void LateUpdate()
	{
		//Update position to follow target while maintaining the offset
		PlayerMover playerMover = playerGO.GetComponent<PlayerMover>();
		isL = playerMover.isLeft;
		
		if(!isL){
			transform.position = new Vector3(player.position.x, player.position.y, zzz);
		}
		else{
			transform.position = new Vector3(player.position.x - xOffset, player.position.y, zzz);	
		}
		
	}

	private void Update(){
		elapsedTimeAttack += Time.deltaTime;
		}

	private void OnTriggerStay2D(Collider2D collision){
		if(collision.gameObject.tag == "Enemy"){
			if(elapsedTimeAttack >= reloadTimeAttack){
				collision.gameObject.GetComponent<EnemyScript>().Damage();
				elapsedTimeAttack = 0f;
			}
		}
		if(collision.gameObject.tag == "FinalBoss"){
			if(elapsedTimeAttack >= reloadTimeAttack){
				collision.gameObject.GetComponent<FinalBoss>().Damage();
				elapsedTimeAttack = 0f;
			}
		}
		if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "BulletBoss"){
			Destroy(collision.gameObject);
		}
	}
}
