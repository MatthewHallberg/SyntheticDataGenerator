using System.Collections;
using System.IO;
using UnityEngine;

public class RandomController : MonoBehaviour {

    static readonly int IMAGES_PER_OBJECT = 25;

    int currChild;
    int currImageNum;

    void Start() {
        StartCoroutine(RandomRoutine());
    }

    IEnumerator RandomRoutine() {
        ActivateCurrChild();
        yield return new WaitForEndOfFrame();
        while (true) {
            ChangeAllItems();
            CheckForPicture();
            yield return new WaitForEndOfFrame();
        }
    }

    void ActivateCurrChild() {

        currImageNum = 0;

        foreach (Transform child in transform) {
            if (child.GetSiblingIndex() == currChild) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
    }

    void CheckForPicture() {

        if (currChild >= transform.childCount) {
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Training complete!");
            return;
        }

        TakePicture();
    }

    void TakePicture() {
        currImageNum++;

        Transform currObject = transform.GetChild(currChild);

        Camera.main.transform.LookAt(currObject);

        //create folder for images if it doesn't exist
        string filePath = Application.streamingAssetsPath + "/" + currObject.name;
        if (!File.Exists(filePath)) {
            Directory.CreateDirectory(filePath);
        }

        //take picture
        Debug.Log("taking picture num: " + currImageNum);
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        photo.Apply();
        byte[] data = photo.EncodeToJPG(80);
        DestroyImmediate(photo);
        File.WriteAllBytes(filePath + "/" + currImageNum + ".jpg", data);

        if (currImageNum >= IMAGES_PER_OBJECT) {
            currChild++;
            ActivateCurrChild();
        }
    }

    void ChangeAllItems() {
        foreach (GameObject item in FindObjectsOfType<GameObject>()) {
            IChangeable changeableItem = item.GetComponent<IChangeable>();
            if (changeableItem != null) {
                changeableItem.ChangeRandom();
            }
        }
    }
}
