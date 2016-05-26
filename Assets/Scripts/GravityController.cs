using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	//List of rigidbodies
	private GameObject[] _movable;

	void Start () {
	
		//Getting all the rigidbodies: movable and player
		_movable = GameObject.FindGameObjectsWithTag("Movable");
		//Adding new method to event
		GChangeController.onGravityChange += OnGravityChange;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnGravityChange from the method list of GChangeController
	/// </summary>
	void OnDestroy(){
		GChangeController.onGravityChange -= OnGravityChange;	
	}

	/// <summary>
	/// Makes gameObjects with the Movable tag Kinematic or not depending on the bool given.
	/// </summary>
	/// <param name="isKinematicVar">If set to <c>true</c>, every "Movable" becomes kinematic, <c>false</c> otherwise.</param>
	/// <param name="listToChange">List of RigidBodies</param>
	void MakeKinematic(bool isKinematicVar, GameObject[] listToModify){

		Debug.Log ("in kinematic method");
		for (int i = 0; i < listToModify.Length; i++) {
			Debug.Log ("in for loop");
			//Ignoring the player because the player is always non-kinematic
			if (listToModify [i].gameObject.tag == "Movable") {
				listToModify [i].GetComponent<Rigidbody2D>().isKinematic = isKinematicVar;
				Debug.Log ("Changed kinematic");
			}
		}

	}

	void Update () {
	
	}

	void OnGravityChange(GravityChange gravityChange){

		Debug.Log (_movable.Length);
		Physics2D.gravity = gravityChange.nGravity;
	}
}
