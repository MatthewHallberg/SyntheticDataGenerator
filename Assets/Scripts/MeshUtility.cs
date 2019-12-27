using UnityEngine;

public class MeshUtility : MonoBehaviour {

    public static Mesh GetMesh(Transform transform) {
        //return mesh from either skinned mesh renderer or mesh filter
        SkinnedMeshRenderer skinnedRend = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        MeshFilter meshFilter = transform.GetComponentInChildren<MeshFilter>();
        return skinnedRend != null ? skinnedRend.sharedMesh : meshFilter.mesh;
    }

    public static void SetMesh(Transform desiredTrans, Mesh mesh) {
        //return mesh from either skinned mesh renderer or mesh filter
        SkinnedMeshRenderer skinnedRend = desiredTrans.GetComponentInChildren<SkinnedMeshRenderer>();
        MeshFilter meshFilter = desiredTrans.GetComponentInChildren<MeshFilter>();
        if (skinnedRend != null) skinnedRend.sharedMesh = mesh;
        if (meshFilter != null) meshFilter.sharedMesh = mesh;
    }

    public static Mesh CloneMesh(Mesh original) {

        Mesh clonedMesh = new Mesh {
            //copy mesh
            vertices = original.vertices,
            triangles = original.triangles,
            normals = original.normals,
            tangents = original.tangents,
            uv = original.uv,
            uv2 = original.uv2,
            uv3 = original.uv3,
            uv4 = original.uv4,
            uv5 = original.uv5,
            uv6 = original.uv6,
            uv7 = original.uv7,
            uv8 = original.uv8,
            subMeshCount = original.subMeshCount
        };

        return clonedMesh;
    }
}
