using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private bool _x = false;

    [SerializeField]
    private bool _y = false;

    [SerializeField]
    private float _speed = 1f;

    private void OnEnable()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        Vector3 position = transform.position;

        if (_x)
        {
            position.x = _target.transform.position.x;
        }

        if (_y)
        {
            position.y = _target.transform.position.y;
        }

        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }
}
