using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharController : MonoBehaviour
{
    public Animator animator;
    public bool moving = false;
    public string moveDirection = string.Empty;
    public float PercentMove = 0;
    public int moveDistance = 0;
    private Face[] faces;
    public bool pause = false;
    private Stack<Vector3> positionStack;
    private Stack<Quaternion> rotationStack;

	internal void Die()
	{
		//throw new System.NotImplementedException();
	}

	// Start is called before the first frame update
	void Start()
    {
        positionStack = new Stack<Vector3>();
        rotationStack = new Stack<Quaternion>();
        animator = GetComponent<Animator>();
        faces = GetComponentsInChildren<Face>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (!moving)
            {
                if(Input.GetKeyDown(KeyCode.Z)){
                    transform.position = positionStack.Pop();
                    transform.rotation = rotationStack.Pop();
				}
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    positionStack.Push(transform.position);
                    rotationStack.Push(transform.rotation);
                    moveDirection = "Up";
                    moving = true;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    positionStack.Push(transform.position);
                    rotationStack.Push(transform.rotation);
                    moveDirection = "Down";
                    moving = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    positionStack.Push(transform.position);
                    rotationStack.Push(transform.rotation);
                    moveDirection = "Left";
                    moving = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    positionStack.Push(transform.position);
                    rotationStack.Push(transform.rotation);
                    moveDirection = "Right";
                    moving = true;
                }
                if (moving)
                {
                    foreach (Face f in faces)
                    {
                        if (f != null)
                        {
                            if (f.faceUp)
                            {
                                moveDistance = f.faceNum;
                            }
                        }
                    }
                }
            }
            else
            {
                var pm = 0f;
                if (PercentMove < 1)
                {
                    pm = Time.deltaTime * 6;
                    if (PercentMove + pm > 1)
                    {
                        pm = 1 - PercentMove;
                        moveDistance--;
                    }
                    PercentMove += pm;
                }
                else
                {
                    if (moveDistance == 0)
                        moving = false;
                    PercentMove = 0;
                }
                if (PercentMove > 1)
                {
                    pm = PercentMove - pm;
                    PercentMove = 1;
                }
                if (moveDirection == "Up")
                {
                    transform.Rotate(Vector3.left, -90 * pm, Space.World);
                    transform.position = transform.position + new Vector3(0, 0, 1 * pm);
                }
                if (moveDirection == "Down")
                {
                    transform.Rotate(Vector3.left, 90 * pm, Space.World);
                    transform.position = transform.position + new Vector3(0, 0, -1 * pm);
                }
                if (moveDirection == "Left")
                {
                    transform.Rotate(Vector3.forward, 90 * pm, Space.World);
                    transform.position = transform.position + new Vector3(-1 * pm, 0, 0);
                }
                if (moveDirection == "Right")
                {
                    transform.Rotate(Vector3.forward, -90 * pm, Space.World);
                    transform.position = transform.position + new Vector3(1 * pm, 0, 0);
                }
            }
        }
    }       
}