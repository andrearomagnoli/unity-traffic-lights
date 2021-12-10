using UnityEngine;

public class GameStatus : MonoBehaviour
{
    #region Parameters

    public static GameStatus Instance;

    public int Score { get; private set; } = 0;

    #endregion

    #region UnityEvents

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Reset the score;
    /// </summary>
    public void ScoreReset()
    {
        Score = 0;
    }

    /// <summary>
    /// Increase the score by one unit.
    /// </summary>
    public void ScoreIncrease()
    {
        Score++;
    }

    #endregion
}
