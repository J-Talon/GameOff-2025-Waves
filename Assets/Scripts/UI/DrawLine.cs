using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] Vector3[] newVertices;
    [SerializeField] Vector2[] newUV;
    [SerializeField] int[] newTriangles;
    Mesh mesh;
    [SerializeField]
    Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh
        {
            vertices = newVertices,
            uv = newUV,
            triangles = newTriangles
        };
        // After updating the Mesh data, recalculate the normals. 
        // If the Mesh uses shaders with normal maps, also call RecalculateTangents for proper lighting.
        // mesh.RecalculateNormals();

        // This assignment is temporary and will reset to the initial Mesh when exiting Play mode.
        GetComponent<MeshFilter>().mesh = mesh;
        //GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        material.SetPass(0);
    }

    // Update is called once per frame)
    void Update()
    {
        //RenderParams rp = new RenderParams(material);
        //for (int i = 0; i < 10; ++i)
        //   Graphics.RenderMesh(rp, mesh, 0, Matrix4x4.Translate(new Vector3(-4.5f + i, 0.0f, 5.0f)));
    }
}
