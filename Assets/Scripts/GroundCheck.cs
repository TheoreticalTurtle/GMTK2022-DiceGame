using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool onGround = true;

	private void OnTriggerExit(Collider other)
	{
		onGround = false;
		foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Ground")){
			if(Vector3.Distance(gameObject.transform.position, transform.position) < .25f){
				onGround = true;
			}
		}
	}
}
