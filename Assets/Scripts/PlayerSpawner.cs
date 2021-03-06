using TMPro;
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
    [Tooltip("Text that display the player's score.")]
    private TMP_Text textScore;

    [SerializeField]
    [Tooltip("Layer that determines what is a player.")]
    private LayerMask playerMask;

    #endregion

    #region UnityEvents

    private void Awake()
    {
        GameStatus.Instance.ScoreReset();
        Instantiate(vehicles[Random.Range(0, vehicles.Length)], startingPosition);
    }

    #endregion

    #region Callbacks
    private void OnTriggerEnter(Collider other)
    {
        if ((playerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            GameStatus.Instance.ScoreIncrease();
            textScore.text = $"Score:\n{GameStatus.Instance.Score}";

            Destroy(other.gameObject);
            Instantiate(vehicles[Random.Range(0, vehicles.Length)], startingPosition);
        }
    }

    #endregion
}
