using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//Row row row your boat, gently down the stream
	public float speed = 3f;
	//Working in sprites
	private Rigidbody2D _character;
	//Where you lookin'
	private bool _facingRight = false;
	//Animation
	private Animator _anim;

	// Use this for initialization
	void Start () {
		_character = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator> ();
	}

	/**
	 * Fixed Update has regular updates on a fixed interval
	 * You could use Time.deltaTime but not obligated to
	**/
	void FixedUpdate ()
	{
		float move = 0f;

		/* Touch screen controls
		if (Input.touchCount > 0) {

			Touch finger = Input.GetTouch (0);

			//Screen and world difference be careful of this
			//Input = Screen, Character = world
			Vector3 fingerPosition = Camera.main.ScreenToWorldPoint (finger.position);
			if (fingerPosition.x > _character.transform.position.x) {
				move = 1;
			} else {
				move = -1;
			}
		}
		*/

		//Input for testing
		move = Input.GetAxis ("Horizontal");
			
		//We don't really care about 1 or -1 which is why we abs the value
		//Just want to know if it's moving
		_anim.SetFloat ("Speed", Mathf.Abs (move));
		_character.velocity = new Vector2 (move*speed, _character.velocity.y);

		if (move > 0 && !_facingRight) {
			Flip ();
		} else if (move < 0 && _facingRight) {
			Flip ();
		}
	}

	void Flip(){
		//could be optimized
		_facingRight = !_facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//transform.localScale *= -1 but only on x
	}
}