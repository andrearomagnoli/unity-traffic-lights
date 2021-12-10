using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Vehicle
{
    #region

    [SerializeField]
    [Tooltip("Layer that determines what is a vehicle.")]
    protected LayerMask vehiclesMask;

    #endregion

    #region UnityEvents

    private void Start()
    {
        _canMove = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _canMove = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _canMove = true;
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

        if ((vehiclesMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            SceneManager.LoadScene("Main");
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
