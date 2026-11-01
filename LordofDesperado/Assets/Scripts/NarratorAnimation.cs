
using UnityEngine;
using UnityEngine.InputSystem;

public class NarratorAnimation : MonoBehaviour
{
    [Header("Body Parts")]
    public Transform leftArm;
    public Transform rightArm;
    public Transform leftLeg;
    public Transform rightLeg;

    [Header("Staff")]
    public Transform staffPivot;

    [Header("Walking")]
    public float walkFrequency = 8f;
    public float legSwingAngle = 25f;
    public float armSwingAngle = 15f;

    [Header("Attack")]
    public float attackAngle = 65f;
    public float attackDuration = 0.3f;

    private Quaternion leftArmStart;
    private Quaternion rightArmStart;
    private Quaternion leftLegStart;
    private Quaternion rightLegStart;
    private Quaternion staffStart;

    private float walkTimer;
    private float attackTimer;
    private bool isAttacking;

    void Start()
    {
        if (leftArm != null) leftArmStart = leftArm.localRotation;
        if (rightArm != null) rightArmStart = rightArm.localRotation;
        if (leftLeg != null) leftLegStart = leftLeg.localRotation;
        if (rightLeg != null) rightLegStart = rightLeg.localRotation;
        if (staffPivot != null) staffStart = staffPivot.localRotation;
    }

    void Update()
    {
        AnimateWalking();
        AnimateAttack();
    }

    void AnimateWalking()
    {
        bool moving = false;

        if (Keyboard.current != null)
        {
            moving =
                Keyboard.current.wKey.isPressed ||
                Keyboard.current.aKey.isPressed ||
                Keyboard.current.sKey.isPressed ||
                Keyboard.current.dKey.isPressed;
        }

        if (moving)
            walkTimer += Time.deltaTime * walkFrequency;

        float cycle = Mathf.Sin(walkTimer);

        float leftLegAngle = moving ? cycle * legSwingAngle : 0f;
        float rightLegAngle = moving ? -cycle * legSwingAngle : 0f;
        float rightArmAngle = moving ? cycle * armSwingAngle : 0f;

        SetRotation(leftLeg, leftLegStart, leftLegAngle);
        SetRotation(rightLeg, rightLegStart, rightLegAngle);
        SetRotation(leftArm, leftArmStart, 0f);
        SetRotation(rightArm, rightArmStart, rightArmAngle);
    }

    void SetRotation(Transform part, Quaternion start, float angle)
    {
        if (part == null) return;

        part.localRotation =
            start * Quaternion.Euler(angle, 0f, 0f);
    }

    void AnimateAttack()
    {
        if (staffPivot == null || !isAttacking) return;

        attackTimer += Time.deltaTime;

        float progress = Mathf.Clamp01(attackTimer / attackDuration);
        float swing = Mathf.Sin(progress * Mathf.PI) * attackAngle;

        staffPivot.localRotation =
            staffStart * Quaternion.Euler(swing, 0f, 0f);

        if (progress >= 1f)
        {
            isAttacking = false;
            staffPivot.localRotation = staffStart;
        }
    }

    public void PlayAttack()
    {
        if (staffPivot == null) return;

        attackTimer = 0f;
        isAttacking = true;
    }
}
