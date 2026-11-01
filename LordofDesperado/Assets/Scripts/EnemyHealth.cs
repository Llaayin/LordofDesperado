using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Death Settings")]
    public GameObject objectiveDrop;
    public TMP_Text defeatNotification;
    public float notificationDuration = 3f;
    public float ragdollDuration = 6f;

    private bool isDead;

    private Rigidbody rb;
    private EnemyAI enemyAI;

    void Awake()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        enemyAI = GetComponent<EnemyAI>();

        if (defeatNotification != null)
            defeatNotification.gameObject.SetActive(false);

        if (objectiveDrop != null)
            objectiveDrop.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("The Defeated Health: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;

        Debug.Log("THE DEFEATED HAS BEEN QUELLED!");

        // Stop enemy movement and attacks.
        if (enemyAI != null)
            enemyAI.enabled = false;

        // Activate physics.
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.WakeUp();
            // Launch the enemy away from the player and into the air.
            rb.AddForce(
                transform.forward * 12f + Vector3.up * 6f,
                ForceMode.VelocityChange
             );

            // Give the enemy a violent, unpredictable spin.
            rb.AddTorque(
                Random.insideUnitSphere * 15f,
                ForceMode.VelocityChange
            );
        }
        else
        {
            Debug.LogWarning("The Defeated has no Rigidbody!");
        }

        // Activate the objective drop.
        if (objectiveDrop != null)
        {
            objectiveDrop.transform.position = transform.position;
            objectiveDrop.SetActive(true);
        }

        // Display defeat notification.
        if (defeatNotification != null)
        {
            defeatNotification.gameObject.SetActive(true);
            defeatNotification.text = "THE DEFEATED HAS BEEN QUELLED";
        }

        StartCoroutine(HideNotification());
        StartCoroutine(RemoveRagdoll());
    }

    IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(notificationDuration);

        if (defeatNotification != null)
            defeatNotification.gameObject.SetActive(false);
    }

    IEnumerator RemoveRagdoll()
    {
        yield return new WaitForSeconds(ragdollDuration);

        gameObject.SetActive(false);
    }
}