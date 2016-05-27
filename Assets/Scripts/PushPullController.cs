using UnityEngine;
using System.Collections;

public class PushPullController : MonoBehaviour {

	private Joint2D _jointBoxPlayer;
	private GameObject _movable;
	private Rigidbody2D _player;

	// Use this for initialization
	void Start () {
		_player = GetComponent<Rigidbody2D> ();
		InputControllerEvent.onTouchInput += OnTouchInput;
	}

	/// <summary>
	/// Call when object is destroyed
	/// - Remove OnTouchInput from the method list of InputController
	/// </summary>
	void OnDestroy(){
		InputControllerEvent.onTouchInput -= OnTouchInput;	
	}

	void Grab(Rigidbody2D movable){
		//Check if the join doesn't exist already and creates it/connects it
		if (_jointBoxPlayer == null) {
			_jointBoxPlayer = gameObject.AddComponent<FixedJoint2D>();
		}

		//Arranging Joint and Physics
		movable.isKinematic = false;
		_jointBoxPlayer.connectedBody = movable;
		_jointBoxPlayer.enabled = true;
		_player.GetComponent<PlayerController> ().isHolding = true;
	}

	void LetGo(){
		//If Joint exists and fixing values
		if (_jointBoxPlayer != null && _jointBoxPlayer.connectedBody != null) {
			_jointBoxPlayer.connectedBody.isKinematic = true;
			_jointBoxPlayer.enabled = false;
			_jointBoxPlayer.connectedBody = null;
		}
		_player.GetComponent<PlayerController> ().isHolding = false;
	}

	void Update () {
		//On first press of the h key and after
		if (Input.GetKeyDown (KeyCode.H)) {
			RaycastHit2D rayHit = Physics2D.Raycast (_player.transform.position, GetComponent<PlayerController> ().isFacingRight ? Vector2.right : Vector2.left, 0.75f);
			if (rayHit.rigidbody != null && rayHit.rigidbody.gameObject.tag == "Movable") {
				Grab (rayHit.rigidbody);
			}
		}

		if (Input.GetKeyUp(KeyCode.H)){
			LetGo ();
		}
	}


	void OnTouchInput(TouchInput touch){
		if (touch.inType == TouchInputType.Tap){
			Collider2D touchPoint = Physics2D.OverlapPoint (touch.position);
			if(touchPoint != null && touchPoint.gameObject.tag == "Movable"){
				if (_player.GetComponent<PlayerController> ().isHolding) {
					LetGo ();
				} else if (touchPoint.IsTouching(GetComponent<BoxCollider2D>())){
					Grab (touchPoint.gameObject.GetComponent<Rigidbody2D> ());
				}
			}
		}
	
	}
}
