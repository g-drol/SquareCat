using UnityEngine;
using System.Collections;

public class PushPullController : MonoBehaviour {

	private Joint2D jointBoxPlayer;
	private GameObject _movableBox;

	// Use this for initialization
	void Start () {
		_movableBox = GetComponent<GameObject> ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionEnter2D(Collision2D col) {

		Debug.Log ("Enters collision");

		if(col.gameObject.tag == "Player"){
			Debug.Log ("Enters collision");
			_movableBox.AddComponent<FixedJoint2D>();
			jointBoxPlayer = GetComponent<FixedJoint2D>() ;
		}
	}

	private void OnCollisionStay2D(Collision2D col) {

		if(col.gameObject.tag == "Player" && Input.GetKey(KeyCode.H)){
			Debug.Log ("Stays collision");
			jointBoxPlayer.connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
		}
	}

	private void OnCollisionExit2D(Collision2D col) {
		Debug.Log ("Leaves collision");
		Destroy (jointBoxPlayer);
	}

}
