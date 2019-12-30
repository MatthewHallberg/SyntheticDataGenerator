using UnityEngine;
using UnityEditor;

public class ChangeWindow : MonoBehaviour, IChangeable {

    public void ChangeRandom() {
        Rect R = GetMainGameView().position;
        R.width = Random.Range(720,1440);
        R.height = R.width / Random.Range(1f, 2.5f);
        GetMainGameView().position = R;
    }

   EditorWindow GetMainGameView() {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetMainGameView.Invoke(null, null);
        return (EditorWindow)Res;
    }
}
