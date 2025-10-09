using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class WaveUIManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> waveText;
    [SerializeField] private List<TextMeshProUGUI> TimeUntilNextWaveText;
    [SerializeField] private float waveTextDisplayTime = 3f;
    [SerializeField] private float waveTextFadeDuration = 1f;
    [SerializeField] private float waveCountdown;
    [SerializeField] private EnemyWaveManager EnemyWaveManager;
    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private AudioClip waveStartClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (waveText == null || TimeUntilNextWaveText == null)
        {
            Debug.LogError("UI Text references are not assigned in the inspector.");
            return;
        }

        foreach (var text in waveText)
            text.gameObject.SetActive(false);

        foreach (var text in TimeUntilNextWaveText)
            text.gameObject.SetActive(false);

        // TimeUntilNextWaveText.gameObject.SetActive(false);
        // waveText.gameObject.SetActive(false);
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
        audioSource.PlayOneShot(waveStartClip);
        yield return new WaitForSeconds(1f);

        foreach (var waveText in waveText)
        waveText.gameObject.SetActive(true);
        
        foreach (var waveText in waveText)
            waveText.text = $"Wave {currentWave}";

        float elapsedTime = 0f;

        Color originalColor = waveText[0].color;

        while (elapsedTime < waveTextFadeDuration)
        {
            foreach (var waveText in waveText)
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, elapsedTime / waveTextFadeDuration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        foreach (var waveText in waveText)
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        yield return new WaitForSeconds(waveTextDisplayTime);

        elapsedTime = 0f;
        while (elapsedTime < waveTextFadeDuration)
        {
            foreach (var waveText in waveText)
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, elapsedTime / waveTextFadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var waveText in waveText)
        waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        foreach (var waveText in waveText)
            waveText.gameObject.SetActive(false);

    }
    private IEnumerator UpdateCountdown(float timeUntilNextWave)
    {
        foreach (var TimeUntilNextWaveText in TimeUntilNextWaveText)
        TimeUntilNextWaveText.gameObject.SetActive(true);
        timeUntilNextWave = EnemyWaveManager.GetTimeBetweenWaves(timeUntilNextWave);
        float countdown = timeUntilNextWave;

        while (countdown > -1)
        {
            foreach (var TimeUntilNextWaveText in TimeUntilNextWaveText)
            TimeUntilNextWaveText.text = $"next wave in:{ Mathf.Ceil(countdown)}";
            yield return new WaitForSeconds(1f);

            if (countdown <= 6f && countdown > 0f)
            {
                audioSource.PlayOneShot(countdownClip);
            }

            countdown -= 1f;
        }

        foreach (var TimeUntilNextWaveText in TimeUntilNextWaveText)
        TimeUntilNextWaveText.gameObject.SetActive(false);
    }
}
