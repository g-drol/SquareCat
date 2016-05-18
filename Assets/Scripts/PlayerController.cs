using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//Working in sprites
	private Rigidbody2D _characterController;

	// Use this for initialization
	void Start () {
		
		_characterController = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		_characterController.velocity = new Vector2 (horizontal*10, _characterController.velocity.y);
	}
}