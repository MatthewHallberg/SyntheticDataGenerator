using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakePictures : MonoBehaviour {

    const bool SHOW_BOXES = false;
    const int TOTAL_IMAGES = 2000;

    int testNum;
    int imageNum = 1;
    string parentPath;

    string testImagePath;
    string trainImagePath;
    string testLabelPath;
    string trainLabelPath;

    void Start() {
        CopyUtils();
        CreateDirectories();
        CreateLabelFiles();
        CreateLabelMap();
        testNum = Mathf.RoundToInt(TOTAL_IMAGES * .20f);
    }

    void Update() {
        if (PictureRoutine == null) {
            PictureRoutine = StartCoroutine(DelayPicture());
        }
    }

    void CopyUtils() {
        DirectoryInfo srcPath = new DirectoryInfo("./TFUtils");
        DirectoryInfo destPath =new DirectoryInfo(Application.streamingAssetsPath + "/UnityStuff/TFUtils");
        CopyAll(srcPath, destPath);

    }

    void CopyAll(DirectoryInfo source, DirectoryInfo target) {
        Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles()) {
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories()) {
            DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }

    void CreateDirectories() {
        //create parent folder
        parentPath = Application.streamingAssetsPath + "/UnityStuff";
        if (!File.Exists(parentPath)) {
            Directory.CreateDirectory(parentPath);
        }
        //create test and train folders
        testImagePath = parentPath + "/test";
        if (!File.Exists(testImagePath)) {
            Directory.CreateDirectory(testImagePath);
        }

        trainImagePath = parentPath + "/train";
        if (!File.Exists(trainImagePath)) {
            Directory.CreateDirectory(trainImagePath);
        }
    }

    void CreateLabelFiles() {
        //create label files
        testLabelPath = parentPath + "/test.txt";
        File.WriteAllText(testLabelPath, "");
        trainLabelPath = parentPath + "/train.txt";
        File.WriteAllText(trainLabelPath, "");
    }

    void CreateLabelMap() {
        string labelMapPath = parentPath + "/labelmap.pbtxt";
        string labelMap = "";
        List<string> uniqueNames = new List<string>();
        foreach (Transform child in transform) {
            if (!uniqueNames.Contains(child.gameObject.name)) {
                uniqueNames.Add(child.gameObject.name);
                labelMap += "item {\n" +
                "\tid: " + (child.GetSiblingIndex() + 1) + "\n" +
                "\tname: '" + child.gameObject.name + "'\n" +
                "}\n";
            }
        }
        File.WriteAllText(labelMapPath, labelMap);
    }

    Coroutine PictureRoutine;
    IEnumerator DelayPicture() {
        ChangeAllItems();
        yield return new WaitForEndOfFrame();
        //update bounds of all objects
        foreach (ObjectBounds bounds in FindObjectsOfType<ObjectBounds>()) {
            bounds.UpdateBounds(SHOW_BOXES);
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

            //make sure bounds are within image
            if (tfRect.xMin != tfRect.xMax && tfRect.yMin != tfRect.xMax) {
                line += "," + obj.Key.name + "," + tfRect.xMin + ","
                    + tfRect.xMax + "," + tfRect.yMin + "," + tfRect.yMax;
            }
         }

        StreamWriter writer;

        if (imageNum > testNum) {
            writer = new StreamWriter(trainLabelPath, true);
        } else {
            writer = new StreamWriter(testLabelPath, true);
        }

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
        if (imageNum > testNum) {
            File.WriteAllBytes(trainImagePath + "/" + imageNum + ".jpg", data);
        } else {
            File.WriteAllBytes(testImagePath + "/" + imageNum + ".jpg", data);
        }
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
