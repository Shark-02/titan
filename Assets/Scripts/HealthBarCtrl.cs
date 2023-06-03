using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCtrl : MonoBehaviour
{
    public float MaxEnergy = 1000;
    public static float energy;
    public Slider slider;
    public float decreaseRate = 1f;
    [SerializeField] GameObject gameover;
    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(MaxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseHealth(decreaseRate * Time.deltaTime);
    }

    public void DecreaseHealth(float amount)
    {
        energy -= amount;
        // 更新血条显示
        slider.value = energy;

        if (energy <= 0)
        {
            Die();
        }
    }

    public void SetMaxHealth(float amount)
    {
        energy = amount;
        // 设置血条的最大值
        slider.maxValue = amount;
        // 初始化当前血量
        slider.value = energy;
    }
    [HideInInspector] public bool dead;
    void Die()
    {
        if (!dead)
            dead = true;
        else
            return;
        StartCoroutine("DieCoroutine");
    }
    IEnumerator DieCoroutine()
    {
        CurtainController.Instance.ShowCurtain(1f);
        yield return new WaitForSeconds(1f);
        gameover.SetActive(true);
        StoryShow_TMP.isStart = true;
    }
    public void SetDecreaseRate(float targetRate)
    {
        decreaseRate = targetRate;
    }
}
