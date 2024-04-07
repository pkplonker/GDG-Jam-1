using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIComponent : MonoBehaviour
{

    public CanvasGroup contentCanvas;
    public CanvasGroup mainCanvas;
    public bool hideOnAwake;
    public float animationSpeed = 0.3f;


    protected virtual void Awake()
    {
        if (hideOnAwake)
        {
            DisplayComponent(this,false,true);
        }
    }

    protected virtual void Start()
    {
        SetVals();
    }


    public void PlayUIClick()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.globalSoundList.uiclick);
    }

    public void Close()
    {
        DisplayComponent(this,false);
    }
    public void DisplayComponent(UIComponent comp, bool isOn,bool overrideAnim = false)
    {
        comp.contentCanvas.interactable = isOn;
        comp.contentCanvas.blocksRaycasts = isOn;


        if (isOn)
        {
            SetVals();

            comp.mainCanvas.interactable = isOn;
            comp.mainCanvas.blocksRaycasts = isOn;
            comp.mainCanvas.alpha = (isOn) ? 1 : 0;
        }

        if (overrideAnim)
        {
            comp.contentCanvas.alpha = (isOn) ? 1 : 0;
            if (isOn) BeginInAnimation(comp.contentCanvas.GetComponent<RectTransform>(),true);
            else BeginOutAnimation(comp.contentCanvas.GetComponent<RectTransform>(),true);


            comp.mainCanvas.interactable = isOn;
            comp.mainCanvas.blocksRaycasts = isOn;
            comp.mainCanvas.alpha = (isOn) ? 1 : 0;
        }

        else
        {
            StartCoroutine(AnimationDelay(comp, isOn));
        }
    }

    protected virtual void SetVals()
    {

    }

    protected virtual void BeginInAnimation(RectTransform animated, bool noAnim = false)
    {

        
        var speed = noAnim ? 0 : animationSpeed;

        animated.DOScale(1.0f,speed);
    }
    protected virtual void BeginOutAnimation(RectTransform animated,bool noAnim = false)
    {
        var speed = noAnim ? 0 : animationSpeed;
        animated.DOScale(0.0f, speed);

    }


    private IEnumerator AnimationDelay(UIComponent comp, bool isOn)
    {

        if (isOn) { 
            comp.contentCanvas.alpha = 1;
            comp.mainCanvas.alpha = 1;
            BeginInAnimation(comp.contentCanvas.GetComponent<RectTransform>());  
        }
        
        else BeginOutAnimation(comp.contentCanvas.GetComponent<RectTransform>());

        yield return new WaitForSeconds(animationSpeed);

        comp.contentCanvas.alpha = (isOn) ? 1 : 0;


        comp.mainCanvas.interactable = isOn;
        comp.mainCanvas.blocksRaycasts = isOn;
        comp.mainCanvas.alpha = (isOn) ? 1 : 0;

    }
}
