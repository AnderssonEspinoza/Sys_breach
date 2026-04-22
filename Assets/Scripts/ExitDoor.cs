using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance.CanFinishLevel())
        {
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            int remaining = GameManager.Instance.scoreToFinishLevel - GameManager.Instance.score;
            GameManager.Instance.ShowMessage("Te faltan " + remaining + " puntos");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ClearMessage();
        }
    }
}
