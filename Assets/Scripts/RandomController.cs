using System.Collections;
using System.IO;
using UnityEngine;

public class RandomController : Singleton<RandomController> {

    static readonly int IMAGES_PER_OBJECT = 700;

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

        ChangeCamera.Instance.UpdateCamera(currObject);
        currObject.GetComponent<ObjectBounds>().UpdateBounds();

        //create folder for images if it doesn't exist
        string filePath = Application.streamingAssetsPath + "/" + currObject.name;
        if (!File.Exists(filePath)) {
            Directory.CreateDirectory(filePath);
        }

        //take picture
        Debug.Log("taking picture num: " + currImageNum);

        //random image size
        float width = Screen.width;
        float height = Screen.height;
        int randWith = Mathf.RoundToInt(Random.Range(width/2f, width));
        int randHeight = Mathf.RoundToInt(Random.Range(height / 2f, height));
        //take photo from active rend texture
        Texture2D photo = new Texture2D(randWith, randHeight, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, randWith, randHeight), 0, 0, false);
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
            IChangeable[] changeableItems = item.GetComponents<IChangeable>();
            foreach (IChangeable changeable in changeableItems) {
                if (changeable != null) {
                    changeable.ChangeRandom();
                }
            }
        }
    }
}
