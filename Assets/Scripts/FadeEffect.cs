using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    private TextMeshProUGUI textFade;

    void Awake()
    {
        textFade = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeInOut());
    }


    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(FadeInOut(1, 0));
            yield return StartCoroutine(FadeInOut(0, 1));
        }
    }

    private IEnumerator FadeInOut(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;

            color.a = Mathf.Lerp(start, end, percent);

            textFade.color = color;

            yield return null;
        }
    }
}
