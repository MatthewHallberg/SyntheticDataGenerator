using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeMesh : MonoBehaviour, IChangeable {

    const float radius = .3f;
    const float distance = .3f;

    Mesh originalMesh;

    void Start() {
        originalMesh = MeshUtility.GetMesh(transform);
    }

    public void ChangeRandom() {

        Mesh clonedMesh = MeshUtility.CloneMesh(originalMesh);

        //set submesh triangles or texture gets all screwed up
        for (int i = 0; i < clonedMesh.subMeshCount; i++) {
            clonedMesh.SetTriangles(originalMesh.GetTriangles(i), i);
        }


        List<Vector3> tempVerts = new List<Vector3>();
        clonedMesh.GetVertices(tempVerts);

        List<Vector3> relatedVerts = tempVerts.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

        float force = Random.Range(-200, 200);

        for (int i = 0; i < relatedVerts.Count; i++) {

            Vector3 currentVertexPos = relatedVerts[i];
            float falloff = GaussFalloff(distance, radius);
            Vector3 translate = (currentVertexPos * force) * falloff;
            translate.z = 0f;
            Quaternion rotation = Quaternion.Euler(translate);
            Matrix4x4 m = Matrix4x4.TRS(translate, rotation, Vector3.one);
            relatedVerts[i] = m.MultiplyPoint3x4(currentVertexPos);

            //replace all similar verts with updated random position
            for (int j = 0; j < tempVerts.Count; j++) {
                if (tempVerts[j] == currentVertexPos) {
                    tempVerts[j] = relatedVerts[i];
                }
            }
        }

        clonedMesh.SetVertices(tempVerts);
        MeshUtility.SetMesh(transform, clonedMesh);
        clonedMesh.RecalculateBounds();
        clonedMesh.RecalculateNormals();
        clonedMesh.RecalculateTangents();
    }

    void OnApplicationQuit() {
        MeshUtility.SetMesh(transform, originalMesh);
    }

    float GaussFalloff(float dist, float inRadius) {
        return Mathf.Clamp01(Mathf.Pow(360, -Mathf.Pow(dist / inRadius, 2.5f) - 0.01f));
    }
}
