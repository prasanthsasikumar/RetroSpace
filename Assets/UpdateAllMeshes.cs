using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class UpdateAllMeshes : MonoBehaviour
{
    EffectMesh[] effectMeshes;
    public Material wallMaterial, LargestWallMaterial, CeilingMaterial;

    void Start()
    {
        //get all the effect meshes in the scene
        effectMeshes = FindObjectsOfType<EffectMesh>();
        //loop through all the effect meshes
        foreach (var effectMesh in effectMeshes)
        {
            //override the material of the wall face effect mesh
            effectMesh.OverrideEffectMaterial(wallMaterial, LabelFilter.Included(MRUKAnchor.SceneLabels.WALL_FACE));
            effectMesh.OverrideEffectMaterial(wallMaterial, LabelFilter.Included(MRUKAnchor.SceneLabels.INVISIBLE_WALL_FACE));
            effectMesh.OverrideEffectMaterial(CeilingMaterial, LabelFilter.Included(MRUKAnchor.SceneLabels.CEILING));
        }
        //Wait for 2 seconds before changing the material of the largest wall face effect mesh
        StartCoroutine(CallChangeLargestWallMaterialWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private IEnumerator CallChangeLargestWallMaterialWithDelay()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeLargestWallMaterial());
}
    private IEnumerator ChangeLargestWallMaterial()
    {
        // Get all MeshRenderer components in the scene
        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();

        // Create a list to store objects using the wallMaterial
        List<GameObject> objectsWithWallMaterial = new List<GameObject>();

        // Iterate through all MeshRenderers and check if they use the wallMaterial
        foreach (var meshRenderer in meshRenderers)
        {
            foreach (var material in meshRenderer.sharedMaterials)
            {
                if (material == wallMaterial)
                {
                    objectsWithWallMaterial.Add(meshRenderer.gameObject);
                    break;
                }
            }
        }

        //find the largest mesh in the objectsWithWallMaterial
        GameObject largestWallObject = null;
        float largestWallArea = 0;
        foreach (var obj in objectsWithWallMaterial)
        {
            var meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                var mesh = meshFilter.sharedMesh;
                var bounds = mesh.bounds;
                var area = bounds.size.x * bounds.size.y;
                if (area > largestWallArea)
                {
                    largestWallArea = area;
                    largestWallObject = obj;
                }
            }
        }

        // Change the material of the largest wall object
        if (largestWallObject != null)
        {
            var meshRenderer = largestWallObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.sharedMaterial = LargestWallMaterial;
            }
        }

        // Output the names of the objects that use the wallMaterial
        foreach (var obj in objectsWithWallMaterial)
        {
            Debug.Log("Object with wallMaterial: " + obj.name);
            obj.GetComponent<MeshRenderer>().sharedMaterial = LargestWallMaterial;
        }
        yield return null;
    }

    
}
