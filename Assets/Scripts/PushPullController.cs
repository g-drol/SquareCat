using UnityEngine;
using System.Collections;

public class PushPullController : MonoBehaviour {

	//Joint between the box and the player
	private Joint2D _jointBoxPlayer;
	//Rigidbody of the player
	private Rigidbody2D _player;
	//Player Controller script (inside the player)
	private PlayerController _playerController;

	// Use this for initialization
	void Start () {
		_player = GetComponent<Rigidbody2D> ();
		_playerController = GetComponent<PlayerController> ();
		InputControllerEvent.onTouchInput += OnTouchInput;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnTouchInput from the method list of InputController
	/// </summary>
	void OnDestroy(){
		InputControllerEvent.onTouchInput -= OnTouchInput;	
	}

	/// <summary>
	/// Grab the specified movable.
	/// </summary>
	/// <param name="movable">Movable.</param>
	void Grab(Rigidbody2D movable){
		
		//Check if the join doesn't exist already and creates it/connects it
		if (_jointBoxPlayer == null) {
			_jointBoxPlayer = gameObject.AddComponent<FixedJoint2D>();
		}

		//Freeze rotation of the movable
		movable.constraints = RigidbodyConstraints2D.FreezeRotation;
		//Connect the movable
		_jointBoxPlayer.connectedBody = movable;
		//Enable the joint
		//Reused multiple times so no use destroying it
		_jointBoxPlayer.enabled = true;
		_playerController.isHolding = true;
	}

	/// <summary>
	/// Lets go the Grab(movable)
	/// </summary>
	void LetGo(){
		//If Joint exists and connected to something
		if (_jointBoxPlayer != null && _jointBoxPlayer.connectedBody != null) {
			//Disable joint
			_jointBoxPlayer.enabled = false;

			//Fix the Position depending what wall the player is on
			if (_playerController.playerWallPosition == PlayerWallPosition.Up || _playerController.playerWallPosition == PlayerWallPosition.Down) {
				_jointBoxPlayer.connectedBody.constraints |= RigidbodyConstraints2D.FreezePositionX;
			} else if (_playerController.playerWallPosition == PlayerWallPosition.Right || _playerController.playerWallPosition == PlayerWallPosition.Left) {
				_jointBoxPlayer.connectedBody.constraints |= RigidbodyConstraints2D.FreezePositionY;
			}

			//Empty the joint
			_jointBoxPlayer.connectedBody = null;
		}
		_playerController.isHolding = false;
	}

	void Update () {

		RaycastHit2D rayHit;

		//On first press of the h key
		if (Input.GetKeyDown (KeyCode.H)) {

			Vector2 rayHitDirection = Vector2.zero;

			//Depending on the orientation of the character the raycast should be in a different directio
			switch (_playerController.playerWallPosition) {
			case PlayerWallPosition.Down:
				if (_playerController.isFacingRight) {
					rayHitDirection = Vector2.right;
				} else {
					rayHitDirection = Vector2.left;
				}
				break;
			case PlayerWallPosition.Up:
				if (_playerController.isFacingRight) {
					rayHitDirection = Vector2.left;
				} else {
					rayHitDirection = Vector2.right;
				}
				break;
			case PlayerWallPosition.Left:
				if (_playerController.isFacingRight) {
					rayHitDirection = Vector2.down;
				} else {
					rayHitDirection = Vector2.up;
				}
				break;
			case PlayerWallPosition.Right:
				if (_playerController.isFacingRight) {
					rayHitDirection = Vector2.up;
				} else {
					rayHitDirection = Vector2.down;
				}
				break;
			}

			//RayCast and grad if if you're touching a movable
			rayHit = Physics2D.Raycast (_player.position, rayHitDirection, 0.75f);
			if (rayHit.rigidbody != null && rayHit.rigidbody.gameObject.tag == "Movable") {
				Grab (rayHit.rigidbody);
			}
		}

		//Let go on key up
		if (Input.GetKeyUp(KeyCode.H)){
			LetGo();
		}
	}

	/// <summary>
	/// Raises the touch input event.
	/// Tap to Grab and to let go
	/// </summary>
	/// <param name="touch">Touch.</param>
	void OnTouchInput(TouchInput touch){
		//If you tap and the tap overlaps the box and you aren't already holding anything then grab
		//If you are holding something let go
		if (touch.inType == TouchInputType.Tap){
			Collider2D touchPoint = Physics2D.OverlapPoint (touch.position);
			if(touchPoint != null && touchPoint.gameObject.tag == "Movable"){
				if (_playerController.isHolding) {
					LetGo ();
				} else if (touchPoint.IsTouching(GetComponent<BoxCollider2D>())){
					Grab (touchPoint.gameObject.GetComponent<Rigidbody2D> ());
				}
			}
		}
	
	}
}
