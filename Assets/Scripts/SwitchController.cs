using UnityEngine;
using System.Collections;

public enum SwitchAction{
	OpenDoor,
	UnlockBlock,
	UnlockSwitch
}

public class SwitchController : MonoBehaviour {

	public bool isSwitchTimed;
	public SwitchAction switchAction;

	private GameObject _door;

	// Use this for initialization
	void Start () {
		_door = GameObject.FindGameObjectWithTag ("Door");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("trigger enter");
		switch (switchAction) {
		case SwitchAction.OpenDoor:
			_door.GetComponent<Animator> ().SetBool ("isDoorOpen", true);
			break;
		case SwitchAction.UnlockBlock:
			break;
		case SwitchAction.UnlockSwitch:
			break;
		}
	
	}
}
