using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    public static bool isGameover = false;
    public Canvas gameoverCanvas;

    private void Start()
    {
        gameoverCanvas.enabled = false;
    }

    private void Update()
    {
        if (isGameover)
        {
            gameoverCanvas.enabled = true;
        }
        else
        {
            gameoverCanvas.enabled = false;
        }
    }

    public void restartGame()
    {
        if(isGameover)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
