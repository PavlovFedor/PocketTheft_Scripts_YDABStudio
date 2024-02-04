using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public GameObject bulletPrefabLeft;
    public GameObject bulletPrefabRight;
    private Rigidbody2D rbPlayer;
    private GameObject playerGO;
    public float xSpeed = 0.3f;
    public float reloadTime = 3f;
    public bool isLeft;
    private float xSpawnPos;
    public float xSpawnPosRange= 2.5f;
    private float elapsedTime = 1f;
    public float minSpawnYLimit = 1f;
    public float maxSpawnYLimit = 1f;
    public int health = 10;
    public Animator animator;
    public int countBullet = 1;
    void Spawn()
    {
        if(elapsedTime > reloadTime)
        {
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
            Spawn();
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
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Игрок входит, агро-режим вкл

        
        if (collision.gameObject.name == "Player")
        {
            //вкл злая анимация
            Spawn();
            animator.SetBool("isAngry",true);
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
    }
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.tag == "FireShield"){
            //Debug.Log("Fire shot");
            Damage();
        }  
    }
    public void Damage(){
        if(health>1){
            health--;
            animator.SetInteger("HP",health);
        }
        else{

            Destroy(gameObject);
        }
    }
}
