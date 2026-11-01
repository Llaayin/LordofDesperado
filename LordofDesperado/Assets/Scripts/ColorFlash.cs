
using UnityEngine;
using System.Collections;

public class ColorFlash : MonoBehaviour
{
    private Renderer[] renderers;
    private MaterialPropertyBlock block;
    private Coroutine flashRoutine;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        block = new MaterialPropertyBlock();
    }

    public void Flash(Color color, float duration = 0.2f)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine(color, duration));
    }

    IEnumerator FlashRoutine(Color color, float duration)
    {
        foreach (Renderer rend in renderers)
        {
            rend.GetPropertyBlock(block);
            block.SetColor("_BaseColor", color);
            block.SetColor("_Color", color);
            rend.SetPropertyBlock(block);
        }

        yield return new WaitForSeconds(duration);

        foreach (Renderer rend in renderers)
            rend.SetPropertyBlock(null);

        flashRoutine = null;
    }
}
