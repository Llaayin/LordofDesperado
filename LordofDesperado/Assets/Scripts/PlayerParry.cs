
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerParry : MonoBehaviour
{
    public float parryDuration = 0.4f;
    public float parryCooldown = 1.2f;

    public bool IsParrying { get; private set; }

    private float nextParryTime;
    private ColorFlash colorFlash;

    void Awake()
    {
        colorFlash = GetComponent<ColorFlash>();
    }

    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.rightButton.wasPressedThisFrame &&
            Time.time >= nextParryTime)
        {
            StartCoroutine(Parry());
        }
    }

    IEnumerator Parry()
    {
        IsParrying = true;
        Debug.Log("Parry window active!");

        yield return new WaitForSeconds(parryDuration);

        IsParrying = false;
        nextParryTime = Time.time + parryCooldown;
    }

    public void SuccessfulParry()
    {
        if (colorFlash != null)
            colorFlash.Flash(new Color(1f, 0.75f, 0.1f), 0.4f);

        Debug.Log("PARRY SUCCESSFUL!");
    }
}
