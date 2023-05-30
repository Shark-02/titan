using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    //private void Start()
    //{
        //StartCoroutine(Shake(5.0f, 0.25f));
    //}
    public IEnumerator Shake(float duration, float magnitude)//摇晃时间、幅度
    {
        Vector3 originalPos = transform.localPosition;//相机原始位置

        float elapsed = 0.0f;//摇晃进行时间
        while (elapsed < duration)
        {
            float x = Random.Range(-2f, 2f) * magnitude;//x轴随机抖动幅度
            float y = Random.Range(-2f, 2f) * magnitude;//y轴随机抖动幅度

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;//再次复原
    }
}