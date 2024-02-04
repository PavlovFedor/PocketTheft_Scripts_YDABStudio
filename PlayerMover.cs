using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public GameObject bulletPrefab;
    public bool isGrounded;
    public bool isKneel;
    public bool isFire;
    public bool isLeft;
    public GameObject player;
    public Rigidbody2D rb;
    public float xSpeed = 0.3f;
    public float xSlowSpeed = 2.5f;
    public float xRunSpeed = 5f;
    public float jumpForce = 1f;
    public float reloadTime = 1f;
    public float fireReloadTime = 8f;
    public Animator animator;
    public GameObject fireGO;
    public int hpHeal = 2;

    private float xSpawnPos;
    private float elapsedTime = 0f;
    private float fireElapsedTime = 9f;
    private float fireTime = 1.5f;

    public int health = 3;

    public int jump = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        fireElapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && jump < 1)
        {
            jump++;
            if (jump == 1)
            {
                rb.AddForce(new Vector2(rb.velocity.x,jumpForce), ForceMode2D.Impulse);
            }
            if (jump == 2)
            {
                rb.AddForce(new Vector2(rb.velocity.x,jumpForce), ForceMode2D.Impulse);   
            }
        }

        if (isGrounded)
        {
            if (jump!=0)
            {
                jump=0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {

            if(!isKneel && !isFire){
                transform.localScale = new Vector2(1f,0.5f);
                xSpeed = xSlowSpeed;
            }
            isKneel = true;
            //animator.SetBool("isKneel",isKneel);
        }
        if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            xSpeed = xRunSpeed;
            transform.localScale = new Vector2(1f,1f);
            isKneel = false;
            //animator.SetBool("isKneel",isKneel);
        }
        
        if(fireElapsedTime > fireTime && isFire){
            fireGO.gameObject.SetActive(false);
            isFire = false;
            animator.SetBool("isFire",false);
        }
        if (Input.GetMouseButtonDown(1) && fireElapsedTime > fireReloadTime && !isKneel)
        {
            Debug.Log(fireElapsedTime+" / "+ fireReloadTime);
            fireGO.gameObject.SetActive(true);
            isFire = true;
            animator.SetBool("isFire",true);
            fireElapsedTime = 0f;
        }
        if (Input.GetMouseButtonDown(0) && elapsedTime > reloadTime && !isFire)
        {
            Vector3 spawnPos =transform.position;
            xSpawnPos = (isLeft) ? -1.2f : 1.2f;
            spawnPos += new Vector3(xSpawnPos, 0f, 0f);
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            elapsedTime = 0f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotate = transform.eulerAngles;

        float xInput = Input.GetAxis("Horizontal");
        
        if (xInput < 0) // Movement
        {
            rotate.y = 180;
            transform.rotation = Quaternion.Euler(rotate);
            isLeft = true;
        }
        else if(xInput > 0) 
        {
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
            isLeft = false;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * xSpeed, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Ground") || collision.GetComponent<Collider2D>().CompareTag("Enemy"))
        {
            if (collision.GetComponent<Collider2D>().isTrigger == false){
                isGrounded = true;      
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Ground") || collision.GetComponent<Collider2D>().CompareTag("Enemy") || collision.GetComponent<Collider2D>().CompareTag("FinalBoss"))
        {
            if (collision.GetComponent<Collider2D>().isTrigger == false){
                isGrounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Ground") || collision.GetComponent<Collider2D>().CompareTag("Enemy") || collision.GetComponent<Collider2D>().CompareTag("FinalBoss"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "BulletBoss"){
            Damage();
            Destroy(collision.gameObject);
        }
    }
    public void Damage(){
        if(health>1){
            health--;
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void Heal(){
        health += hpHeal;
    }
}
