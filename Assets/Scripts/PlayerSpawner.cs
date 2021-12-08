using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    [Tooltip("Player's vehicle.")]
    private GameObject vehicle;

    [SerializeField]
    [Tooltip("Starting position of the player.")]
    private Transform startingPosition;


    [SerializeField]
    [Tooltip("Layer that determines what is a player.")]
    protected LayerMask playerMask;

    #endregion

    #region UnityEvents

    private void Awake()
    {
        Instantiate(vehicle, startingPosition);
    }

    #endregion

    #region Callbacks
    private void OnTriggerEnter(Collider other)
    {
        if ((playerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Destroy(other.gameObject);
            Instantiate(vehicle, startingPosition);
        }
    }

    #endregion
}
