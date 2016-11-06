using UnityEngine;
using System.Collections;

public class ClimbPoint : MonoBehaviour {
    [SerializeField]
    private Transform _start;
    [SerializeField]
    private Transform _end;
    [SerializeField]
    private PlayerWallPosition _climbableOrientation;

    public Transform Start
    {
        get { return _start; }
    }

    public Transform End
    {
        get { return _end; }
    }

    public PlayerWallPosition ClimbableOrientation
    {
        get { return _climbableOrientation; }
    }
}
