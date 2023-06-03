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
    public LightParticleManager lightParticle;
    public ParticleSystemForceField particleForceField;
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
                        yield return new WaitForSeconds(totTime * .3f);
                        audioSource.Stop();
                        isPlaying = false;
                        CurtainController.Instance.ShowCurtain(3f);
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
        yield return new WaitForSeconds(.2f);
        particleForceField.gravity = 6;
        var emission = lightParticle.targetParticle.emission;
        float startMult = emission.rateOverTimeMultiplier;
        float endMult = startMult * 40;
        float sumTime = 4;
        for(float cntSum=0;cntSum<sumTime;cntSum+=Time.deltaTime)
        {
            emission.rateOverTimeMultiplier = cntSum / sumTime * (endMult - startMult) + startMult;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }
}