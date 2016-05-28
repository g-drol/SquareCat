using UnityEngine;
using System.Collections;

public struct GravityChange {
	public DeviceOrientation deviceOrientation;
	public Vector2 nGravity;
}

public class GravityControllerEvent : MonoBehaviour {

	//Delegate and event
	public delegate void GravityChangeDelegate(GravityChange gravity);
	public static event GravityChangeDelegate onGravityChange = delegate {};
	//List of movables
	private Movable[] _movable;
	private DeviceOrientation _previousOrientation;

	void Start () {
		_movable = FindObjectsOfType<Movable>();
		_previousOrientation = DeviceOrientation.LandscapeLeft;
	}

	/// <summary>
	/// Checking if all the rigidbodies are colliding with something
	/// Prevents gravity switch in the middle of an action
	/// Make sure gravity effect is big enough to make the switch as quick as possible
	/// </summary>
	/// <returns><c>true</c>, if they're all in place, <c>false</c> otherwise.</returns>
	/// <param name="listToCheck">List to check.</param>
	public static bool Colliding(Movable[] listToCheck){
		for (int i = 0; i < listToCheck.Length; i++) {
			if (!listToCheck [i].IsGrounded()) {
					return false;
			}
		}
		return true;
	}

	void FixedUpdate () {

		//Creating a GravityChange for events
		GravityChange gravityChange = new GravityChange ();
		gravityChange.deviceOrientation = _previousOrientation;
		//If All the movable bodies are already colliding with something
		if(Colliding(_movable)){
			//Editor and testing purposes
			if (Application.isEditor) {
				if(Input.GetKey(KeyCode.I)){
					gravityChange.nGravity = new Vector2 (0, -1f);
					gravityChange.deviceOrientation = DeviceOrientation.LandscapeLeft;
				}
				if(Input.GetKey(KeyCode.J)){
					gravityChange.nGravity = new Vector2 (-1f, 0);
					gravityChange.deviceOrientation = DeviceOrientation.PortraitUpsideDown;
				}
				if(Input.GetKey(KeyCode.K)){
					gravityChange.nGravity = new Vector2 (0, 1f);
					gravityChange.deviceOrientation = DeviceOrientation.LandscapeRight;
				}
				if(Input.GetKey(KeyCode.M)){
					gravityChange.nGravity = new Vector2 (1f, 0);
					gravityChange.deviceOrientation = DeviceOrientation.Portrait;
				}
			}

			//Mobile Phone settings
			switch (Input.deviceOrientation) {
			case DeviceOrientation.LandscapeLeft:
				gravityChange.nGravity = new Vector2 (0, -1f);
				gravityChange.deviceOrientation = DeviceOrientation.LandscapeLeft;
				break;
			case DeviceOrientation.PortraitUpsideDown:
				gravityChange.nGravity = new Vector2 (-1f, 0);
				gravityChange.deviceOrientation = DeviceOrientation.PortraitUpsideDown;
				break;
			case DeviceOrientation.LandscapeRight:
				gravityChange.nGravity = new Vector2 (0, 1f);
				gravityChange.deviceOrientation = DeviceOrientation.LandscapeRight;
				break;
			case DeviceOrientation.Portrait:
				gravityChange.nGravity = new Vector2 (1f, 0);
				gravityChange.deviceOrientation = DeviceOrientation.Portrait;
				break;
			}//end switch

			if (_previousOrientation != gravityChange.deviceOrientation) {
				_previousOrientation = gravityChange.deviceOrientation;
				onGravityChange(gravityChange);
			}

		}//end if colliding
	}//end Update
}//end class
