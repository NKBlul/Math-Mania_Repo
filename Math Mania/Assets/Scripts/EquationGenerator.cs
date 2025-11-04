using System.Collections;
using TMPro;
using UnityEngine;

public class EquationGenerator : MonoBehaviour
{
    public string currentEquation;
    public int correctAnswer;
    public int points = 0;
    public enum Difficulty { Easy, Medium, Hard }

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI equationText;
    public TextMeshProUGUI pointText;
    public TMP_InputField inputText;

    public float timeLimit = 30f; // seconds per question
    private float currentTime;
    private Coroutine countdownCoroutine;
    private bool isRunning = false;

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
                points++;
                pointText.text = $"Points: {points}";
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
    }
}
