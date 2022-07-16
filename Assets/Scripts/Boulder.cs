using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public CharController player;
    public bool moving = false;
    public bool falling = false;
    public bool fallen = false;
    public char moveDirection;
    public Vector3 cur_position;
    private Stack<Vector3> positionStack;
    private Stack<Quaternion> rotationStack;
    private Stack<bool> fallenStack;

    // Start is called before the first frame update
    void Start()
    {
        positionStack = new Stack<Vector3>();
        rotationStack = new Stack<Quaternion>();
        fallenStack = new Stack<bool>();
        cur_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position = positionStack.Pop();
            cur_position = transform.position;
            moving = false;
            transform.rotation = rotationStack.Pop();
            fallen = fallenStack.Pop();
        }
        
        if (moving)
        {
            if (moveDirection == 'R')
            {
                transform.position = Vector3.Lerp(transform.position, cur_position + Vector3.right, Time.deltaTime*10);
                if(Vector3.Distance(transform.position,cur_position + Vector3.right) < .05){
                    transform.position = cur_position + Vector3.right;
                    cur_position = transform.position;
                    moving = false;
                    checkFall();
                }
            }
            if (moveDirection == 'L')
            {
                transform.position = Vector3.Lerp(transform.position, cur_position + Vector3.left, Time.deltaTime * 10);
                if (Vector3.Distance(transform.position, cur_position + Vector3.left) < .05)
                {
                    transform.position = cur_position + Vector3.left;
                    cur_position = transform.position;
                    moving = false;
                    checkFall();
                }
            }
            if (moveDirection == 'U')
            {
                transform.position = Vector3.Lerp(transform.position, cur_position + Vector3.forward, Time.deltaTime * 10);
                if (Vector3.Distance(transform.position, cur_position + Vector3.forward) < .05)
                {
                    transform.position = cur_position + Vector3.forward;
                    cur_position = transform.position;
                    moving = false;
                    checkFall();
                }
            }
            if (moveDirection == 'D')
            {
                transform.position = Vector3.Lerp(transform.position, cur_position + Vector3.back, Time.deltaTime * 10);
                if (Vector3.Distance(transform.position, cur_position + Vector3.back) < .05)
                {
                    transform.position = cur_position + Vector3.back;
                    cur_position = transform.position;
                    moving = false;
                    checkFall();
                }
            }
        }
        else if(falling){
            transform.position = Vector3.Lerp(transform.position, cur_position + Vector3.down, Time.deltaTime * 10);
            if (Vector3.Distance(transform.position, cur_position + Vector3.down) < .05)
            {
                transform.position = cur_position + Vector3.down;
                cur_position = transform.position;
                falling = false;
                fallen = true;
            }
        }
        else if(player != null){
            player.pause = false;
            player = null;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)
        || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)
        || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)
        || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            positionStack.Push(transform.position);
            rotationStack.Push(transform.rotation);
            fallenStack.Push(fallen);
        }
    }

    private void checkFall()
    {
        falling = !GetComponentInChildren<GroundCheck>().onGround;
    }


    private void OnTriggerEnter(Collider other)
	{
        if (!fallen)
        {
            player = other.GetComponent<CharController>();
            if (player != null)
            {
                Debug.Log("hit Boulder");
                player.pause = true;
                moving = true;
                if(player.transform.position.x < transform.position.x && Math.Abs(transform.position.z - player.transform.position.z) < 0.1)
                    moveDirection = 'R';
                else if (player.transform.position.x > transform.position.x && Math.Abs(transform.position.z - player.transform.position.z) < 0.1)
                    moveDirection = 'L';
                else if (player.transform.position.z < transform.position.z && Math.Abs(transform.position.x - player.transform.position.x) < 0.1)
                    moveDirection = 'U';
                else if (player.transform.position.z > transform.position.z && Math.Abs(transform.position.x - player.transform.position.x) < 0.1)
                    moveDirection = 'D';
            }
        }
	}
}
