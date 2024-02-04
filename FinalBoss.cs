using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    public GameObject bulletPrefabLeft;
    public GameObject bulletPrefabRight;
    public GameObject barierPhase2Boss1;
    public GameObject ExitText;
    public GameObject redScreen;
    private Rigidbody2D rbPlayer;
    private GameObject playerGO;
    public float xSpeed = 0.3f;
    public float reloadTime = 3f;
    public float reloadTimeFast = 0.2f;
    public float reloadTimeSlow = 3f;
    public float redScreenTime = 0.8f;
    public bool isLeft;
    public bool isRedScreen = false;
    private float xSpawnPos;
    public float xSpawnPosRange= 2.5f;
    private float elapsedTime = 1f;
    public float elapsedTimeRedScreen = 1f;
    public float minSpawnYLimit = 1f;
    public float maxSpawnYLimit = 1f;
    public int health = 4; 
    public int healthPhase1 = 4;
    public int healthPhase2 = 4;
    public int healthPhase3 = 1;
    public Animator animator;
    public int countBullet = 1;
    public int bossPhase = 1;
    public Transform phase1pos2;
    public Transform phase1pos3;
    public Transform phase1pos4;
    public Transform phase2pos1;
    public Transform phase2pos2;
    public Transform phase2pos3;
    public Transform phase2pos4;
    public Transform phase3pos1;
    public Transform phase3pos2;
    public Transform tpPlayer;

    public int bulletFired = 0;
    
    void Spawn()
    {
        if(elapsedTime > reloadTime)
        {
        	bulletFired++;
            for(int i = 0; i < countBullet; i++)
            {    
                float random = Random.Range(minSpawnYLimit, maxSpawnYLimit);
                Vector3 spawnPos = transform.position;
                xSpawnPos = (isLeft) ? -xSpawnPosRange : xSpawnPosRange;
                spawnPos += new Vector3(xSpawnPos, random, 0f);
                if(isLeft){
                    Instantiate(bulletPrefabLeft, spawnPos, Quaternion.identity);
                }else{
                    Instantiate(bulletPrefabRight, spawnPos, Quaternion.identity);
                }
            }
            elapsedTime = 0f;
        }
    }
    void Start()
    {
        playerGO = GameObject.Find("Player");
        rbPlayer = playerGO.gameObject.GetComponent<Rigidbody2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        elapsedTimeRedScreen += Time.deltaTime;
        if(isRedScreen){
        	if(elapsedTimeRedScreen > redScreenTime){
        		redScreen.gameObject.SetActive(false);
        	}
        }
    }
    void LastUpdate()
    {
         
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 rotate = transform.eulerAngles;
        isLeft = (transform.localPosition.x > rbPlayer.position.x) ? true : false;
        
        if (collision.gameObject.tag == "Player")
        {
        	if(bossPhase == 1){
        		reloadTime = reloadTimeSlow;
        	}
        	else if(bossPhase == 2)
        	{
        		if(reloadTime == reloadTimeSlow && bulletFired == 3){
        			reloadTime = reloadTimeFast;
        			countBullet = 1;
        			bulletFired = 0;
        		}
        		else if(reloadTime == reloadTimeFast && bulletFired == 25){
        			reloadTime = reloadTimeSlow;
        			countBullet = 5;
        			bulletFired = 0;
        		}
        	}else if(bossPhase == 3){
        		
        	}else if(bossPhase == 4){
        		
        	}

            if (isLeft) // Movement
            {
                rotate.y = 180;
                transform.rotation = Quaternion.Euler(rotate);

            }
            else
            {
                rotate.y = 0;
                transform.rotation = Quaternion.Euler(rotate);
            }
            Spawn();
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Bullet"){
            Destroy(collision.gameObject);
            //Debug.Log("Bullet shot");
            Damage();
        }
        if(collision.gameObject.tag == "Player"){
          //  Debug.Log("Player shot");
            Damage();
        }
        if(collision.gameObject.tag == "FireShield"){
        //    Debug.Log("Fire shot");
            Damage();
        }
        Debug.Log(collision.gameObject.tag);


    }
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.tag == "FireShield"){
            //Debug.Log("Fire shot");
            Damage();
        }
        
    }
    public void Damage(){
    	if(bossPhase == 1)
    	{
        	if(health>1){
            	health--;
            	if(health == 3){
            		gameObject.transform.position = phase1pos2.transform.position;
            	}
            	if(health == 2){
            		gameObject.transform.position = phase1pos3.transform.position;
            	}
    	        if(health == 1){
            		gameObject.transform.position = phase1pos4.transform.position;
    	        }
        	}
        	else{
        		gameObject.transform.position = phase2pos1.transform.position;
        		bossPhase = 2;
            	health = healthPhase2;
               	animator.SetInteger("Phase", 2);
               	barierPhase2Boss1.gameObject.SetActive(false);
        	}       		
      	}
      	else if(bossPhase == 2)
      	{
        	if(health>1){
            	health--;
            	if(health == 3){
            		gameObject.transform.position = phase2pos2.transform.position;
            	}
            	if(health == 2){
            		gameObject.transform.position = phase2pos3.transform.position;
            	}
    	        if(health == 1){
            		gameObject.transform.position = phase2pos4.transform.position;
    	        }
        	}
        	else{
        		redScreen.gameObject.SetActive(true);
        		isRedScreen = true;
        		elapsedTimeRedScreen = 0f;
        		gameObject.transform.position = phase3pos1.transform.position;
        		bossPhase = 3;
            	health = healthPhase3;
            	animator.SetInteger("Phase", 3);      
            	playerGO.transform.position = tpPlayer.transform.position;
            }  	
       	}
       	else if(bossPhase == 3)
       	{
       		Destroy(gameObject);
        	ExitText.gameObject.SetActive(true);       
       	}
    }
}
