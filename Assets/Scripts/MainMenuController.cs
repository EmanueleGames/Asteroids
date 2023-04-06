using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        if (!FindObjectOfType<AudioManager>().IsPlaying(AudioManager.DEEPSPACE_NOISE_SOUND))
            FindObjectOfType<AudioManager>().Play(AudioManager.DEEPSPACE_NOISE_SOUND);
    }

    // Start Game Button
    public void StartGame()
    {
        SceneManager.LoadScene(GameManager.GAMEPLAY_SCENE_TAG);
    }
    // Quit Game Button
    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }
}
