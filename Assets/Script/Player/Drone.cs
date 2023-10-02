using RoomInteriorGeneratorTag;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Drone : MonoBehaviour
{
    float dumping = 5.4f;
    public GameObject Player;
    public Camera MainCamera;
    public SpriteRenderer sprite;
    public TrailRenderer tracerEffect;

    public float angle;

    Vector2 MousePosition;

    void Start()
    {
        Player = GameObject.Find("Duck");
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.5f, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, target, dumping * Time.deltaTime);

        MousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = new Vector2 (MousePosition.x - this.GetComponent<Transform>().position.x, MousePosition.y - this.GetComponent<Transform>().position.y);
        float realAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float animationAngle;

        if (90 < Mathf.Abs(realAngle))
        {
            sprite.flipX = true;
            animationAngle = realAngle;
        }
        else
        {
            sprite.flipX = false;
            animationAngle = realAngle + 180;
        }

        Quaternion quaternion = Quaternion.identity * Quaternion.Euler(this.GetComponent<Transform>().rotation.x, this.GetComponent<Transform>().rotation.y, animationAngle);
        this.GetComponent<Transform>().rotation = quaternion;


        if (Input.GetKeyDown(InputController.getInput("shooting"))) 
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(this.GetComponent<Transform>().position, lookDirection, 25f);
            Debug.DrawRay(this.GetComponent<Transform>().position, lookDirection, Color.red, 1.0f);

            for (int i = 0; i < hit.Length; i++)
            {
                if ((hit[i].point.x == this.GetComponent<Transform>().position.x) && (hit[i].point.y == this.GetComponent<Transform>().position.y))
                {
                    continue;
                }

                if ((hit[i].transform.gameObject.GetComponent<Health>() != null) && (!hit[i].collider.isTrigger))
                {
                    TrailRenderer tracer = Instantiate(tracerEffect, this.GetComponent<Transform>().position, Quaternion.identity);
                    tracer.AddPosition(this.GetComponent<Transform>().position);

                    tracer.transform.position = hit[i].point;

                    hit[i].transform.gameObject.GetComponent<Health>().TakeDamage(1);
                    break;
                }
            }
        }



        //+= transform.forward* Time.deltaTime * mw * wheel_speed;
    }
}
