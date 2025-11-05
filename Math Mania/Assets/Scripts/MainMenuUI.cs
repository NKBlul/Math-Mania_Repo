using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panel: ")]
    public GameObject panel;
    public TextMeshProUGUI difficultyText;
    public Slider difficultySlider;
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => ScenesManager.instance.LoadLevel("SpeedCountScene"));
    }

    public void DifficultySlider()
    {
        int difficultyValue = Mathf.RoundToInt(difficultySlider.value); // 0, 1, 2
        switch (difficultyValue)
        {
            case 0:
                difficultyText.text = $"Easy";
                break;
            case 1:
                difficultyText.text = $"Medium";
                break;
            case 2:
                difficultyText.text = $"Hard";
                break;
        }

        PlayerPrefs.SetInt("Difficulty", difficultyValue);
        PlayerPrefs.Save();
    }
}
