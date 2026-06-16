using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private float timer;

    private void Update()
    {
        timer += GameTime.UnscaledDeltaTime;

        if (timer >= 0.25f) // Update 4 times per second
        {
            int fps = Mathf.RoundToInt(1f / GameTime.UnscaledDeltaTime);
            fpsText.text = fps.ToString();
            timer = 0f;
        }
    }
}
