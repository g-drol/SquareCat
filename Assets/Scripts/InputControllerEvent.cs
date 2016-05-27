using UnityEngine;
using System.Collections;

public enum TouchInputType {
	Tap,
	Hold
}
	
public struct TouchInput {
	public Vector2 position;
	public TouchInputType inType;
}
	
public class InputControllerEvent : MonoBehaviour {

	//Delegate and event
	public delegate void TouchInputDelegate(TouchInput touchInput);
	public static event TouchInputDelegate onTouchInput = delegate {};

	//Constants
	private const int kMaxNumTouches = 2;
	private const float kMaxDistanceTap = 1f;

	//Vector array of start positions
	private Vector2 [] _startPosition = new Vector2[kMaxNumTouches];
	private bool[] _isTap = new bool[kMaxNumTouches];

	void Update(){

		Touch [] touchList;
		TouchInput touchInput = new TouchInput ();

		if (Input.touchCount != 0) {

			touchList = Input.touches;

			for (int i = 0; i < touchList.Length && i < kMaxNumTouches; i++) {

				Vector2 touchPosition = Camera.main.ScreenToWorldPoint (touchList[i].position);

				switch (touchList [i].phase) {

				case TouchPhase.Began:
					_startPosition [i] = touchPosition;
					_isTap [i] = true;
					break;

				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if (Vector2.Distance (_startPosition[i], touchPosition) > kMaxDistanceTap) {
						_isTap [i] = false;					
					}
					touchInput.inType = TouchInputType.Hold;
					touchInput.position = touchPosition;
					onTouchInput (touchInput);
					break;

				case TouchPhase.Ended:
					if (_isTap [i]) {
						touchInput.inType = TouchInputType.Tap;
						touchInput.position = touchPosition;
						onTouchInput (touchInput);
					}
					break;

				}
			}
		}
	
	
	}
}
