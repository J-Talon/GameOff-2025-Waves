using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public static MeshGenerator Instance;
    [SerializeField] Material material;
    [SerializeField] float xOrigin;
    [SerializeField] float yOrigin;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] float xScale;
    [SerializeField] float yScale;
    [SerializeField] Vector3[] vertices;
    [SerializeField] int[] triangles;
    [SerializeField] Vector2[] uv;

    private Mesh lineMesh;
    private Vector3[] lineVertices;
    public float graphOffset = 0f;

    Mesh mesh;
    MeshFilter filter;
    MeshRenderer renderer;
    bool running = false;
    [Header("EQ Parameters")]
    [SerializeField] float samplingFreq, dBgain, BW, S;
    [Range(0, 20000)]
    [SerializeField] float significantFreq;
    [SerializeField] float a0, a1, a2, b0, b1, b2;

    float fMin = 20f;
    float fMax = 20000f;
    float[] bandFreqs = { 100, 2000, 5000, 10000, 20000 };
    public float[] bandGains = new float[5];
    public float Q = 1.0f;
    void Start()
    {
        Instance = this;
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();

        // Create the line mesh for the EQ curve
        CreateLineMesh();

        // Apply initial EQ values
        ApplyFiveBandEQ();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!running) StartCoroutine(AnimateMesh());
        ApplyFiveBandEQ();
        //UpdateMesh();
    }
    private void CreateMesh()
    {
        mesh = new Mesh();
        mesh.Clear();
        filter.mesh = mesh;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        renderer.material = material;
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        filter.mesh = mesh;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    private void CreateGrid()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        triangles = new int[xSize * ySize * 6];
        int vert = 0;
        int tris = 0;

        for (int i = 0, k = 0; k <= ySize; k++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                float y = yOrigin + (k - (ySize / 2f)) * yScale;
                vertices[i] = new Vector3(xOrigin + j * xScale, y, 0);
                i++;
            }
        }
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
    }
    private void CreateLineMesh()
    {
        lineMesh = new Mesh();
        lineVertices = new Vector3[xSize + 1];

        // initial positions
        for (int i = 0; i <= xSize; i++)
        {
            float x = i * xScale;
            lineVertices[i] = new Vector3(x, graphOffset, 0);
        }

        lineMesh.vertices = lineVertices;

        // Create indices for line strip
        int[] indices = new int[xSize + 1];
        for (int i = 0; i <= xSize; i++)
            indices[i] = i;

        lineMesh.SetIndices(indices, MeshTopology.LineStrip, 0);

        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = lineMesh;

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = material; // your unlit color material
    }
    struct Coeffs
    {
        public float b0, b1, b2, a1, a2;
    }

    private Coeffs ComputePeaking(float freq, float gainDB, float Q)
    {
        float w0 = 2f * Mathf.PI * freq / samplingFreq;
        float cosw0 = Mathf.Cos(w0);
        float sinw0 = Mathf.Sin(w0);
        float A = Mathf.Pow(10f, gainDB / 40f);
        float alpha = sinw0 / (2f * Q);

        float b0 = 1f + alpha * A;
        float b1 = -2f * cosw0;
        float b2 = 1f - alpha * A;
        float a0 = 1f + alpha / A;
        float a1 = -2f * cosw0;
        float a2 = 1f - alpha / A;

        b0 /= a0;
        b1 /= a0;
        b2 /= a0;
        a1 /= a0;
        a2 /= a0;

        return new Coeffs { b0 = b0, b1 = b1, b2 = b2, a1 = a1, a2 = a2 };
    }
    private float MagnitudeAtFreq(Coeffs c, float w)
    {
        float cosw = Mathf.Cos(w);
        float sinw = Mathf.Sin(w);
        float cos2w = Mathf.Cos(2f * w);
        float sin2w = Mathf.Sin(2f * w);

        float numRe = c.b0 + c.b1 * cosw + c.b2 * cos2w;
        float numIm = -c.b1 * sinw - c.b2 * sin2w;

        float denRe = 1f + c.a1 * cosw + c.a2 * cos2w;
        float denIm = -c.a1 * sinw - c.a2 * sin2w;

        float numMag = Mathf.Sqrt(numRe * numRe + numIm * numIm);
        float denMag = Mathf.Sqrt(denRe * denRe + denIm * denIm);

        return numMag / Mathf.Max(1e-12f, denMag);
    }

    public void ApplyFiveBandEQ()
    {
        // 1. Precompute coefficients for 5 bands
        Coeffs[] coeffs = new Coeffs[5];
        for (int b = 0; b < 5; b++)
            coeffs[b] = ComputePeaking(bandFreqs[b], bandGains[b], Q);

        // 2. Sweep across X samples
        for (int i = 0; i <= xSize; i++)
        {
            float freq = (i / (float)xSize) * (samplingFreq / 2f);
            float w = 2f * Mathf.PI * freq / samplingFreq;

            float totalMag = 1f;

            for (int b = 0; b < 5; b++)
                totalMag *= MagnitudeAtFreq(coeffs[b], w);

            // Convert to dB — negative allowed
            float dB = 20f * Mathf.Log10(Mathf.Max(1e-12f, totalMag));

            lineVertices[i].y = dB * yScale + graphOffset;
        }

        lineMesh.vertices = lineVertices;
        lineMesh.RecalculateBounds();
    }
}
