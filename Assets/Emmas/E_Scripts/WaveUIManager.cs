using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class WaveUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI TimeUntilNextWaveText;
    [SerializeField] private float waveTextDisplayTime = 3f;
    [SerializeField] private float waveTextFadeDuration = 1f;
    [SerializeField] private float waveCountdown;
    [SerializeField] private EnemyWaveManager EnemyWaveManager;

    void Start()
    {
        if (waveText == null || TimeUntilNextWaveText == null)
        {
            Debug.LogError("UI Text references are not assigned in the inspector.");
            return;
        }
        TimeUntilNextWaveText.gameObject.SetActive(false);
        //UpdateWaveUI(1, 0);
    }

    public void ShowWaveStartText(int currentWave)
    {
        EnemyWaveManager.GetCurrentWave(currentWave);
        StartCoroutine(ShowWaveText(currentWave));
    }

    public void StartCountdown(float timeUntilNextWave)
    {
        StartCoroutine(UpdateCountdown(timeUntilNextWave));
    }
    private IEnumerator ShowWaveText(int currentWave)
    {
        yield return new WaitForSeconds(1f); // Small delay before showing wave text

        waveText.gameObject.SetActive(true);
        waveText.text = $"Wave {currentWave}";

        // Fade in
        float elapsedTime = 0f;
        Color originalColor = waveText.color;
        while (elapsedTime < waveTextFadeDuration)
        {
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, elapsedTime / waveTextFadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        // Wait for display time
        yield return new WaitForSeconds(waveTextDisplayTime);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < waveTextFadeDuration)
        {
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, elapsedTime / waveTextFadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        waveText.gameObject.SetActive(false);

    }
    private IEnumerator UpdateCountdown(float timeUntilNextWave)
    {
        TimeUntilNextWaveText.gameObject.SetActive(true);
        float countdown = timeUntilNextWave;

        while (countdown > 0)
        {
            TimeUntilNextWaveText.text = $"Next Wave In: {Mathf.Ceil(countdown)}s";
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }

        TimeUntilNextWaveText.gameObject.SetActive(false);
    }
}
