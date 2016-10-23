using UnityEngine;
using System.Collections;

/// <summary>
/// Should be on every Movable Object including the player
/// </summary>
public class Movable : MonoBehaviour {

	//Collider 2D of the element
	private Collider2D _collider;
    private Rigidbody2D _rigidbody;

	//Initializing
	void Start()
	{
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
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
                if (_rigidbody.velocity.y > 0f)
                {
                    return false;
                }
                groundPoints[0] = new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x, _collider.bounds.center.y - _collider.bounds.extents.y);
                groundPoints[1] = new Vector2(_collider.bounds.center.x, _collider.bounds.center.y - _collider.bounds.extents.y);
                groundPoints[2] = _collider.bounds.min;
                break;
            case DeviceOrientation.Portrait:
                if (_rigidbody.velocity.x < 0f)
                {
                    return false;
                }
                groundPoints[0] = new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x, _collider.bounds.center.y - _collider.bounds.extents.y);
                groundPoints[1] = new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x, _collider.bounds.center.y);
                groundPoints[2] = _collider.bounds.max;
                break;
            case DeviceOrientation.LandscapeRight:
                if (_rigidbody.velocity.y < 0f)
                {
                    return false;
                }
                groundPoints[0] = _collider.bounds.max;
                groundPoints[1] = new Vector2(_collider.bounds.center.x, _collider.bounds.center.y + _collider.bounds.extents.y);
                groundPoints[2] = new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, _collider.bounds.center.y + _collider.bounds.extents.y);
                break;
            case DeviceOrientation.PortraitUpsideDown:
                if (_rigidbody.velocity.x > 0f)
                {
                    return false;
                }
                groundPoints[0] = _collider.bounds.min;
                groundPoints[1] = new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, _collider.bounds.center.y);
                groundPoints[2] = new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, _collider.bounds.center.y + _collider.bounds.extents.y);
                break;
        }

        //Raycast from every ground point
        //Needed when we want to know if the movable objcet is in contact with anything
        for (int i = 0; i < groundPoints.Length; i++)
        {
            Collider2D[] colliders;
            //Raycast in the direction of the gravity to see if they are touching the element closest to them
            //If a box is over a box, the box under it will return false for as long as it doesn't touch a wall
            colliders = Physics2D.OverlapCircleAll(groundPoints[i], 0.02f);

            for (int j = 0; j < colliders.Length; j++)
            {
                if (colliders[j] != null && colliders[j].gameObject != gameObject)
                {
                    return true;
                    //RayCast collider in Unity changed to ignore themselves
                    //Edit --> Project Settings --> Physics 2D --> Queries start in Collider (Unchecked)
                }
            }
        }
		return false;
	}
}
