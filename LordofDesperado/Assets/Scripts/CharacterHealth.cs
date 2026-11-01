
using UnityEngine;
using TMPro;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Desperado Meter")]
    public int desperadoMeter = 0;
    public int desperadoGainPerHit = 20;

    [Header("Desperado Activation")]
    public TMP_Text desperadoAnnouncement;
    public float announcementDuration = 3f;
    public float boostDuration = 10f;
    public float damageMultiplier = 2f;
    public float flashInterval = 0.25f;

    public bool IsDesperadoActive { get; private set; }

    private bool desperadoSequenceRunning;

    void Awake()
    {
        currentHealth = maxHealth;
        desperadoMeter = 0;
        IsDesperadoActive = false;

        if (desperadoAnnouncement != null)
            desperadoAnnouncement.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (!desperadoSequenceRunning && !IsDesperadoActive)
        {
            desperadoMeter += desperadoGainPerHit;
            desperadoMeter = Mathf.Clamp(desperadoMeter, 0, 100);
        }

        Debug.Log(gameObject.name + " Health: " + currentHealth);
        Debug.Log(gameObject.name + " Desperado: " + desperadoMeter);

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " has been defeated!");
            gameObject.SetActive(false);
            return;
        }

        if (desperadoMeter >= 100 && !desperadoSequenceRunning)
        {
            StartCoroutine(DesperadoSequence());
        }
    }

    IEnumerator DesperadoSequence()
    {
        desperadoSequenceRunning = true;

        // Phase 1: Flash the DESPERADO announcement.
        if (desperadoAnnouncement != null)
        {
            desperadoAnnouncement.text = "DESPERADO";
            desperadoAnnouncement.gameObject.SetActive(true);

            float elapsed = 0f;
            float nextFlash = flashInterval;

            while (elapsed < announcementDuration)
            {
                elapsed += Time.deltaTime;

                if (elapsed >= nextFlash)
                {
                    desperadoAnnouncement.gameObject.SetActive(
                        !desperadoAnnouncement.gameObject.activeSelf
                    );

                    nextFlash += flashInterval;
                }

                yield return null;
            }

            desperadoAnnouncement.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(announcementDuration);
        }

        // Phase 2: Activate double attack damage.
        IsDesperadoActive = true;

        Debug.Log(gameObject.name + " DESPERADO ACTIVATED!");

        float boostElapsed = 0f;

        while (boostElapsed < boostDuration)
        {
            boostElapsed += Time.deltaTime;

            float remaining =
                1f - (boostElapsed / boostDuration);

            desperadoMeter = Mathf.Clamp(
                Mathf.CeilToInt(remaining * 100f),
                0,
                100
            );

            yield return null;
        }

        // Phase 3: Return to normal combat.
        desperadoMeter = 0;
        IsDesperadoActive = false;
        desperadoSequenceRunning = false;

        Debug.Log(gameObject.name + " DESPERADO ENDED!");
    }

    public int GetAttackDamage(int baseDamage)
    {
        if (IsDesperadoActive)
            return Mathf.Max(1, Mathf.RoundToInt(baseDamage * damageMultiplier));

        return baseDamage;
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
