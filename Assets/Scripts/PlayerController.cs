using UnityEngine;
using System.Collections;

public enum PlayerOrientation{
	Left,
	Right,
	Up,
	Down
}

public class PlayerController : MonoBehaviour {

	//Row row row your boat, gently down the stream
	public float speed = 3f;
	//Working in sprites
	private Rigidbody2D _player;
	//Player's movable component
	private Movable _playerMovable;
	//Where you lookin'
	private bool _facingRight = false;
	//Movement of the Player
	private Vector2 _move = Vector2.zero;
	//Animation
	private Animator _anim;
	//Player rotation
	private PlayerOrientation _playerOrientation;
	//Holding the boxes
	public bool isHolding{get; set;}
	public bool isFacingRight{ get {return _facingRight;} }
	public PlayerOrientation playerOrientation{ get {return _playerOrientation;} }
		
	// Use this for initialization
	void Start () {
		_player = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator> ();
		_playerMovable = GetComponent<Movable>();
		isHolding = false;
		_playerOrientation = PlayerOrientation.Down;
		InputControllerEvent.onTouchInput += OnTouchInput;
		GravityControllerEvent.onGravityChange += OnGravityChange;
	}

	void OnGravityChange (GravityChange gravity)
	{
		_player.velocity = Vector2.zero;

		switch(gravity.deviceOrientation) {
		case DeviceOrientation.LandscapeLeft:
			_player.rotation = 0f;
			_playerOrientation = PlayerOrientation.Down;
			break;
		case DeviceOrientation.PortraitUpsideDown:
			_player.rotation = -90f;
			_playerOrientation = PlayerOrientation.Left;
			break;
		case DeviceOrientation.LandscapeRight:
			_player.rotation = 180f;
			_playerOrientation = PlayerOrientation.Up;
			break;
		case DeviceOrientation.Portrait:
			_player.rotation = 90f;
			_playerOrientation = PlayerOrientation.Right;
			break;
		}
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
			_move.x = Input.GetAxis ("Horizontal");
			_move.y = Input.GetAxis ("Vertical");
		}

		if (_playerMovable.IsGrounded()) {
			if (_playerOrientation == PlayerOrientation.Up || _playerOrientation == PlayerOrientation.Down) {
				Move (_move.x);
			} else {
				Move (_move.y);
			}
		} else {
			Move (0f);
		}
		

		_move = Vector2.zero;
	}

	void Move(float f){
		//We don't really care about 1 or -1 which is why we abs the value
		//Just want to know if it's moving
		_anim.SetFloat ("Speed", Mathf.Abs (f));

		if (_playerOrientation == PlayerOrientation.Up || _playerOrientation == PlayerOrientation.Down) {
			_player.velocity = new Vector2 (f*speed, _player.velocity.y);
		} else {
			_player.velocity = new Vector2 (_player.velocity.x, f*speed);
		}
			
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
				_move.x = 1;
			} else {
				_move.x = -1;
			}
			if (touch.position.y > transform.position.y) {
				_move.y = 1;
			} else {
				_move.y = -1;
			}
		}
	}
}