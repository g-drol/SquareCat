using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickable : MonoBehaviour {

	[SerializeField]
	private int ID;

	private void OnTriggerEnter2D(Collider2D other){
		PickableController.instance.PickUp (this);
		Destroy (gameObject);
	}

	public int getID(){
		return ID;
	}
}
