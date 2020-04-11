using UnityEngine;

public class ChangeAmbient : MonoBehaviour, IChangeable {

    public void ChangeRandom() {
        Color lightColor = Color.white;
        lightColor *= Random.Range(.8f, 1.3f);
        RenderSettings.ambientLight = lightColor;
    }
    
    void OnApplicationQuit() {
        //set light back to normal
        RenderSettings.ambientLight = Color.white;
    }
}
