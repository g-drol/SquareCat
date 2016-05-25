using UnityEngine;
using System.Collections;

public struct GravityChange {
	public DeviceOrientation deviceOrientation;
}

public class GChangeController : MonoBehaviour {
	
	/*//Delegate and event
	public delegate void GravityChangeDelegate(GravityChange gravity);
	public static event GravityChangeDelegate onGravityChange = delegate {};

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.isEditor) {
			if(Input.GetKey(KeyCode.I))
				Physics2D.gravity = new Vector2 (0, -1f);
			if(Input.GetKey(KeyCode.J))
				Physics2D.gravity = new Vector2 (-1f, 0);
			if(Input.GetKey(KeyCode.K))
				Physics2D.gravity = new Vector2 (0, 1f);
			if(Input.GetKey(KeyCode.M))
				Physics2D.gravity = new Vector2 (1f, 0);
		}

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
	}*/
}
