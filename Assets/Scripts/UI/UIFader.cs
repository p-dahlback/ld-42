using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour {

    public MaskableGraphic[] graphics;
    public float startAlpha;
    public float endAlpha;
    public bool reverse = false;
    public float fadeTime = 1f;

    private float time = 0.0f;

    private void OnEnable()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        var progress = Mathf.Min(time / fadeTime, 1.0f);
        var sinProgress = Mathf.Sin(progress * Mathf.PI / 2);
        var alpha = startAlpha + (reverse ? 1.0f - sinProgress : 1.0f) * (endAlpha - startAlpha);

        foreach (var graphic in graphics)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }

        if (sinProgress >= 1.0f)
        {
            enabled = false;
        }
    }
}
