using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	//List of rigidbodies
	private GameObject[] _movable;

	void Start () {
	
		//Getting all the rigidbodies: movable and player
		_movable = GameObject.FindGameObjectsWithTag("Movable");
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

	/// <summary>
	/// Makes gameObjects with the Movable tag Kinematic or not depending on the bool given.
	/// </summary>
	/// <param name="isKinematicVar">If set to <c>true</c>, every "Movable" becomes kinematic, <c>false</c> otherwise.</param>
	/// <param name="listToChange">List of RigidBodies</param>
	void MakeNonKinematic(){

		Debug.Log ("in kinematic method");
		for (int i = 0; i < _movable.Length; i++) {
			Debug.Log ("in for loop");
			_movable [i].gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			Debug.Log ("Changed kinematic");
		}

	}

	void Update () {
	
	}

	void OnGravityChange(GravityChange gravityChange){
		Physics2D.gravity = gravityChange.nGravity;
	}
}
