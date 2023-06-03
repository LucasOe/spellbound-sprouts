using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Outline : MonoBehaviour
{
	[SerializeField] private Material outlineMaterial;
    [SerializeField] private Renderer[] meshRenderers;

	public void Enable() {
		foreach(Renderer meshRenderer in meshRenderers) {
			List<Material> materials = meshRenderer.materials.ToList();
			materials.Add(outlineMaterial);
			meshRenderer.materials = materials.ToArray();
		}
    }

	public void Disable() {
		foreach(Renderer meshRenderer in meshRenderers) {
			List<Material> materials = meshRenderer.materials.ToList();
			materials.RemoveAt(materials.Count - 1);
			meshRenderer.materials = materials.ToArray();
		}
	}
}