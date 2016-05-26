using UnityEngine;
using System.Collections;

public struct GravityChange {
	public DeviceOrientation deviceOrientation;
	public Vector2 nGravity;
}

public class GChangeController : MonoBehaviour {

	//Delegate and event
	public delegate void GravityChangeDelegate(GravityChange gravity);
	public static event GravityChangeDelegate onGravityChange = delegate {};
	//List of movables
	private Rigidbody2D[] _movable;
	//players
	private Rigidbody2D _player;

	void Start () {

		_movable = null;
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Movable");
		//Getting all the rigidbodies: movable and player
		for (int i = 0; i < temp.Length; i++){
			_movable[i] = temp[i].GetComponent<Rigidbody2D>();
		}
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Checking if all the rigidbodies are colliding with something
	/// Prevents gravity switch in the middle of an action
	/// Make sure gravity effect is big enough to make the switch as quick as possible
	/// </summary>
	/// <returns><c>true</c>, if they're all in place, <c>false</c> otherwise.</returns>
	/// <param name="listToCheck">List to check.</param>
	bool Colliding(Rigidbody2D[] listToCheck){

		Debug.Log ("In first colliding method");
		//Raycast in the direction of the gravity, get the collider the rest hits and then IsTouching
		RaycastHit2D rayHit;

		//For loop on all the rigidBodies
		for (int i = 0; i < listToCheck.Length; i++) {

			//Raycast in the direction of the gravity to see if they are touching the element closest to them
			//If a box is over a box, the box under it will return false for as long as it doesn't touch a wall
			rayHit = Physics2D.Raycast (listToCheck [i].position, Physics2D.gravity, 1f);
			if (rayHit.collider == null || !rayHit.collider.IsTouching (listToCheck [i].GetComponent<Collider2D> ())) {
				return false;
				//RayCast collider in Unity changed to ignore themselves
				//Edit --> Project Settings --> Physics 2D --> Queries start in Collider (Unchecked)
			}
		}
		Debug.Log ("Do I get here?");
		return true;
	}

	/// <summary>
	/// Ovearloading
	/// Checking if all the rigidbodies are colliding with something
	/// Prevents gravity switch in the middle of an action
	/// Make sure gravity effect is big enough to make the switch as quick as possible
	/// </summary>
	/// <returns><c>true</c>, if they're all in place, <c>false</c> otherwise.</returns>
	/// <param name="listToCheck">List to check.</param>
	bool Colliding(Rigidbody2D elementToCheck){

		//Raycast in the direction of the gravity, get the collider the rest hits and then IsTouching
		RaycastHit2D rayHit;
		//Raycast in the direction of the gravity to see if they are touching the element closest to them
		//If a box is over a box, the box under it will return false for as long as it doesn't touch a wall
		rayHit = Physics2D.Raycast (elementToCheck.position, Physics2D.gravity, 1f);
		if (rayHit.collider == null || !rayHit.collider.IsTouching (elementToCheck.GetComponent<Collider2D> ())) {
			return false;
			//RayCast collider in Unity changed to ignore themselves
			//Edit --> Project Settings --> Physics 2D --> Queries start in Collider (Unchecked)
		}
		return true;
	}

	void Update () {

		//Creating a GravityChange for events
		GravityChange gravityChange = new GravityChange ();

		//Check if we aren't already in the given orientation
		//if (!Input.deviceOrientation.) {
		//If All the movable bodies are already colliding with something
		if(Colliding(_movable) && Colliding(_player)){
				//Editor and testing purposes
				if (Application.isEditor) {
					if(Input.GetKey(KeyCode.I)){
						gravityChange.nGravity = new Vector2 (0, -1f);
						gravityChange.deviceOrientation = DeviceOrientation.LandscapeLeft;
						onGravityChange (gravityChange);
					}
					if(Input.GetKey(KeyCode.J)){
						gravityChange.nGravity = new Vector2 (-1f, 0);
						gravityChange.deviceOrientation = DeviceOrientation.PortraitUpsideDown;
						onGravityChange (gravityChange);
					}
					if(Input.GetKey(KeyCode.K)){
						gravityChange.nGravity = new Vector2 (0, 1f);
						gravityChange.deviceOrientation = DeviceOrientation.LandscapeRight;
						onGravityChange (gravityChange);
					}
					if(Input.GetKey(KeyCode.M)){
						gravityChange.nGravity = new Vector2 (1f, 0);
						gravityChange.deviceOrientation = DeviceOrientation.Portrait;
						onGravityChange (gravityChange);
					}
				}

				//Mobile Phone settings
				switch (Input.deviceOrientation) {
				case DeviceOrientation.LandscapeLeft:
					gravityChange.nGravity = new Vector2 (0, -1f);
					gravityChange.deviceOrientation = DeviceOrientation.LandscapeLeft;
					onGravityChange (gravityChange);
					break;
				case DeviceOrientation.PortraitUpsideDown:
					gravityChange.nGravity = new Vector2 (-1f, 0);
					gravityChange.deviceOrientation = DeviceOrientation.PortraitUpsideDown;
					onGravityChange (gravityChange);
					break;
				case DeviceOrientation.LandscapeRight:
					gravityChange.nGravity = new Vector2 (0, 1f);
					gravityChange.deviceOrientation = DeviceOrientation.LandscapeRight;
					onGravityChange (gravityChange);
					break;
				case DeviceOrientation.Portrait:
					gravityChange.nGravity = new Vector2 (1f, 0);
					gravityChange.deviceOrientation = DeviceOrientation.Portrait;
					onGravityChange (gravityChange);
					break;
				}//end switch
			}//end if colliding
		//}//end if device orientation is different
	}//end Update
}//end class
