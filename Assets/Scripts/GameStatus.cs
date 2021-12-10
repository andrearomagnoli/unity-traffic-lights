using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance;

    public int Score = 0;

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
}
