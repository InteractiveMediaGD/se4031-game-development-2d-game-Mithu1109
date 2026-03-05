using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    public float flashAlpha = 0.4f;
    public AudioSource hitSound;

    public void Flash()
{
    hitSound.Play();
    StartCoroutine(FlashRoutine());
}

    IEnumerator FlashRoutine()
    {
        Color color = flashImage.color;
        color.a = flashAlpha;
        flashImage.color = color;

        yield return new WaitForSeconds(flashDuration);

        color.a = 0f;
        flashImage.color = color;
    }
}