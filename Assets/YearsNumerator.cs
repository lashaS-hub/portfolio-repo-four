using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YearsNumerator : MonoBehaviour
{
    public int Speed;
    public GameObject Panel;
    public Transform Parent;
    public Text textPref;
    public int amountOfNumInYear;

    private Text[] numbers;
    private List<GameObject> numberOfObject;
    private int num;

    public void Start()
    {
        numberOfObject = new List<GameObject>();
        numbers = new Text[10];
        for (int i = 0; i < amountOfNumInYear; i++)
        {
            Generate(i);
        }

        
// TravelThroughTheTime();
        
    }

    public void TravelThroughTheTime()
    {
        StartCoroutine(NumUpdate(Random.Range(1000, 2500)));
    }

    public void Generate(int k)
    {
        GameObject center = new GameObject();
        center.name = k + " obj";
        var g = center.AddComponent<RectTransform>();
        g.sizeDelta = new Vector2(200, 400);
        center.transform.position = Vector3.forward * 1435;
        center.transform.parent = Parent;
        numberOfObject.Add(center);
        for (int i = 0; i < 10; i++)
        {
            var clone = Instantiate(textPref, center.transform);
            clone.transform.localPosition = Vector3.back * 450f;
            clone.transform.RotateAround(center.transform.position, Vector3.right, 36 * i);
            clone.text = "" + i;
            numbers[i] = clone;
        }
    }

    IEnumerator NumUpdate(int num)
    {
        Panel.SetActive(true);
        var list = new List<Coroutine>();
        for (int k = 0; k < numberOfObject.Count; k++)
        {
            list.Add(StartCoroutine(Randomize(numberOfObject[k])));
        // yield return 0;
        }
        yield return new WaitForSeconds(.5f);
        int i = numberOfObject.Count;
        while (i > 0)
        {
            i--;
            StopCoroutine(list[i]);
            yield return StartCoroutine(RotateNumerator(numberOfObject[i], num % 10));
            num = num / 10;
        }
        yield return new WaitForSeconds(.5f);
        Panel.SetActive(false);

    }

    IEnumerator Randomize(GameObject i)
    {
        while (true){
            i.transform.Rotate(Vector3.right, Speed);
            yield return null;
        }
    }

    IEnumerator RotateNumerator(GameObject i, int num)
    {
        var x = (360 - num * 36);
        if (x < i.transform.eulerAngles.x)
            x += 360;
        var newAngle = Quaternion.Euler(Vector3.right * x);
        Debug.Log(newAngle);
        

        while (Quaternion.Angle(newAngle, i.transform.localRotation) > Speed)
        {
            i.transform.Rotate(Vector3.right, Speed);
            
            // i.transform.localRotation = Quaternion.Lerp(i.transform.localRotation, newAngle, Time.deltaTime * 2);
            yield return null;
        }
        i.transform.localRotation = newAngle;

    }

}
