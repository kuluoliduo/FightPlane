using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using ClashGame;

[AddComponentMenu("UI/Interaction/Button Effect")]

public class UIButtonEffect : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 originalPosition;
    void Awake()
    {
        originalScale = transform.GetComponent<RectTransform>().localScale;
        originalPosition = transform.GetComponent<RectTransform>().localPosition;
    }

    public int audioClipID = 7;
    public void PlayTheAudioClip()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            if (btn.interactable == true)
            {
                //GameApp.m_AudioManager.PlayUIAudioClip(UICanvasMgr.mInstance.GetUIRoot().transform.FindChild("UIAudio").gameObject, audioClipID);
            }
        }
    }

    public Vector3 endScale = new Vector3(1.1f,1.1f,1);
    public float scaleDuration = 0.2f;
    public void PlayScaleEnlarge()
    {
        //Tweener tweener = transform.DOScale(endScale, scaleDuration);
        //设置这个Tween不受Time.scale影响
        //tweener.SetUpdate(true);
        //tweener.SetEase(Ease.OutBack);
    }
    public void PlayScaleLesson()
    {
        //Tweener tweener = transform.DOScale(originalScale, scaleDuration);
        //设置这个Tween不受Time.scale影响
        //tweener.SetUpdate(true);
        //tweener.SetEase(Ease.OutBack);
    }

    public Vector2 offset = new Vector2(-1, 1);
    public float offsetDuration = 0.2f;
    public void PlayPositionMove()
    {
        //Debuger.Log(originalPosition);
        //Debuger.Log(transform.GetComponent<RectTransform>().localPosition);
        //Debuger.Log(transform.GetComponent<RectTransform>().sizeDelta);
        //Tweener tweener = transform.DOMove(new Vector3(originalPosition.x - offset.x, originalPosition.y - offset.y, originalPosition.z), offsetDuration);
        //设置这个Tween不受Time.scale影响
        //tweener.SetUpdate(true);
        //tweener.SetEase(Ease.OutBack);
        transform.localPosition = new Vector3(originalPosition.x - offset.x, originalPosition.y - offset.y, originalPosition.z);
    }
    public void PlayPositionMoveBack()
    {
        //Debuger.Log("===" + transform.localPosition);
        //Debuger.Log("===" + transform.GetComponent<RectTransform>().localPosition);
        //Debuger.Log("===" + transform.GetComponent<RectTransform>().sizeDelta);
        //Tweener tweener = transform.DOScale(originalPosition, offsetDuration);
        //设置这个Tween不受Time.scale影响
        //tweener.SetUpdate(true);
        //tweener.SetEase(Ease.OutBack);
        transform.localPosition = originalPosition;
    }
}
