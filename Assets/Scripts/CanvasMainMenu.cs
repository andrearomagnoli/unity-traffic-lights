using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Text that is updated.")]
    private TMP_Text scoreText;

    private void Start()
    {
        if (GameStatus.Instance.Score <= 0)
        {
            scoreText.text = "";
        }
        else
        {
            scoreText.text = $"Score: {GameStatus.Instance.Score}";
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("Game");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
