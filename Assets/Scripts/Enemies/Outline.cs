using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material outlineMaterial;
    public Renderer Renderer;

    public void Enable()
    {
        List<Material> materials = Renderer.materials.ToList();
        materials.Add(outlineMaterial);
        Renderer.materials = materials.ToArray();
    }

    public void Disable()
    {
        List<Material> materials = Renderer.materials.ToList();
        materials.RemoveAt(materials.Count - 1);
        Renderer.materials = materials.ToArray();
    }
}