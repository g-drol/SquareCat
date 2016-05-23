using UnityEngine;
using System.Collections;

public class PushPullController : MonoBehaviour {

	//private Joint2D jointBoxPlayer;

	private Rigidbody2D _movableBox;

	// Use this for initialization
	void Start () {
		_movableBox = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnControllerColliderHit(ControllerColliderHit hit){

		if (Input.GetKey (KeyCode.H) && hit.gameObject.tag == "Player") {
			_movableBox.velocity = hit.gameObject.GetComponent<Rigidbody2D> ().velocity;
		}
	}

	/*private void OnCollisionEnter(Collision col) {

		if(col.gameObject.tag == "Player"){
			gameObject.AddComponent<FixedJoint2D>();
		}

		jointBoxPlayer = GetComponent<FixedJoint2D>() ;
	}

	private void OnCollisionStay () {

		GameObject player = GameObject.FindWithTag("Player");

		if (Input.GetKey(KeyCode.H)) {
			jointBoxPlayer.connectedBody = player.gameObject.GetComponent<Rigidbody2D>();
		}
	}

	private void OnCollisionExit(Collision col) {
		Destroy (jointBoxPlayer);
	}*/

}
