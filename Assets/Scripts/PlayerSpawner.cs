using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    [Tooltip("Player's vehicles pool (one is randomly picked).")]
    private GameObject[] vehicles;

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
        if (GameStatus.Instance != null)
        {
            GameStatus.Instance.Score = 0;
        }
        Instantiate(vehicles[Random.Range(0, vehicles.Length)], startingPosition);
    }

    #endregion

    #region Callbacks
    private void OnTriggerEnter(Collider other)
    {
        if ((playerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (GameStatus.Instance != null)
            {
                GameStatus.Instance.Score++;
            }
            Destroy(other.gameObject);
            Instantiate(vehicles[Random.Range(0, vehicles.Length)], startingPosition);
        }
    }

    #endregion
}
