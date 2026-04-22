using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Estado del juego")]
    public int score = 0;
    public int lives = 3;
    public int currentLevel = 1;
    public int scoreToFinishLevel = 5;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text levelText;
    public TMP_Text messageText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
        ClearMessage();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RebindUI();

        if (scene.name == "Level1")
        {
            currentLevel = 1;
            scoreToFinishLevel = 5;
        }
        else if (scene.name == "Level2")
        {
            currentLevel = 2;
            scoreToFinishLevel = 8;
        }

        UpdateUI();
        ClearMessage();
    }

    private void RebindUI()
    {
        GameObject scoreObj = GameObject.Find("ScoreText");
        GameObject livesObj = GameObject.Find("LivesText");
        GameObject levelObj = GameObject.Find("LevelText");
        GameObject messageObj = GameObject.Find("MessageText");

        scoreText = scoreObj ? scoreObj.GetComponent<TMP_Text>() : null;
        livesText = livesObj ? livesObj.GetComponent<TMP_Text>() : null;
        levelText = levelObj ? levelObj.GetComponent<TMP_Text>() : null;
        messageText = messageObj ? messageObj.GetComponent<TMP_Text>() : null;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        if (score >= scoreToFinishLevel)
        {
            ShowMessage("Puerta desbloqueada");
        }
    }

    public bool CanFinishLevel()
    {
        return score >= scoreToFinishLevel;
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            score = 0;
            SceneManager.LoadScene("Level2");
        }
        else
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void RestartGame()
    {
        score = 0;
        lives = 3;
        currentLevel = 1;
        scoreToFinishLevel = 5;
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMessage(string text)
    {
        if (messageText != null)
        {
            messageText.text = text;
        }
    }

    public void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (livesText != null) livesText.text = "Vidas: " + lives;
        if (levelText != null) levelText.text = "Nivel: " + currentLevel;
    }
}
