using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
