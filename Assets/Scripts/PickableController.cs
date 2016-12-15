using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableController : MonoBehaviour {

	[SerializeField]
	private Image[] imageList;

	private static PickableController pickableC;
	public static PickableController instance{
		get { return pickableC; }
	}
		
	void Awake(){
		pickableC = this;
	}

	public void PickUp(Pickable pickable){
		imageList [pickable.getID()].gameObject.SetActive (true);
	}

}
