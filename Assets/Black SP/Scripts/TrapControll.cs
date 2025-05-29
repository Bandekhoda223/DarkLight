using UnityEngine;
using System.Collections;

public class TrapControll : MonoBehaviour {

	public bool rotationInStart = false;
	public float speed_rotation = -5;
	
	void Start () {
		if(speed_rotation>0){
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,-gameObject.transform.localScale.y,1);
		}
	}
	

	void FixedUpdate () {
		if (rotationInStart) {
				gameObject.transform.Rotate(0,0,speed_rotation);
		}
	}
}
