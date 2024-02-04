using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRight : MonoBehaviour
{
	public GameObject bulletPrefab;
	public float minSpawnYLimit = 1f;
	public float maxSpawnYLimit = 1f;
	public float reloadTime = 5f;
	public float attackTime = 1f;
	public float attackTimeInterval = 0.2f;
	public float xSpawnPosRange = 1f;
    public float elapsedTimeReload = 0f;
    
    private float elapsedTimeAttack = 0f;
    private float elapsedTimeAttackInterval = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTimeAttack += Time.deltaTime;
        elapsedTimeReload += Time.deltaTime;
        elapsedTimeAttackInterval += Time.deltaTime;
    
    	if(elapsedTimeReload > reloadTime)
        {                
            if(elapsedTimeReload > reloadTime && elapsedTimeReload < reloadTime+0.2f){
            	elapsedTimeAttack = 0f;
            }
        	if(elapsedTimeAttack < attackTime && elapsedTimeAttackInterval > attackTimeInterval){
        	    float random = Random.Range(minSpawnYLimit, maxSpawnYLimit);
    	        Vector3 spawnPos = transform.position;
            	spawnPos += new Vector3(xSpawnPosRange, random, 0f);
        	    Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
	            
            	elapsedTimeAttackInterval = 0f;
            }

            if(elapsedTimeAttack > attackTime){
            	elapsedTimeReload = 0f;
        	}
        }
    }
}
