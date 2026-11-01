
using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float detectionRange = 12f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 8f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float attackTelegraph = 0.65f;
    public int attackDamage = 10;

    private Transform target;
    private float nextAttackTime;
    private bool isAttacking;

    private ColorFlash colorFlash;

    void Awake()
    {
        colorFlash = GetComponent<ColorFlash>();
    }

    void Start()
    {
        FindTarget();
    }

    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            FindTarget();
            return;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > detectionRange || isAttacking)
            return;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion rotation = Quaternion.LookRotation(-direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (distance > attackRange)
        {
            transform.position += direction.normalized *
                                  moveSpeed * Time.deltaTime;
        }
        else if (Time.time >= nextAttackTime)
        {
            StartCoroutine(AttackSequence());
        }
    }

    void FindTarget()
    {
        GameObject player = GameObject.Find("Player");
        GameObject narrator = GameObject.Find("TheNarrator");

        if (player != null && player.activeInHierarchy)
            target = player.transform;
        else if (narrator != null && narrator.activeInHierarchy)
            target = narrator.transform;
    }

    IEnumerator AttackSequence()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldown;

        
        if (colorFlash != null)
            colorFlash.Flash(Color.red, attackTelegraph);

        Debug.Log("The Defeated is preparing an attack!");

        yield return new WaitForSeconds(attackTelegraph);

        // Check that the target is still valid and in range
        if (target != null && target.gameObject.activeInHierarchy)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackRange + 0.5f)
            {
                PlayerParry parry = target.GetComponent<PlayerParry>();

                if (parry != null && parry.IsParrying)
                {
                    parry.SuccessfulParry();

                    if (colorFlash != null)
                        colorFlash.Flash(new Color(0.65f, 0.2f, 1f), 0.5f);
                }
                else
                {
                    CharacterHealth health = target.GetComponent<CharacterHealth>();

                    if (health != null)
                        health.TakeDamage(attackDamage);
                }
            }
        }

        isAttacking = false;
    }
}
