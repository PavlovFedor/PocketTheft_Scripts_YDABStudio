using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRight : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = GameObject.Find("Кнопочный");
        //EnemyScript enemyScript = go.GetComponent<EnemyScript>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //speed = (enemyScript.isLeft) ? -speed: speed;
        rb.transform.Rotate(0f,0f,90f);
        rb.velocity = new Vector2(speed, 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "FireShield")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
