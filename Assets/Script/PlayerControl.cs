using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Napravlenie;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sprite;

    public bool keyboardActive;
    float playerSpeed;
    int HP;


    void Start()
    {
        Napravlenie.x = 0;
        Napravlenie.y = 0;
        playerSpeed = 40;
        //keyboardActive = false;

        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent< Rigidbody2D >();
    }

    void Update()
    {
        //if(Input.GetKey(KeyCode.W)) { 
        //    rb.velocity = new Vector2(rb.velocity.x, playerSpeed); 
        //}
        //if (Input.GetKey(KeyCode.A)) { 
        //    rb.velocity = new Vector2(-playerSpeed, rb.velocity.y); 
        //}
        //if (Input.GetKey(KeyCode.S)) { 
        //    rb.velocity = new Vector2(rb.velocity.x, -playerSpeed); 
        //}
        //if (Input.GetKey(KeyCode.D)) { 
        //    rb.velocity = new Vector2(playerSpeed, rb.velocity.y); 
        //}


        if (keyboardActive)
        {
            if (Input.GetKeyDown(KeyCode.W)) { Napravlenie.y = Napravlenie.y + 1; }
            if (Input.GetKeyUp(KeyCode.W)) { Napravlenie.y = Napravlenie.y - 1; }

            if (Input.GetKeyDown(KeyCode.A)) { Napravlenie.x = Napravlenie.x - 1; sprite.flipX = false; }
            if (Input.GetKeyUp(KeyCode.A)) { Napravlenie.x = Napravlenie.x + 1; }

            if (Input.GetKeyDown(KeyCode.S)) { Napravlenie.y = Napravlenie.y - 1; }
            if (Input.GetKeyUp(KeyCode.S)) { Napravlenie.y = Napravlenie.y + 1; }

            if (Input.GetKeyDown(KeyCode.D)) { Napravlenie.x = Napravlenie.x + 1; sprite.flipX = true; }
            if (Input.GetKeyUp(KeyCode.D)) { Napravlenie.x = Napravlenie.x - 1; }

            if ((Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S)) || 
                (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D)))
            { 
                animator.Play("Utka_go"); 
            } 
            else { animator.Play("Utka_idle"); }
        }
    }

    void FixedUpdate()
    {
        // Скорость перемещения
        rb.AddForce(Napravlenie.normalized * playerSpeed);
    }

    public void SetKeyboardActive(bool Active)
    {
        keyboardActive = Active;

        Napravlenie.x = 0;
        Napravlenie.y = 0;
    }
}
