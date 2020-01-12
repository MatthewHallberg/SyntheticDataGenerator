using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmbient : MonoBehaviour, IChangeable {

    public void ChangeRandom() {
        Color lightColor = Color.white;
        lightColor *= Random.Range(.5f, 1.5f);
        RenderSettings.ambientLight = lightColor;
    }
}
