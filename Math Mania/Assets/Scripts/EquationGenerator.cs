using TMPro;
using UnityEngine;

public class EquationGenerator : MonoBehaviour
{
    public string currentEquation;
    public int correctAnswer;
    public enum Difficulty { Easy, Medium, Hard }

    public TextMeshProUGUI equationText;
    public TMP_InputField inputText;

    private void Start()
    {
       GenerateEquation();
    }

    public void GenerateEquation()
    {
        int a = Random.Range(1, 20);
        int b = Random.Range(1, 20);
        //string op = Random.Range(1, 2) == 0 ? "+" : "-";
        equationText.text = currentEquation = $"{a} + {b}";
        correctAnswer = a + b;
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
            }
        }
    }
}
