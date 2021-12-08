using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Vehicle : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    [Tooltip("Speed of the vehicle.")]
    protected float speed = 100f;

    [SerializeField]
    [Tooltip("Timer after which, if still not grounded, will be destroyed.")]
    protected float groundedTimer = 1f;

    [SerializeField]
    [Tooltip("Layer that determines what is ground.")]
    protected LayerMask groundMask;

    protected Rigidbody _rigidbody;
    protected bool _canMove;

    protected float _destroyTimer;
    protected bool _isDestroyTimerRunning;

    #endregion

    #region UnityEvents

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _destroyTimer = groundedTimer;
        _isDestroyTimerRunning = true;
    }

    private void Start()
    {
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (_isDestroyTimerRunning)
        {
            _destroyTimer -= Time.fixedDeltaTime;
        }

        if (_destroyTimer <= 0f)
        {
            Destroy(gameObject);
        }

        if (_canMove)
        {
            _rigidbody.AddForce(transform.forward * (speed * Time.fixedDeltaTime), ForceMode.Acceleration);
        }
    }

    #endregion

    #region Callbacks

    protected void OnCollisionEnter(Collision collision)
    {
        if ((groundMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            _isDestroyTimerRunning = false;
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if ((groundMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            _isDestroyTimerRunning = true;
            _destroyTimer = groundedTimer;
        }
    }

    #endregion
}
