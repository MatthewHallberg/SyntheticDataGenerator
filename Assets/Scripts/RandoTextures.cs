using UnityEngine;

public class RandoTextures : Singleton<RandoTextures> {

    Object[] textures;

    void Start() {
        textures = Resources.LoadAll("Textures", typeof(Texture));
    }

    public Texture GetRandomTexture() {
        return textures[Random.Range(0, textures.Length)] as Texture;
    }
}
