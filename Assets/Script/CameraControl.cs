using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float dumping = 1.8f;
    public GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Duck");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, target, dumping * Time.deltaTime);
    }
}
