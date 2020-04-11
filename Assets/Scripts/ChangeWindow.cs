using System.Linq;
using UnityEditor;
<<<<<<< HEAD
using System.Linq;
=======
using UnityEngine;
>>>>>>> 1073e6df3d66cd9a46a884fbe4d3205d154a34bc

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
