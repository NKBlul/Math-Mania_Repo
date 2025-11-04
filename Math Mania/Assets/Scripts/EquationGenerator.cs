using System.Collections;
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
                StartCoroutine(Timer(0.5f));
                GenerateEquation();
                inputText.text = "";
            }
        }
    }

    IEnumerator Timer(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
