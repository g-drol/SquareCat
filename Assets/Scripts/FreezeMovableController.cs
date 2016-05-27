using UnityEngine;
using System.Collections;

public class FreezeMovableController : MonoBehaviour {

	private Rigidbody2D _movable;

	void Start () {
		_movable = GetComponent<Rigidbody2D> ();
	}

	void Update () {
	
	}

	void OnGravityChange(GravityChange gravity){
		if (gravity.nGravity.x == 0f) {
			
		
		}
	
	}
}
