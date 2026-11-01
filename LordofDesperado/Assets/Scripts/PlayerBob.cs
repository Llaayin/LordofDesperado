
using UnityEngine;

public class PlayerBob : MonoBehaviour
{
    public Transform visual;
    public Animator animator;

    public float bobAmount = 0.04f;
    public float bobSpeed = 10f;
    public float tiltAmount = 5f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float timer;

    void Start()
    {
        startPosition = visual.localPosition;
        startRotation = visual.localRotation;
    }

    void Update()
    {
        float speed = animator.GetFloat("Speed");

        if (speed > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;

            float bob = Mathf.Abs(Mathf.Sin(timer)) * bobAmount;
            float tilt = Mathf.Sin(timer) * tiltAmount;

            visual.localPosition =
                startPosition + Vector3.up * bob;

            visual.localRotation =
                startRotation * Quaternion.Euler(0f, 0f, tilt);
        }
        else
        {
            timer = 0f;
            visual.localPosition = startPosition;
            visual.localRotation = startRotation;
        }
    }
}
