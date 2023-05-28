using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public float dissolveSpeed = 1.0f; // 控制增加的速度
    private Material material;
    private float dissolveValue = 1.0f;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        // 获取材质
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        material.SetFloat("_dissolve", dissolveValue);

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            // 根据时间和速度更新 dissolveValue
            dissolveValue -= Time.deltaTime * dissolveSpeed;
            dissolveValue = Mathf.Clamp01(dissolveValue); // 限制在0到1的范围内
            // 设置材质的 dissolve 属性
            material.SetFloat("_dissolve", dissolveValue);
        }

    }
}
