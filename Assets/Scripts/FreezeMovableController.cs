using UnityEngine;
using System.Collections;

public class FreezeMovableController : MonoBehaviour {

	private GameObject[] _movable;

	void Start () {
		_movable = GameObject.FindGameObjectsWithTag("Movable");
		GravityControllerEvent.onGravityChange += OnGravityChange;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnGravityChange from the method list of GravityControllerEvent
	/// </summary>
	void OnDestroy(){
		GravityControllerEvent.onGravityChange -= OnGravityChange;
	}

	void Update () {
	
	}

	void OnGravityChange(GravityChange gravity){

		for (int i = 0; i < _movable.Length; i++) {
			_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

			if (gravity.nGravity.x == 0f) {
				_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
			}
			if (gravity.nGravity.y == 0f) {
				_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
			}

			_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
