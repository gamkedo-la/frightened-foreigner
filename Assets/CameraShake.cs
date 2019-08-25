using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform CamerasTransform;

    private float shakeDuration = 0f;

    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPosition;

    private void Awake()
    {
        CamerasTransform = GetComponent(typeof(Transform)) as Transform;
    }

    
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetShakeSettings()
    {
        shakeDuration = 0.5f;
        originalPosition = CamerasTransform.localPosition;
    }

    public void ShakeTheCamera()
    {
        if (shakeDuration > 0)
        {
            CamerasTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            CamerasTransform.localPosition = originalPosition;
        }
    }
}
