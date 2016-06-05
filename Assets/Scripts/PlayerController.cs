using UnityEngine;
using System.Collections;

//What side is the player walking on on the editor application
public enum PlayerWallPosition{
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
	//Player's movable component, to know if he's grounded
	private Movable _playerMovable;
	//Where you lookin'
	private bool _facingRight = false;
	//Movement of the Player
	private Vector2 _move = Vector2.zero;
	//Animation
	private Animator _anim;
	//Player rotation
	private PlayerWallPosition _playerWallPosition;

	//Getters and Setters
	public bool isHolding{get; set;}
	public bool isFacingRight{ get {return _facingRight;} }
	public PlayerWallPosition playerWallPosition{ get {return _playerWallPosition;} }
		
	//Initialization
	void Start () {
		_player = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator> ();
		_playerMovable = GetComponent<Movable>();
		isHolding = false;
		_playerWallPosition = PlayerWallPosition.Down;
		InputControllerEvent.onTouchInput += OnTouchInput;
		GravityControllerEvent.onGravityChange += OnGravityChange;
	}

	/// <summary>
	/// Raises the gravity change event. Change player orientation (what wall he is on) and player rotation.
	/// </summary>
	/// <param name="gravity">Gravity.</param>
	void OnGravityChange (GravityChange gravity)
	{
		_player.velocity = Vector2.zero;

		switch(gravity.deviceOrientation) {
		case DeviceOrientation.LandscapeLeft:
			_player.rotation = 0f;
			_playerWallPosition = PlayerWallPosition.Down;
			break;
		case DeviceOrientation.PortraitUpsideDown:
			_player.rotation = -90f;
			_playerWallPosition = PlayerWallPosition.Left;
			break;
		case DeviceOrientation.LandscapeRight:
			_player.rotation = 180f;
			_playerWallPosition = PlayerWallPosition.Up;
			break;
		case DeviceOrientation.Portrait:
			_player.rotation = 90f;
			_playerWallPosition = PlayerWallPosition.Right;
			break;
		}
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnTouchInput from the method list of InputController
	/// </summary>
	void OnDestroy(){
		InputControllerEvent.onTouchInput -= OnTouchInput;
		GravityControllerEvent.onGravityChange -= OnGravityChange;
	}
		
	void FixedUpdate ()
	{
		//Input for testing
		if(Application.isEditor){
			_move.x = Input.GetAxis ("Horizontal");
			_move.y = Input.GetAxis ("Vertical");
		}

		//If you're grounded and where you are determines where you move
		if (_playerMovable.IsGrounded()) {
			if (_playerWallPosition == PlayerWallPosition.Up || _playerWallPosition == PlayerWallPosition.Down) {
				Move (_move.x);
			} else {
				Move (_move.y);
			}
		} else {
			//Needs to be put back at 0
			Move (0f);
		}

		_move = Vector2.zero;
	}

	/// <summary>
	/// Changing the velocity to specified f
	/// </summary>
	/// <param name="f">F - speed</param>
	void Move(float f){
		//We don't really care about 1 or -1 which is why we abs the value
		//Just want to know if it's moving so we can set the animation
		_anim.SetFloat ("Speed", Mathf.Abs (f));

		//If your on ceiling or ground then X
		//If your on the sides then Y
		if (_playerWallPosition == PlayerWallPosition.Up || _playerWallPosition == PlayerWallPosition.Down) {
			_player.velocity = new Vector2 (f*speed, _player.velocity.y);
		} else {
			_player.velocity = new Vector2 (_player.velocity.x, f*speed);
		}

		if (_playerWallPosition == PlayerWallPosition.Down || _playerWallPosition == PlayerWallPosition.Right) {
			//This works for Down and Right wall
			if (f > 0 && !_facingRight) {
				Flip ();
			} else if (f < 0 && _facingRight) {
				Flip ();
			}
		} else if (_playerWallPosition == PlayerWallPosition.Up || _playerWallPosition == PlayerWallPosition.Left) {
			if (f < 0 && !_facingRight) {
				Flip ();
			} else if (f > 0 && _facingRight) {
				Flip ();
			}
		}
	}

	/// <summary>
	/// Flip this instance.
	/// </summary>
	void Flip(){
		//Don't flip if the player is holding onto a box
		if (!isHolding) {
			//could be optimized
			_facingRight = !_facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			//transform.localScale *= -1 but only on x
		}
	}

	/// <summary>
	/// Raises the touch input event. Allows us to know what position and what side we are on
	/// </summary>
	/// <param name="touch">Touch.</param>
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