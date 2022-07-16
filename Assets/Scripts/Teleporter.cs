using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject teleporter_other;
    public Vector3 offset;
    public GameObject player;
    public bool arrived = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = teleporter_other.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(player.transform.position.x - transform.position.x) <= 0.25f
            && Math.Abs(player.transform.position.z - transform.position.z) <= 0.25f){
            if (arrived == false)
            {
                teleporter_other.GetComponent<Teleporter>().arrived = true;
                Time.timeScale = 0;
                player.transform.position += offset;
                Time.timeScale = 1;
            }
        }else{
            arrived = false;
		}
    }
}
