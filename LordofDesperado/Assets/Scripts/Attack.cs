
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 2f;
    public float attackCooldown = 0.7f;

    public HumanoidAnimation humanoidAnimation;

    private float nextAttackTime;

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame &&
            Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        if (humanoidAnimation != null)
        {
            humanoidAnimation.PlayAttack();
        }

        CharacterHealth characterHealth = GetComponent<CharacterHealth>();

        int attackDamage = damage;

        if (characterHealth != null)
            attackDamage = characterHealth.GetAttackDamage(damage);

        Collider[] hits = Physics.OverlapSphere(
            transform.position + transform.forward,
            attackRange
        );

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                break;
            }
        }
    }
}
