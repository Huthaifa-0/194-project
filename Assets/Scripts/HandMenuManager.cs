using UnityEngine;
using TMPro;

public class HandMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform seaHealthFill;
    [SerializeField] private RectTransform beachHealthFill;
    [SerializeField] private TextMeshProUGUI seaScoreText;
    [SerializeField] private TextMeshProUGUI beachScoreText;

    public void IncrementSeaHealth()
    {
        float currentFill = seaHealthFill.localScale.x;
        float newFill = Mathf.Clamp01(currentFill + 0.1f); // Add 10%
        seaHealthFill.localScale = new Vector3(newFill, 1f, 1f);
        seaScoreText.text = $"{Mathf.Round(newFill * 100)}%";
    }

    public void IncrementBeachHealth()
    {
        float currentFill = beachHealthFill.localScale.x;
        float newFill = Mathf.Clamp01(currentFill + 0.1f); // Add 10%
        beachHealthFill.localScale = new Vector3(newFill, 1f, 1f);
        beachScoreText.text = $"{Mathf.Round(newFill * 100)}%";
    }
}