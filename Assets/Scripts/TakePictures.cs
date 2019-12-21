using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakePictures : MonoBehaviour {

    static readonly int TOTAL_IMAGES = 100;

    int imageNum = 1;
    string imagePath;
    string labelPath;

    void Start() {
        //create image folder
        imagePath = Application.streamingAssetsPath + "/Images";
        if (!File.Exists(imagePath)) {
            Directory.CreateDirectory(imagePath);
        }

        //create label file
        labelPath = Application.streamingAssetsPath + "/labelData.txt";

        //clear label file
        File.WriteAllText(labelPath, "");
    }

    void Update() {
        if (PictureRoutine == null) {
            PictureRoutine = StartCoroutine(DelayPicture());
        }
    }

    Coroutine PictureRoutine;
    IEnumerator DelayPicture() {
        ChangeAllItems();
        yield return new WaitForEndOfFrame();
        //update bounds of all objects
        foreach (ObjectBounds bounds in FindObjectsOfType<ObjectBounds>()) {
            bounds.UpdateBounds();
        }

        WriteObjectsToFile(ObjectController.Instance.GetObjects());

        yield return new WaitForEndOfFrame();

        TakeFullScreenPicture();

        if (imageNum == TOTAL_IMAGES) {
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Training data collected!");
        } else {
            imageNum++;
            PictureRoutine = null;
        }
    }

    void WriteObjectsToFile(Dictionary<GameObject, Rect> objects) {
        //image_id, image_width, image_height, label_name, x1, x2, y1, y2 
        string line = imageNum + "," + Screen.width + "," + Screen.height;
        foreach (KeyValuePair<GameObject,Rect> obj in objects) {

            Rect tfRect = ConvertUnityRectToTensorflow(obj.Value);

            line += "," + obj.Key.name + "," + tfRect.xMin + ","
                + tfRect.xMax + "," + tfRect.yMin + "," + tfRect.yMax;
         }
        StreamWriter writer = new StreamWriter(labelPath, true);
        writer.WriteLine(line);
        writer.Close();
    }

    Rect ConvertUnityRectToTensorflow(Rect unityRect) {
        return new Rect {
            xMin = Mathf.RoundToInt(unityRect.xMin),
            xMax = Mathf.RoundToInt(unityRect.xMax),
            yMin = Screen.height - Mathf.RoundToInt(unityRect.yMax),
            yMax = Screen.height - Mathf.RoundToInt(unityRect.yMin)
        };
    }

    Texture2D TakeCroppedPicture(Rect bounds) {
        Texture2D photo = new Texture2D((int)bounds.width, (int)bounds.height, TextureFormat.RGB24, false);
        photo.ReadPixels(bounds, 0, 0, false);
        return photo;
    }

    void TakeFullScreenPicture() {
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        photo.Apply();
        byte[] data = photo.EncodeToJPG(75);
        DestroyImmediate(photo);
        File.WriteAllBytes(imagePath + "/" + imageNum + ".jpg", data);
    }

    void ChangeAllItems() {
        foreach (GameObject item in FindObjectsOfType<GameObject>()) {
            IChangeable[] changeableItems = item.GetComponents<IChangeable>();
            foreach (IChangeable changeable in changeableItems) {
                if (changeable != null) {
                    changeable.ChangeRandom();
                }
            }
        }
        ObjectController.Instance.ActivateObjects();
    }
}
