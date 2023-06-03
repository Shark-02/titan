using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaAudioController : MonoBehaviour
{
    [SerializeField] UIFadeController UIC;
    [SerializeField] UIFadeController UIC_Accept;
    [SerializeField] UIFadeController UIC_Refuse;
    //[SerializeField] BlurController BC;
    public Button playButton;
    public Button AcceptButton;
    public Button RefuseButton;
    public AudioClip[] audioClips;//对话列表
    public AudioClip[] refusedClips;
    bool answered;
    bool accepted;
    private AudioSource audioSource;
    private int currentClipIndex = 0;//当前对话片段
    private bool isPlaying = true;//是否需要播放声音
    //public XRController controller;
    public Transform seaend;
    [SerializeField] LightParticleManager lightParticle;
    [SerializeField] ParticleSystemForceField particleForceField;
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioSource mysteryAudio;
    [SerializeField] Text[] acceptTexts;
    [SerializeField] GameObject gameover;
    Color particleColor = new Color(0.706f, 0.9384183f, 1);
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playButton.onClick.AddListener(OnPlayButtonClicked);
        AcceptButton.onClick.AddListener(OnAcceptButtonClicked);
        RefuseButton.onClick.AddListener(OnRefuseButtonClicked);

        StartCoroutine(PlayAudioClips());
    }

    public IEnumerator PlayAudioClips()
    {
        //BC.StartBlur();
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (isPlaying)
            {
                if (!answered)
                {
                    if (currentClipIndex < audioClips.Length - 1 && currentClipIndex > 0)
                    {

                        audioSource.clip = audioClips[currentClipIndex];
                        audioSource.Play();
                        yield return new WaitForSeconds(audioSource.clip.length);
                        audioSource.Stop();
                        UIC.UI_FadeIn_Event();
                    }
                    else if (currentClipIndex == 0)
                    {
                        audioSource.clip = audioClips[currentClipIndex];
                        audioSource.Play();
                        yield return new WaitForSeconds(audioSource.clip.length);
                        audioSource.Stop();
                        UIC.UI_FadeIn_Event();
                        //yield return new WaitForSeconds(5f);
                        //UIC.UI_FadeOut_Event();
                    }
                    else if (currentClipIndex == audioClips.Length - 1)
                    {
                        audioSource.clip = audioClips[currentClipIndex];
                        audioSource.Play();
                        yield return new WaitForSeconds(audioSource.clip.length);
                        audioSource.Stop();
                        UIC.UI_FadeOut_Event();
                        UIC_Accept.UI_FadeIn_Event();
                        UIC_Refuse.UI_FadeIn_Event();
                        isPlaying = false;
                        Debug.Log("UIC_Accept.UI_Fadein_Event");

                    }
                }
                else if (!accepted)
                {
                    if (currentClipIndex < refusedClips.Length - 1)
                    {
                        audioSource.clip = refusedClips[currentClipIndex];
                        audioSource.Play();
                        yield return new WaitForSeconds(audioSource.clip.length);
                        audioSource.Stop();
                        UIC.UI_FadeIn_Event();
                    }
                    else
                    {
                        audioSource.clip = refusedClips[currentClipIndex];
                        audioSource.Play();
                        float totTime = audioSource.clip.length;
                        lightParticle.StopLightParticle();
                        yield return new WaitForSeconds(totTime * .7f);
                        lightParticle.particlePlane.GetComponent<Collider>().enabled = false;
                        CurtainController.Instance.ShowCurtain(5f);
                        yield return new WaitForSeconds(totTime * .3f);
                        audioSource.Stop();
                        isPlaying = false;
                        StartCoroutine(DelayChangeScene(5));
                    }
                }
                isPlaying = false;
            }

            yield return null;
        }
    }
    IEnumerator DelayChangeScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        float audioFadeTime = delayTime * .6f;
        float audioFadeTimeFull = audioFadeTime;
        delayTime -= audioFadeTime;
        while (audioFadeTime > 0)
        {
            audioFadeTime -= Time.deltaTime;
            bgmAudio.volume = audioFadeTime / audioFadeTimeFull;
            yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene("spaceship");
    }
    public void OnPlayButtonClicked()
    {
        if (!isPlaying && ((!answered && currentClipIndex < audioClips.Length) || (answered && currentClipIndex < refusedClips.Length)))
        {
            isPlaying = true;
            //UIC.UI_FadeOut_Event();
            currentClipIndex++;
            UIC.UI_FadeOut_Event();
        }
    }
    public void OnAcceptButtonClicked()
    {
        UIC_Refuse.UI_FadeOut_Event();
        UIC_Accept.UI_FadeOut_Event();
        Debug.Log("UIC_Accept.UI_FadeOut_Event");
        answered = true;
        accepted = true;
        StartCoroutine("AcceptCallback");
    }
    public void OnRefuseButtonClicked()
    {
        UIC_Refuse.UI_FadeOut_Event();
        UIC_Accept.UI_FadeOut_Event();
        Debug.Log("UIC_Refuse.UI_FadeOut_Event");
        answered = true;
        accepted = false;
        currentClipIndex = 0;
        isPlaying = true;
    }
    IEnumerator AcceptCallback()
    {
        yield return new WaitForSeconds(1f);
        particleForceField.gravity = -20;
        yield return new WaitForSeconds(1f);
        particleForceField.gravity = 6;
        var emission = lightParticle.targetParticle.emission;
        float startMult = emission.rateOverTimeMultiplier;
        float endMult = startMult * 60;
        mysteryAudio.Play();
        float sumTime = 3;
        for (float cntSum = 0; cntSum < sumTime; cntSum += Time.deltaTime)
        {
            emission.rateOverTimeMultiplier = cntSum / sumTime * (endMult - startMult) + startMult;
            bgmAudio.volume = 1 - cntSum / sumTime;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        CurtainController.Instance.ShowCurtain(0);
        CurtainController.Instance.SetCurtainColor(particleColor);
        lightParticle.targetParticle.gameObject.SetActive(false);
        Debug.Log("!");
        //for (int i = 0; i < acceptTexts.Length; ++i)
        //{
        //    sumTime = 0;
        //    Color c = acceptTexts[i].color;
        //    while (sumTime < 2)
        //    {
        //        sumTime += Time.deltaTime;
        //        c.a = sumTime / 2;
        //        acceptTexts[i].color = c;
        //        yield return null;
        //    }
        //    c.a = 1;
        //    acceptTexts[i].color = c;
        //    yield return new WaitForSeconds(1f);
        //}
        yield return new WaitForSeconds(1f);
        for(sumTime=0;sumTime<2f;sumTime+=Time.deltaTime)
        {
            CurtainController.Instance.SetCurtainColor(Color.Lerp(particleColor, Color.black, sumTime / 2));
            yield return null;
        }
        CurtainController.Instance.SetCurtainColor(Color.black);
        yield return new WaitForSeconds(1f);
        gameover.SetActive(true);
        StoryShow_TMP.isStart = true;
        //UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }
}