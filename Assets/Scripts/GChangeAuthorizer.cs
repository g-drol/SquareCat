using UnityEngine;
using System.Collections;


public class GChangeAuthorizer : MonoBehaviour {

	//List of rigidbodies
	private Rigidbody2D[] _movableAndPlayer;

	// Use this for initialization
	void Start () {
		_movableAndPlayer = GetComponents<Rigidbody2D> ();
	}

	bool AllColliding(Rigidbody2D[] listToCheck){
		//Raycast in the direction of the gravity, get the collider the rest hits and then IsTouching
		//Physics2D.gravity
		RaycastHit2D rayHit;
		for (int i = 0; i < listToCheck.Length; i++) {
			rayHit = Physics2D.Raycast (listToCheck [i].position, Physics2D.gravity, 1f);
			if (rayHit.collider == null || !rayHit.collider.IsTouching (listToCheck [i].GetComponent<Collider2D> ())) {
				//Yep, looks good. Might need to make a layer for the non-moving things or you might hit the box itself there.
				//Problem would be that you want the thing without one... 
				//or actually no cause if you have a box on top of another one it should count as on the ground, right?
				//Actually no cause what you really want is to ignore yourself... gotta start the raycast on the edge of the box I think.
				//Or do a RaycastAll (which gives you an array of everything it hits) and check for collision only with things 
				//that aren't you.
				return false;
			}
		}
		return true;
	}

	void MakeKinematic(bool isKinematicVar){
		for (int i = 0; i < _movableAndPlayer.Length; i++) {
			if (_movableAndPlayer [i].gameObject.tag == "Movable") {
				_movableAndPlayer [i].isKinematic = isKinematicVar;
			}
		}
	}

	void ReleaseAllForces(){
		for (int i = 0; i < _movableAndPlayer.Length; i++) {
			//_movableAndPlayer [i].velocity = 0f;
		}
	}

	// Update is called once per frame
	void Update () {


	}
}
