using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : MonoBehaviour
{

    public CanvasGroup contentCanvas;
    public CanvasGroup mainCanvas;
    public bool hideOnAwake;
    public float animationSpeed = 0.5f;


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
        }

        if (overrideAnim)
        {
            comp.contentCanvas.alpha = (isOn) ? 1 : 0;
        }

        else
        {
            StartCoroutine(AnimationDelay(comp, isOn));
        }
    }

    protected virtual void SetVals()
    {

    }

    protected virtual void BeginInAnimation()
    {
        
    }
    protected virtual void BeginOutAnimation()
    {

    }


    private IEnumerator AnimationDelay(UIComponent comp, bool isOn)
    {
        if (isOn) BeginInAnimation();
        else BeginOutAnimation();

        yield return new WaitForSeconds(animationSpeed);

        comp.contentCanvas.alpha = (isOn) ? 1 : 0;

        comp.mainCanvas.interactable = isOn;
        comp.mainCanvas.blocksRaycasts = isOn;
        comp.mainCanvas.alpha = (isOn) ? 1 : 0;

    }
}
