using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    #region singleton
    public static CameraShake instance;

    public static CameraShake GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public float totalShakeTime;
    public float shakeAmount;
    public Camera mainCamera;
    private bool shaking = false;
    private float timer = 0;
    

    private void Update() {
        if (shaking)
        {
            if (timer < totalShakeTime)
            {
                timer += Time.deltaTime;
                Vector2 newPos = new Vector2(Random.insideUnitSphere.x * shakeAmount, Random.insideUnitSphere.y * 1f + 1.78f);
                transform.position = newPos;
            }
            else
            {
                shaking = false;
                transform.position = Vector3.zero;
                timer = 0;
            }
        }
    }

    public void Shake(float _shakeAmount, float _shakeTime) {
        totalShakeTime = _shakeTime;
        shakeAmount = _shakeAmount;
        shaking = true;
    }
}
