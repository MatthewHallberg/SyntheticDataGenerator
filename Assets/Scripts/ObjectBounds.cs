using UnityEngine;

public class ObjectBounds : Singleton<ObjectBounds> {

    public GUISkin guiSkin;

    Camera cam;
    Rect currBox = new Rect();
    Rect photoRect = new Rect();
    bool showBox = true;

    void Start() {
        cam = Camera.main;
    }

    void OnGUI() {

        if (!showBox) {
            // return;
        }

        //resize box for GUI coords
        Vector2 min = new Vector2(currBox.xMin, currBox.yMin);
        Vector2 max = new Vector2(currBox.xMax, currBox.yMax);
        Rect rect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

        //Render the box
        GUI.skin = guiSkin;
        GUI.Box(rect, "");
    }

    public Rect GetBounds() {
        return photoRect;
    }

    public void UpdateBounds(Transform trans, bool visualize = true) {

        showBox = visualize;
        
        Vector3[] verts = MeshUtility.GetMesh(trans).vertices;

        //convert to world point, then screen space
        for (int i = 0; i < verts.Length; i++) {
            verts[i] = cam.WorldToScreenPoint(trans.TransformPoint(verts[i]));
        }

        //create new box
        currBox = new Rect {
            xMin = verts[0].x,
            xMax = verts[0].x,
            yMin = verts[0].y,
            yMax = verts[0].y
        };

        //find min and max screen space values
        for (int i = 0; i < verts.Length; i++) {
            currBox.xMin = currBox.xMin < verts[i].x ? currBox.xMin : verts[i].x;
            currBox.xMax = currBox.xMax > verts[i].x ? currBox.xMax : verts[i].x;
            currBox.yMin = currBox.yMin < verts[i].y ? currBox.yMin : verts[i].y;
            currBox.yMax = currBox.yMax > verts[i].y ? currBox.yMax : verts[i].y;
        }

        photoRect = currBox;

        currBox.yMin = Screen.height - currBox.yMin;
        currBox.yMax = Screen.height - currBox.yMax;
    }
}
