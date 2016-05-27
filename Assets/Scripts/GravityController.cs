using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	//List of rigidbodies
	private GameObject[] _movable;
	private GameObject _player;

	void Start () {
	
		//Getting all the rigidbodies: movable and player
		_movable = GameObject.FindGameObjectsWithTag("Movable");
		_player = GameObject.FindGameObjectWithTag("Player");
		//Adding new method to event
		GravityControllerEvent.onGravityChange += OnGravityChange;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnGravityChange from the method list of GChangeController
	/// </summary>
	void OnDestroy(){
		GravityControllerEvent.onGravityChange -= OnGravityChange;	
	}

	void Update () {
		
		if (GravityControllerEvent.Colliding (_movable) && !_player.GetComponent<PlayerController>().isHolding) {
			for (int i = 0; i < _movable.Length; i++) {
				if (Mathf.Approximately(Physics2D.gravity.x, 0f)) {
					_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
				} else if (Mathf.Approximately(Physics2D.gravity.y, 0f)) {
					_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
				}
				_movable[i].GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezeRotation;
			}
		}
	}

	void OnGravityChange(GravityChange gravityChange){

		for (int i = 0; i < _movable.Length; i++) {
			_movable [i].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		}

		Physics2D.gravity = gravityChange.nGravity;
	}
}
