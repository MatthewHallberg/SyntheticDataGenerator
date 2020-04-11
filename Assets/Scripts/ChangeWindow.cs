using System.Linq;
using UnityEditor;
using UnityEngine;

public class ChangeWindow : MonoBehaviour, IChangeable {

    public void ChangeRandom() {
        Rect R = GetMainGameView().position;
        R.width = Random.Range(720,1440);
        R.height = R.width / Random.Range(1.25f, 2.5f);
        GetMainGameView().position = R;
    }

   EditorWindow GetMainGameView() {
        EditorWindow[] windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
        return windows.FirstOrDefault(e => e.titleContent.text.Contains("Game"));
    }
}
