using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public GameObject pauseMenuUIReference;
    public GameObject gameoverMenuUIReference;
    public GameObject scoreReference;
    public GameObject finalscoreReference;

    void OnEnable()
    {
        // Event subscription for score update
        GameManager.ScoreUpdatedInfo += UpdateScoreUI;
    }
    void OnDestroy()
    {
        // Event unsubscription for score update
        GameManager.ScoreUpdatedInfo -= UpdateScoreUI;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.instance.IsPlayerAlive())
            {
                if (GameManager.instance.GamePaused())
                    Resume();
                else
                    Pause();
            }
        }

        if (!GameManager.instance.IsPlayerAlive())
        {
            scoreReference.SetActive(false);
            gameoverMenuUIReference.SetActive(true);
        }
    }

    void Pause()
    {
        pauseMenuUIReference.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.TogglePause(true);
    }
    public void Resume()
    {
        GameManager.instance.TogglePause(false);
        Time.timeScale = 1f;
        pauseMenuUIReference.SetActive(false);
    }

    public void MainMenuScreen()
    {
        GameManager.instance.TogglePause(false);
        GameManager.instance.ResetPoints();
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameManager.TITLE_SCENE_TAG);
    }

    public void PlayAgain()
    {
        GameManager.instance.ResetPoints();
        scoreReference.SetActive(true);
        gameoverMenuUIReference.SetActive(false);
        SceneManager.LoadScene(GameManager.GAMEPLAY_SCENE_TAG);
    }

    public void UpdateScoreUI()
    {
        int points = GameManager.instance.GetPoints();
        string score_string;

        if (points == 0)
            score_string = "Score: 0";
        else
            score_string = "Score: " + GameManager.instance.GetPoints();

        scoreReference.GetComponent<TextMeshProUGUI>().text = score_string;
        finalscoreReference.GetComponent<TextMeshProUGUI>().text = score_string;
    }
}
