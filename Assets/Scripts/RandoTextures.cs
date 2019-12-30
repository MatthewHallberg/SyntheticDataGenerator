using UnityEngine;

public class RandoTextures : Singleton<RandoTextures> {

    Object[] textures;
    int texNum;

    protected override void Awake() {
        base.Awake();
        textures = Resources.LoadAll("Textures", typeof(Texture));
        Shuffle(textures);
    }

    public Texture GetRandomTexture() {
        if (texNum < textures.Length - 1) {
            texNum++;
        } else {
            texNum = 0;
        }
        return textures[texNum] as Texture;
    }

    void Shuffle(Object[] items) {
        for (int t = 0; t < items.Length; t++) {
            Object tmp = items[t];
            int r = Random.Range(t, items.Length);
            items[t] = items[r];
            items[r] = tmp;
        }
    }
}
