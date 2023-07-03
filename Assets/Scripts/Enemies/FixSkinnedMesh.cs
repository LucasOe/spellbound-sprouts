using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSkinnedMesh : MonoBehaviour
{
    public Mesh[] Meshes;

    private void Start()
    {
        foreach (Mesh mesh in Meshes)
        {
            // https://discussions.unity.com/t/quickoutline-how-can-i-change-the-shader-to-work-with-objects-with-two-materials/234528/3
            if (mesh.subMeshCount > 1)
            {
                mesh.subMeshCount = mesh.subMeshCount + 1;
                mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
            }
        }
    }
}