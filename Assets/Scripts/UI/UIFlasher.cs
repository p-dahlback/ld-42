using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlasher : MonoBehaviour {

    public MaskableGraphic graphic;
    public float flashesPerSecond = 4;
    public float minimumAlpha = 0.2f;
    public float maximumAlpha = 0.8f;

    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        currentTime %= 1.0f / flashesPerSecond;

        Color color = graphic.color;
        float interpolation = (float)(Mathf.Cos((currentTime * flashesPerSecond) * Mathf.PI * 2) + 1) / 2;
        color.a = minimumAlpha + interpolation * (maximumAlpha - minimumAlpha);
        graphic.color = color;
    }

    void OnDisable()
    {
        Color color = graphic.color;
        color.a = maximumAlpha;
        graphic.color = color;
    }
}
