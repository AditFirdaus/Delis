using System;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScreen : MonoBehaviour
{
    public AudioClip countdownSound;
    [Header("Values")]
    public float duration = 0;
    public float timeLeft = 0;
    public Action action;
    public CanvasGroup background;


    [Header("References")]
    public TMP_Text Rcountdown;

    public void Countdown(float duration, Action action)
    {

        this.duration = duration;
        this.action = action;
        Begin();
    }

    public void Begin()
    {
        Rcountdown.gameObject.SetActive(true);

        LeanAudio.play(countdownSound);

        gameObject.SetActive(true);
        gameObject.LeanCancel();
        gameObject.LeanValue(UpdateTime, duration, 0, duration)
            .setOnComplete(() => End()).setIgnoreTimeScale(true);

        Rcountdown.rectTransform.LeanCancel();
        Rcountdown.rectTransform.LeanScale(Vector2.one * 30, 0).setIgnoreTimeScale(true);
        Rcountdown.rectTransform.LeanScale(Vector2.one, duration / 4).setEaseInOutExpo().setIgnoreTimeScale(true);
    }

    public void UpdateTime(float t)
    {
        timeLeft = t;
        Rcountdown.text = String.Format("{0:0.0}", timeLeft);
    }

    public void End()
    {
        Rcountdown.gameObject.SetActive(false);
        background.gameObject.LeanCancel();
        background.alpha = 1;



        gameObject.LeanCancel();
        action?.Invoke();

        background.LeanAlpha(0, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true).setOnComplete(
            () =>
            {
                gameObject.SetActive(false);
            }
        );

    }
}
