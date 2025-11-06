using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquationGenerator : MonoBehaviour
{
    [Header("Values: ")]
    public string currentEquation;
    public int correctAnswer;
    public int score = 0;
    public int highScore = 0;
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty;

    [Header("Main Texts:")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI equationText;
    public TextMeshProUGUI pointText;
    public TMP_InputField inputText;

    [Header("Timer: ")]
    public float timeLimit = 30f; // seconds per question
    private float currentTime;
    private Coroutine countdownCoroutine;
    private bool isRunning = false;

    [Header("End Panel: ")]
    public GameObject endPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Button mainMenuButton;
    public Button replayButton;

    private void Start()
    {
        int savedDifficulty = PlayerPrefs.GetInt("Difficulty", 0); // default = Easy
        currentDifficulty = (Difficulty)savedDifficulty;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"High Score: {highScore}";

        StartCountdown();
        GenerateEquation();
    }

    public void GenerateEquation()
    {
        int a, b;
        string op;
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                a = Random.Range(1, 11);
                b = Random.Range(1, 11);
                op = Random.Range(0, 2) == 0 ? "+" : "-";
                if (op == "+")
                    correctAnswer = a + b;
                else
                {
                    if (b > a) { int temp = a; a = b; b = temp; }
                    correctAnswer = a - b;
                }
                equationText.text = currentEquation = $"{a} {op} {b}";
                break;

            case Difficulty.Medium:
                a = Random.Range(1, 11);
                b = Random.Range(1, 11);
                op = Random.Range(0, 2) == 0 ? "*" : "/";
                if (op == "*")
                    correctAnswer = a * b;
                else
                {
                    // Ensure integer division
                    correctAnswer = a;
                    a = a * b; // make it divisible by b
                }
                equationText.text = currentEquation = $"{a} {op} {b}";
                break;

            case Difficulty.Hard:
                // Generate a PEMDAS-style expression
                int c = Random.Range(1, 6);
                int d = Random.Range(1, 6);
                a = Random.Range(1, 6);
                b = Random.Range(1, 6);

                // Randomly choose operators
                string op1 = Random.Range(0, 2) == 0 ? "+" : "-";
                string op2 = Random.Range(0, 2) == 0 ? "*" : "/";

                // Make division integer-friendly
                if (op2 == "/")
                {
                    a = a * b; // ensures divisible
                }

                // Build expression: (a op1 b) op2 (c op1 d)
                string expression = $"({a} {op1} {b}) {op2} ({c} {op1} {d})";
                currentEquation = expression;

                // Evaluate manually
                int left = (op1 == "+") ? a + b : a - b;
                int right = (op1 == "+") ? c + d : c - d;
                if (op2 == "*")
                    correctAnswer = left * right;
                else
                    correctAnswer = left / right;

                equationText.text = currentEquation;
                break;
        }
    }

    public void CheckValue()
    {
        int playerAnswer;

        if (int.TryParse(inputText.text, out playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                GenerateEquation();
                inputText.text = "";
                score++;
                pointText.text = $"Score: {score}";
            }
        }
    }

    private void StartCountdown()
    {
        currentTime = timeLimit;
        isRunning = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerText.text = Mathf.Ceil(currentTime).ToString("0");
            yield return null;
        }

        timerText.text = "0";
        isRunning = false;
        inputText.interactable = false;
        ShowPanel();
    }

    private void ShowPanel()
    {
        endPanel.SetActive(true);

        // Check for new high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        scoreText.text = $"Score: {score}";
        highScoreText.text = $"High Score: {highScore}";

        mainMenuButton.onClick.AddListener(() => ScenesManager.instance.LoadLevel("MainMenu"));
        replayButton.onClick.AddListener(() => ScenesManager.instance.LoadLevel("SpeedCountScene"));
    }
}