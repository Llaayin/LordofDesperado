
using UnityEngine;

public class HumanoidAnimation : MonoBehaviour
{
    public Transform player;
    public Transform leftArm;
    public Transform rightArm;
    public Transform leftLeg;
    public Transform rightLeg;

    public float swingSpeed = 8f;
    public float swingAmount = 30f;
    public float legSwingAmount = 12f;

    public float attackDuration = 0.3f;
    public float attackSwingAngle = 45f;

    private Vector3 lastPosition;
    private float timer;
    private float attackTimer;
    private bool isAttacking;

    void Start()
    {
        lastPosition = player.position;
    }

    void Update()
    {
        Vector3 movement = player.position - lastPosition;
        movement.y = 0f;

        float speed = movement.magnitude / Time.deltaTime;
        lastPosition = player.position;

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            float progress = attackTimer / attackDuration;
            float attackSwing = Mathf.Sin(progress * Mathf.PI) * attackSwingAngle;

            rightArm.parent.localRotation =
                Quaternion.Euler(-attackSwing, 0f, 0f);

            if (progress >= 1f)
            {
                isAttacking = false;
                attackTimer = 0f;
            }
        }
        else if (speed > 0.1f)
        {
            timer += Time.deltaTime * swingSpeed;

            float swing = Mathf.Sin(timer) * swingAmount;
            float legSwing = Mathf.Sin(timer) * legSwingAmount;

            leftArm.localRotation =
                Quaternion.Euler(swing, 0f, 0f);

            rightArm.localRotation =
                Quaternion.Euler(-swing, 0f, 0f);

            leftLeg.localRotation =
                Quaternion.Euler(-legSwing, 0f, 0f);

            rightLeg.localRotation =
                Quaternion.Euler(legSwing, 0f, 0f);
        }
        else
        {
            timer = 0f;

            leftArm.localRotation = Quaternion.identity;
            rightArm.localRotation = Quaternion.identity;

            leftLeg.localRotation = Quaternion.identity;
            rightLeg.localRotation = Quaternion.identity;
        }
    }

    public void PlayAttack()
    {
        isAttacking = true;
        attackTimer = 0f;
    }
}
