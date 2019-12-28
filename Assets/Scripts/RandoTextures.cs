using UnityEngine;

public class RandoTextures : Singleton<RandoTextures> {

    Object[] textures;
    int texNum;

    protected override void Awake() {
        base.Awake();
        textures = Resources.LoadAll("Textures", typeof(Texture));
    }

    public Texture GetRandomTexture() {
        if (texNum < textures.Length - 1) {
            texNum++;
        } else {
            texNum = 0;
        }
        return textures[texNum] as Texture;
    }
}
