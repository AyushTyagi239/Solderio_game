using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Rigidbody2D myrigidbody;
    PlayerMovement player;
    float xspeed;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xspeed = player.transform.localScale.x*bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        myrigidbody.velocity = new Vector2(xspeed, 0f);
    }
     void OnTriggerEnter2D(Collider2D other)
{
    if (other.tag =="enemy")
    {
        Destroy(other.gameObject);
    }
    Destroy(gameObject);
     if (other.tag =="floor")
    {
        Destroy(gameObject);
    }
   
}
    void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);
    }
}

