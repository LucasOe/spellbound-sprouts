using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material outlineMaterial;
    public SkinnedMeshRenderer SkinnedMeshRenderer;

    public void Enable()
    {

        List<Material> materials = SkinnedMeshRenderer.materials.ToList();
        materials.Add(outlineMaterial);
        SkinnedMeshRenderer.materials = materials.ToArray();
    }

    public void Disable()
    {
        List<Material> materials = SkinnedMeshRenderer.materials.ToList();
        materials.RemoveAt(materials.Count - 1);
        SkinnedMeshRenderer.materials = materials.ToArray();
    }
}