using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Methods
{

    public static int AngularSpeed(float radius, float degPerSec)
    {
        return (int)(2 * Mathf.PI * radius * (degPerSec / 360));
    }


    public static Transform FindObjectWithTag(this Transform root, string name)
    {
        if (root.tag == name) return root;
        foreach (Transform child in root)
        {
            Debug.Log(child.name);
            var n = child.FindObjectWithTag(name);
            if (n != null) return n;
        }
        return null;
    }


}
