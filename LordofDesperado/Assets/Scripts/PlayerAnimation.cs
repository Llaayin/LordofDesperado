
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Transform player;
    public Animator animator;

    private Vector3 lastPosition;

    void Start()
    {
        if (player != null)
        {
            lastPosition = player.position;
        }
    }

    void Update()
    {
        if (player == null || animator == null) return;

        Vector3 movement = player.position - lastPosition;
        movement.y = 0f;

        float speed = movement.magnitude / Time.deltaTime;

        animator.SetFloat("Speed", speed);

        lastPosition = player.position;
    }
}
