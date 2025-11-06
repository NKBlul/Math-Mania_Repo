using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public Button button;
    public string levelName;

    [Header("Panel: ")]
    public GameObject panel;
    public TextMeshProUGUI difficultyText;
    public Slider difficultySlider;
    public Button playButton;
    int difficultyValue = 0;
    private void Start()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ShowPanel);

        difficultySlider.onValueChanged.RemoveAllListeners();
        difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        //playButton.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        playButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() =>
        {
            Debug.Log($"PlayButton clicked: loading scene '{levelName}' (from LevelDisplay of {gameObject.name})");
            panel.SetActive(false);
            ScenesManager.instance.LoadLevel(levelName);
        });
    }

    public int GetDifficultyValue()
    {
        return difficultyValue;
    }

    public void OnDifficultyChanged(float value)
    {
        difficultySlider.value = difficultyValue = Mathf.RoundToInt(value); // 0, 1, 2
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
