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
            if (Input.GetKeyDown(InputController.getInput("up"))) { Napravlenie.y = Napravlenie.y + 1; }
            if (Input.GetKeyUp(InputController.getInput("up"))) { Napravlenie.y = Napravlenie.y - 1; }

            if (Input.GetKeyDown(InputController.getInput("left"))) { Napravlenie.x = Napravlenie.x - 1; sprite.flipX = false; }
            if (Input.GetKeyUp(InputController.getInput("left"))) { Napravlenie.x = Napravlenie.x + 1; }

            if (Input.GetKeyDown(InputController.getInput("down"))) { Napravlenie.y = Napravlenie.y - 1; }
            if (Input.GetKeyUp(InputController.getInput("down"))) { Napravlenie.y = Napravlenie.y + 1; }

            if (Input.GetKeyDown(InputController.getInput("right"))) { Napravlenie.x = Napravlenie.x + 1; sprite.flipX = true; }
            if (Input.GetKeyUp(InputController.getInput("right"))) { Napravlenie.x = Napravlenie.x - 1; }

            if ((Input.GetKey(InputController.getInput("up")) ^ Input.GetKey(InputController.getInput("down"))) || 
                (Input.GetKey(InputController.getInput("left")) ^ Input.GetKey(InputController.getInput("right"))))
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
