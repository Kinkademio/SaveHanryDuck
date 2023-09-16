using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Napravlenie;
    float Sila;
    public Rigidbody2D rb;
    int HP;


    void Start()
    {
        Napravlenie.x = 0;
        Napravlenie.y = 0;
        Sila = 40;

        rb = this.GetComponent< Rigidbody2D >();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Napravlenie.y = Napravlenie.y + 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Napravlenie.y = Napravlenie.y - 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Napravlenie.x = Napravlenie.x - 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Napravlenie.x = Napravlenie.x + 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Napravlenie.y = Napravlenie.y - 1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Napravlenie.y = Napravlenie.y + 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Napravlenie.x = Napravlenie.x + 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Napravlenie.x = Napravlenie.x - 1;
        }
    }

     void FixedUpdate()
    {
        // Скорость перемещения
        rb.AddForce(Napravlenie.normalized * Sila);

    }
}
