using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeMesh : MonoBehaviour, IChangeable {

    const float range = .1f;

    Mesh originalMesh;

    void Start() {
        originalMesh = MeshUtility.GetMesh(transform);
    }

    public void ChangeRandom() {

        Mesh clonedMesh = MeshUtility.CloneMesh(originalMesh);

        //set submesh triangles or texture gets all screwed up
        for (int i = 0; i < clonedMesh.subMeshCount; i++) {
            clonedMesh.SetTriangles(originalMesh.GetTriangles(i),i);
        }

        //dont always change just in case it matters
        if (Random.Range(0, 6) < 4) {

            //get current verts
            List<Vector3> tempVerts = new List<Vector3>();
            clonedMesh.GetVertices(tempVerts);

            List<Vector3> relatedVerts = tempVerts.GroupBy(x => x)
                  .Where(g => g.Count() > 1)
                  .Select(y => y.Key)
                  .ToList();

            foreach (Vector3 vert in relatedVerts) {
                Vector3 currVert = vert;
                currVert.x += Random.Range(0f, range);
                currVert.y += Random.Range(0f, range);
                currVert.z += Random.Range(0f, range);

                //replace all similar verts with updated random position
                for (int i = 0; i < tempVerts.Count; i++) {
                    if (tempVerts[i] == vert) {
                        tempVerts[i] = currVert;
                    }
                }
            }
            clonedMesh.SetVertices(tempVerts);
        }
        MeshUtility.SetMesh(transform, clonedMesh);
    }

    void OnApplicationQuit() {
        MeshUtility.SetMesh(transform, originalMesh);
    }
}
