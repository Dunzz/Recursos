using System.Collections;
using UnityEngine;

public class Animation_CameraShake_2 : MonoBehaviour
{
    private Vector3 originalPosition;

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        originalPosition = transform.localPosition; // Guarda la posición inicial con el offset

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition; // Restablece la posición con offset
    }
}
