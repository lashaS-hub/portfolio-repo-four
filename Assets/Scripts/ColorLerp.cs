using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour {
    public Outline Outline;
    public Image image;


    public Slider SpeedSlider;

    public float sliderMaxValue;

	private void Start() {
        // Outline = GetComponent<Outline>();
        sliderMaxValue = SpeedSlider.maxValue;
    }
    void LateUpdate() {
        var col = Color.Lerp(Color.red,Color.green,SpeedSlider.value/sliderMaxValue);
        if(image)
            image.color = col;
        if(Outline)
            Outline.effectColor = col;

    }
}