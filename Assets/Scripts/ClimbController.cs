using UnityEngine;
using System.Collections;

public class ClimbController : MonoBehaviour {
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _climbSpeed = 10f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
        _rigidbody = _player.GetComponent<Rigidbody2D>();
        _animator = _player.GetComponent<Animator>();
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ClimbPoint climbPoint = collider.GetComponent<ClimbPoint>();

        if (climbPoint != null && _playerController.isJumping && _playerController.playerWallPosition == climbPoint.ClimbableOrientation)
        {
            StartCoroutine(DoClimb(climbPoint));
        }
    }

    private IEnumerator DoClimb(ClimbPoint climbPoint)
    {
        Debug.Log("Climb!");

        _rigidbody.velocity = Vector2.zero;

        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.simulated = false;

        while (Vector2.Distance(climbPoint.Start.position, _player.transform.position) > 0.1f)
        {
            _player.transform.position = Vector2.Lerp(_player.transform.position, climbPoint.Start.position, _climbSpeed * Time.deltaTime);

            yield return 0;
        }

        _animator.SetTrigger("Climb");

        while (Vector2.Distance(climbPoint.End.position, _player.transform.position) > 0.1f)
        {
            _player.transform.position = Vector2.Lerp(_player.transform.position, climbPoint.End.position, _climbSpeed * Time.deltaTime);

            yield return 0;
        }

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.simulated = true;
    }
}
