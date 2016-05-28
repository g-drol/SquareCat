using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	//List of rigidbodies
	private GameObject[] _movable;

	public static DeviceOrientation orientation { get; private set;}

	void Start () {
		//Getting all the rigidbodies: movable and player
		_movable = GameObject.FindGameObjectsWithTag("Movable");
		//Adding new method to event
		orientation = DeviceOrientation.LandscapeLeft;
		GravityControllerEvent.onGravityChange += OnGravityChange;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnGravityChange from the method list of GChangeController
	/// </summary>
	void OnDestroy(){
		GravityControllerEvent.onGravityChange -= OnGravityChange;	
	}

	void OnGravityChange(GravityChange gravityChange){

		Physics2D.gravity = gravityChange.nGravity;
		orientation = gravityChange.deviceOrientation;

		for (int i = 0; i < _movable.Length; i++) {
			_movable [i].GetComponent<Rigidbody2D> ().isKinematic = true;

			if (gravityChange.deviceOrientation == DeviceOrientation.LandscapeLeft || gravityChange.deviceOrientation == DeviceOrientation.LandscapeRight) {
				_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			} else if (gravityChange.deviceOrientation == DeviceOrientation.Portrait || gravityChange.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
				_movable[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			}

			_movable [i].GetComponent<Rigidbody2D> ().isKinematic = false;
		}
	}
}
