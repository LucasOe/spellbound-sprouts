using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSkinnedMesh : MonoBehaviour
{
    public GameObject[] Objects;

    private void Start()
    {
        foreach (GameObject obj in Objects)
        {
            SkinnedMeshRenderer meshRenderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();

            // https://discussions.unity.com/t/quickoutline-how-can-i-change-the-shader-to-work-with-objects-with-two-materials/234528/3
            if (meshRenderer.sharedMesh.subMeshCount > 1)
            {
                meshRenderer.sharedMesh.subMeshCount = meshRenderer.sharedMesh.subMeshCount + 1;
                meshRenderer.sharedMesh.SetTriangles(meshRenderer.sharedMesh.triangles, meshRenderer.sharedMesh.subMeshCount - 1);
            }
        }
    }
}