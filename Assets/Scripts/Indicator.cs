using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public struct IndicatorValue
{
    [Range(0f, 1f)] public float point;
    public Color color;

}

[RequireComponent(typeof(Image))]
public class Indicator : MonoBehaviour
{
    public IndicatorValue low;
    public IndicatorValue medium;
    public IndicatorValue high;
    // [Range(0.0f, 1.0f)] public float value;


    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void FillImage(float f, Color color)
    {
        myImage.fillAmount = f;
        myImage.color = color;
		// if (myImage.fillAmount >= 0 && myImage.fillAmount <= low.point)
        // {
        //     myImage.color = low.color;

        // }
        // else if (myImage.fillAmount > low.point && myImage.fillAmount <= medium.point)
        // {
        //     myImage.color = medium.color;
        // }
        // else if (myImage.fillAmount > medium.point && myImage.fillAmount <= high.point)
        // {
        //     myImage.color = high.color;
        // }
    }

    public void ResetIndicator()
    {
        myImage.fillAmount = 0;
    }


}
