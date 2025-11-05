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
    public enum Difficulty { Easy, Medium, Hard }

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
        StartCountdown();
        GenerateEquation();
    }

    public void GenerateEquation()
    {
        int a = Random.Range(1, 20);
        int b = Random.Range(1, 20);
        string op = Random.Range(0, 2) == 0 ? "+" : "-";
        if (op == "+")
        {
            correctAnswer = a + b;
        }
        else // "-"
        {
            if (b > a) //e.g. a = 5 b = 10
            {
                int temp;
                temp = a;
                a = b;
                b = temp;
            }
            correctAnswer = a - b;
        }
        equationText.text = currentEquation = $"{a} {op} {b}";

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
        mainMenuButton.onClick.AddListener(() => ScenesManager.instance.LoadLevel("MainMenu"));
        replayButton.onClick.AddListener(() => ScenesManager.instance.LoadLevel("SpeedCountScene"));
        scoreText.text = pointText.text;
    }
}
