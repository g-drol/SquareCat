using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	//private Rigidbody2D _rigidBody;
	// Use this for initialization
	void Start () {
		
		//_rigidBody = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {

		switch (Input.deviceOrientation) {

		case DeviceOrientation.LandscapeLeft:
			Physics2D.gravity = new Vector2 (0, -1f);
			break;
		case DeviceOrientation.PortraitUpsideDown:
			Physics2D.gravity = new Vector2 (-1f, 0);
			break;
		case DeviceOrientation.LandscapeRight:
			Physics2D.gravity = new Vector2 (0, 1f);
			break;
		case DeviceOrientation.Portrait:
			Physics2D.gravity = new Vector2 (1f, 0);
			break;

		}
	}
}
