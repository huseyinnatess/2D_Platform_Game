using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeControl : MonoBehaviour
{
    public static FadeControl fadeControl;
    CanvasGroup fadeCanvasGroup;

    private void Start()
    {
        fadeCanvasGroup = GetComponent<CanvasGroup>();
        Seffaflastir();
        fadeControl = this;
    }


    public void Seffaflastir()
    {
        fadeCanvasGroup.alpha = 1f;
        fadeCanvasGroup.DOFade(0f, 2f);
    }

    public void Matlastir()
    {
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.DOFade(1f, 2f);
    }
}
