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

	//Editable values
    [SerializeField]
    private int _maxNumTouches = 2;
    [SerializeField]
    private float _maxDistanceTap = 1f;
    [SerializeField]
    private float _timeforHold = 0.7f; //arbitrary
    [SerializeField]
    private float _newSwipeThreshold = 0.1f;

	private float _holdCounter = 0f;
	private Vector2 _startPosition = new Vector2();
    private Vector2 _lastFramePosition;

	void Update(){

		//Touch [] touchList;
		Touch touch;
		TouchInput touchInput = new TouchInput ();

		mousePosition (touchInput);

		if (Input.touchCount != 0) {

			for (int i = 0; i < Input.touchCount; i++) {

				touch = Input.GetTouch(i);
				Vector2 touchPosition = touch.position;

				switch (touch.phase) {

				case TouchPhase.Began:
					_startPosition = touchPosition;
					break;

				case TouchPhase.Moved:
					touchInput.swipeDistance = touchPosition - _startPosition;

					if (touchInput.swipeDistance.magnitude < _maxDistanceTap) {
						if (_holdCounter > _timeforHold) {
							touchInput.inType = TouchInputType.Hold;
						} else {
							touchInput.inType = TouchInputType.Tap;
						}
					} else {
						touchInput.inType = TouchInputType.Swipe;
					}
					break;

				case TouchPhase.Stationary:
					_holdCounter += Time.deltaTime;
					if (_holdCounter > _timeforHold) {
						touchInput.inType = TouchInputType.Hold;
					} else {
						touchInput.inType = TouchInputType.Tap;
					}
					break;
				
				case TouchPhase.Ended:
					_holdCounter = 0f;
					break;
				}

				touchInput.position = touchPosition;
				onTouchInput (touchInput);
			}
		}
	}

	void mousePosition(TouchInput touchInput){

		touchInput.position = Input.mousePosition;

		if (Input.GetMouseButtonDown (0)) {
			_startPosition = (Vector2)Input.mousePosition;
		}

		if (Input.GetMouseButton (0)) {
			_holdCounter += Time.deltaTime;

			//Need this because Hold needs to be active before movement is over
			if (_holdCounter > _timeforHold) {
				touchInput.inType = TouchInputType.Hold;
                if (Vector2.Distance(touchInput.position, _lastFramePosition) < _newSwipeThreshold)
                {
                    _startPosition = (Vector2)Input.mousePosition;
                }
				onTouchInput (touchInput);
			}
		}
	
		if(Input.GetMouseButtonUp(0)){
            touchInput.swipeDistance = (Vector2)Input.mousePosition - _startPosition;

			if (touchInput.swipeDistance.magnitude < _maxDistanceTap) {
				if (_holdCounter > _timeforHold) {
					touchInput.inType = TouchInputType.Hold;
				} else {
					touchInput.inType = TouchInputType.Tap;
				}
			} else {
				touchInput.inType = TouchInputType.Swipe;
				Debug.Log (touchInput.inType);
			}
			onTouchInput (touchInput);
		}

        _lastFramePosition = (Vector2)Input.mousePosition;
	}
}
