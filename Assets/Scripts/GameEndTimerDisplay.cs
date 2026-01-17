using UnityEngine;

public class GameEndTimerDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text uiText;

    void Update()
    {
        var minutes = Mathf.FloorToInt(GameManager.instance.timerSeconds / 60);
        var seconds = Mathf.FloorToInt(GameManager.instance.timerSeconds % 60);

        uiText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
