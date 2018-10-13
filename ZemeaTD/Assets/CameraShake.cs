using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

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

    public float totalShakeTime = 0.5f;
    public float shakeAmt = 0;
    public Camera mainCamera;
    private bool shaking = false;
    private float timer = 0;
    

    private void Update() {        
        if (shaking)
        {
            if (timer < totalShakeTime)
            {
                Shake();
                timer += Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.zero;
                timer = 0;
                shaking = false;
            }
        }
    }

    public void ShakeOnce()
    {
        if (!shaking)
            shaking = true;
    }

    private void Shake() {
        if(shakeAmt > 0) {
            Vector2 newPos = new Vector2(Random.insideUnitSphere.x * shakeAmt, Random.insideUnitSphere.y * 1f + 1.78f);
            transform.position = newPos;
        }
    }
}
