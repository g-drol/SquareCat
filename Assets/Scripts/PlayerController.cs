using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//Row row row your boat, gently down the stream
	public float speed = 3f;
	//Working in sprites
	private Rigidbody2D _player;
	//Where you lookin'
	private bool _facingRight = false;
	//Movement of the Player
	private float _move = 0f;
	//Animation
	private Animator _anim;
	//Holding the boxes
	public bool isHolding{get; set;}
	public bool isFacingRight{ get {return _facingRight;} }
		
	// Use this for initialization
	void Start () {
		_player = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator> ();
		isHolding = false;
		InputControllerEvent.onTouchInput += OnTouchInput;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnTouchInput from the method list of InputController
	/// </summary>
	void OnDestroy(){
		InputControllerEvent.onTouchInput -= OnTouchInput;	
	}

	/**
	 * Fixed Update has regular updates on a fixed interval
	 * You could use Time.deltaTime but not obligated to
	**/
	void FixedUpdate ()
	{
		//Input for testing
		if(Application.isEditor){
			_move = Input.GetAxis ("Horizontal");
		}
			
		Move (_move);
		_move = 0f;
	}

	void Move(float f){
		//We don't really care about 1 or -1 which is why we abs the value
		//Just want to know if it's moving
		_anim.SetFloat ("Speed", Mathf.Abs (f));
		//Moving the character
		_player.velocity = new Vector2 (f*speed, _player.velocity.y);

		if (f > 0 && !_facingRight) {
			Flip ();
		} else if (f < 0 && _facingRight) {
			Flip ();
		}
	}

	void Flip(){

		if (!isHolding) {
			//could be optimized
			_facingRight = !_facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			//transform.localScale *= -1 but only on x
		}
	}

	void OnTouchInput(TouchInput touch){
		if (touch.inType == TouchInputType.Hold) {
			if (touch.position.x > transform.position.x) {
				_move = 1;
			} else {
				_move = -1;
			}
		}
	}
}