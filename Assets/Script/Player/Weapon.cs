using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool Player;

    Camera MainCamera;
    public GameObject target;
    public TrailRenderer tracerEffect;

    Vector2 targetPosition;
    Vector2 lookDirection;



    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Player) {
            targetPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        } else {
            targetPosition = target.transform.position;
        }
        
        lookDirection = new Vector2(targetPosition.x - this.GetComponent<Transform>().position.x, targetPosition.y - this.GetComponent<Transform>().position.y);

        if (Player)
        {
            PlayerShooting();
        }
        else
        {
            BotShooting();
        }
    }

    void BotShooting()
    {

    }

    void PlayerShooting()
    {
        if (Input.GetKeyDown(InputController.getInput("shooting")))
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(this.GetComponent<Transform>().position, lookDirection, 25f);
            //Debug.DrawRay(this.GetComponent<Transform>().position, lookDirection, Color.red, 1.0f);

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
    }



}
