using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<StarterAssetsInputs>().cursorLocked = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;
    }

    public void OnEast(InputValue value)
    {
        if (!value.isPressed)
            return;

        Quit();
    }

    public void OnSouth(InputValue value)
    {
        if (!value.isPressed)
            return;

        PlayAgain();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
