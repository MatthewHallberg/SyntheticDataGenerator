using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ObjectBounds : MonoBehaviour {

    public GUISkin guiSkin;

    Camera cam;
    MeshFilter meshFilter;

    Box currBox = new Box();

    struct Box {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }

    void Start() {
        meshFilter = GetComponent<MeshFilter>();
        cam = Camera.main;
    }

    void OnGUI() {
        //convert to GUI
        Vector2 min = new Vector2(currBox.xMin, currBox.yMin);
        Vector2 max = new Vector2(currBox.xMax, currBox.yMax);

        currBox.yMin = Screen.height - currBox.yMin;
        currBox.yMax = Screen.height - currBox.yMax;

        //Construct a rect of the min and max positions and apply some margin
        Rect rect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

        //Render the box
        GUI.skin = guiSkin;
        GUI.Box(rect, "");
    }

    public void UpdateBounds() {
        Vector3[] verts = meshFilter.mesh.vertices;

        //convert to world point, then screen space
        for (int i = 0; i < verts.Length; i++) {
            verts[i] = cam.WorldToScreenPoint(transform.TransformPoint(verts[i]));
        }

        //Calculate the min and max positions
        currBox = new Box();
        currBox.xMin = verts[0].x;
        currBox.xMax = verts[0].x;
        currBox.yMin = verts[0].y;
        currBox.yMax = verts[0].y;
        for (int i = 0; i < verts.Length; i++) {
            currBox.xMin = currBox.xMin < verts[i].x ? currBox.xMin : verts[i].x;
            currBox.xMax = currBox.xMax > verts[i].x ? currBox.xMax : verts[i].x;
            currBox.yMin = currBox.yMin < verts[i].y ? currBox.yMin : verts[i].y;
            currBox.yMax = currBox.yMax > verts[i].y ? currBox.yMax : verts[i].y;
        }
    }
}
