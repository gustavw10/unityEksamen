using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   
    //Coroutine
    public IEnumerator Shake (float duration, float power)
    {
        //Tager en vector3(3d verden) og finder vores original position (kamera)
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * power;
            float y = Random.Range(-1f, 1f) * power;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        //Kom tilbage til originale position
        transform.localPosition = originalPos;
    }
}