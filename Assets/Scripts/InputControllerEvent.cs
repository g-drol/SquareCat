using UnityEngine;
using System.Collections;

public enum TouchInputType {
	Tap,
	Hold,
	Swipe
}
	
public struct TouchInput {
	public Vector2 position;
	public TouchInputType inType;
	public Vector2 swipeDistance;
}
	
public class InputControllerEvent : MonoBehaviour {

	//Delegate and event
	public delegate void TouchInputDelegate(TouchInput touchInput);
	public static event TouchInputDelegate onTouchInput = delegate {};

	//Constants
	private const int kMaxNumTouches = 2;
	private const float kMaxDistanceTap = 1f;
	private const float kTimeforHold = 0.1f; //arbitrary

	private bool _isHolding = false;

	//Vector array of start positions
	public Vector2 _startPosition = new Vector2();

	//private Vector2 _startPosition = new Vector2[kMaxNumTouches];
	//private bool[] _isTap = new bool[kMaxNumTouches];

	void Update(){

		//Touch [] touchList;
		Touch touch;
		TouchInput touchInput = new TouchInput ();

		mousePosition (touchInput);

		if (Input.touchCount != 0) {

			_isHolding = false;

			for (int i = 0; i < Input.touchCount; i++) {

				touch = Input.GetTouch(i);
				Vector2 touchPosition = Camera.main.ScreenToWorldPoint (touch.position);

				switch (touch.phase) {

				case TouchPhase.Began:
					_startPosition = touchPosition;
					break;

				case TouchPhase.Moved:
					touchInput.swipeDistance = touch.deltaPosition;
					touchInput.inType = TouchInputType.Swipe;
					touchInput.position = touchPosition;
					onTouchInput (touchInput);
					break;

				case TouchPhase.Stationary:
					_isHolding = true;
					//If Ended gets ignored
					touchInput.inType = TouchInputType.Hold;
					touchInput.position = touchPosition;
					onTouchInput (touchInput);
					break;

				case TouchPhase.Ended:
					if (touch.deltaTime > kTimeforHold || _isHolding)
						touchInput.inType = TouchInputType.Hold;
					else
						touchInput.inType = TouchInputType.Tap;
					touchInput.position = touchPosition;
					onTouchInput (touchInput);
					break;
				}
			}
		}
	}

	void mousePosition(TouchInput touchInput){

		if (Input.GetMouseButtonDown (0)) {
			_startPosition = (Vector2)Input.mousePosition;
		}

		if (Input.GetMouseButton (0)) {
			touchInput.inType = TouchInputType.Hold;
			Debug.Log (touchInput.inType);
			touchInput.position = (Vector2)Input.mousePosition;
			Debug.Log (touchInput.position);
			onTouchInput (touchInput);
		}
	
		if(Input.GetMouseButtonUp(0)){
			touchInput.swipeDistance = _startPosition - (Vector2)Input.mousePosition;
			if (touchInput.swipeDistance.Equals (Vector2.zero))
				touchInput.inType = TouchInputType.Tap;
			else
				touchInput.inType = TouchInputType.Swipe;
			touchInput.position = (Vector2)Input.mousePosition;
			onTouchInput (touchInput);
		}
	}
}
