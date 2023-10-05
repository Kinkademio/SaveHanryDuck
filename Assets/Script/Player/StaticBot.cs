using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBot : MonoBehaviour
{
    //[SerializeField]
    GameObject target;
    Weapon weapon;

    float targetDistance;

    private void Start()
    {
        weapon = gameObject.GetComponent<Weapon>();
        target = GameObject.Find("Duck");

        gameObject.GetComponent<Weapon>().target = target;
    }

    void Update()
    {
        targetDistance = Mathf.Sqrt (
            (target.transform.position.x - gameObject.transform.position.x) * (target.transform.position.x - gameObject.transform.position.x) +
            (target.transform.position.y - gameObject.transform.position.y) * (target.transform.position.y - gameObject.transform.position.y));

        if (targetDistance < 1.5)
        {
            weapon.SetShoot(true);
        }
        else
        {
            weapon.SetShoot(false);
        }
    }
}
