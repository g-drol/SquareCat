using UnityEngine;
using System.Collections;

/// <summary>
/// Should be on every Movable Object including the player
/// </summary>
public class Movable : MonoBehaviour {

	//Collider 2D of the element
	private Collider2D _collider;

	//Initializing
	void Start()
	{
		_collider = GetComponent<Collider2D>();
	}

	/// <summary>
	/// Determines whether this instance is grounded.
	/// </summary>
	/// <returns><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</returns>
	public bool IsGrounded()
	{
		Vector2[] groundPoints = new Vector2[3];
		//Creating Ground ppints depending on the orientation of Gravity
		switch (GravityController.orientation)
		{
		case DeviceOrientation.LandscapeLeft:
			groundPoints[0] = new Vector2 (_collider.bounds.center.x + _collider.bounds.extents.x, _collider.bounds.center.y - _collider.bounds.extents.y);
			groundPoints[1] = new Vector2 (_collider.bounds.center.x, _collider.bounds.center.y - _collider.bounds.extents.y);
			groundPoints[2] = _collider.bounds.min;
			break;
		case DeviceOrientation.Portrait:
			groundPoints[0] = new Vector2 (_collider.bounds.center.x + _collider.bounds.extents.x, _collider.bounds.center.y - _collider.bounds.extents.y);
			groundPoints[1] = new Vector2 (_collider.bounds.center.x + _collider.bounds.extents.x, _collider.bounds.center.y);
			groundPoints[2] = _collider.bounds.max;
			break;
		case DeviceOrientation.LandscapeRight:
			groundPoints[0] = _collider.bounds.max;
			groundPoints[1] = new Vector2 (_collider.bounds.center.x, _collider.bounds.center.y + _collider.bounds.extents.y);
			groundPoints[2] = new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, _collider.bounds.center.y + _collider.bounds.extents.y);
			break;
		case DeviceOrientation.PortraitUpsideDown:
			groundPoints[0] = _collider.bounds.min;
			groundPoints[1] = new Vector2 (_collider.bounds.center.x - _collider.bounds.extents.x, _collider.bounds.center.y);
			groundPoints[2] = new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, _collider.bounds.center.y + _collider.bounds.extents.y);
			break;
		}

		//Raycast from every ground point
		//Needed when we want to know if the movable objcet is in contact with anything
		for (int i = 0; i < groundPoints.Length; i++) {
			RaycastHit2D rayHit;
			//Raycast in the direction of the gravity to see if they are touching the element closest to them
			//If a box is over a box, the box under it will return false for as long as it doesn't touch a wall
			rayHit = Physics2D.Raycast(groundPoints[i], Physics2D.gravity, 0.1f);
			if (rayHit.collider != null) {
				return true;
				//RayCast collider in Unity changed to ignore themselves
				//Edit --> Project Settings --> Physics 2D --> Queries start in Collider (Unchecked)
			}
		}
		return false;
	}
}
