using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{

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

    Mesh mesh;
    MeshFilter filter;
    MeshRenderer renderer;
    bool running = false;
    [Header("EQ Parameters")]
    [SerializeField] float samplingFreq, dBgain, Q, BW, S;
    [Range(0, 20000)]
    [SerializeField] float significantFreq;
    [SerializeField] float a0, a1, a2, b0, b1, b2;

    void Start()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        CreateGrid();
        CreateMesh();
        ApplyBPF();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!running) StartCoroutine(AnimateMesh());
        ApplyBPF();
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
                vertices[i] = new Vector3(xOrigin + j * xScale, yOrigin + k * yScale, 0);
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
    private IEnumerator AnimateMesh()
    {
        running = true;
        for (int i = 0; i < xSize; i++)
        {
            //vertices[i].y += 2 * yScale;
            vertices[i + xSize + 1].y += 2 * yScale;

            yield return new WaitForSeconds(0.1f);
            UpdateMesh();

            //vertices[i].y -= 2 * yScale;
            vertices[i + xSize + 1].y -= 2 * yScale;
            yield return new WaitForSeconds(0.5f);
            UpdateMesh();

        }
        running = false;
    }

    public void ApplyBPF()
    {
        float w0 = 2f * Mathf.PI * significantFreq / samplingFreq;
        float cosw0 = Mathf.Cos(w0);
        float sinw0 = Mathf.Sin(w0);
        float cos2w0 = Mathf.Cos(2 * w0);
        float sin2w0 = Mathf.Sin(2 * w0);

        float A = Mathf.Pow(10, dBgain / 40);


        float alpha = sinw0 / (2 * Q);

        b0 = sinw0 / 2;
        b1 = 0;
        b2 = -sinw0 / 2;
        a0 = 1 + alpha;
        a1 = -2 * cosw0;
        a2 = 1 - alpha;

        for (int i = 0; i <= xSize; i++)
        {
            float freq = (i / (float)(xSize - 1)) * (samplingFreq / 2f);
            float w = 2f * Mathf.PI * freq / samplingFreq;

            float cosw = Mathf.Cos(w);
            float sinw = Mathf.Sin(w);
            float cos2w = Mathf.Cos(2f * w);
            float sin2w = Mathf.Sin(2f * w);

            float numRe = b0 + b1 * cosw + b2 * cos2w;
            float numIm = -b1 * sinw - b2 * sin2w;

            float denRe = 1f + a1 * cosw + a2 * cos2w;
            float denIm = -a1 * sinw - a2 * sin2w;

            float numMag = Mathf.Sqrt(numRe * numRe + numIm * numIm);
            float denMag = Mathf.Sqrt(denRe * denRe + denIm * denIm);

            float mag = numMag / Mathf.Max(1e-12f, denMag);
            float dB = 20f * Mathf.Log10(Mathf.Max(1e-12f, mag));

            vertices[i + xSize + 1].y = dB * yScale;
            //vertices[i].y = 0.95f * dB;
        }
        UpdateMesh();
    }
    private void ApplyLPF()
    {
        float w0 = 2f * Mathf.PI * significantFreq / samplingFreq;
        float cosw0 = Mathf.Cos(w0);
        float sinw0 = Mathf.Sin(w0);
        float cos2w0 = Mathf.Cos(2 * w0);
        float sin2w0 = Mathf.Sin(2 * w0);

        float A = Mathf.Pow(10, dBgain / 40);


        float alpha = sinw0 / (2 * Q);

        b0 = (1 - cosw0) / 2;
        b1 = 1 - cosw0;
        b2 = (1 - cosw0) / 2;
        a0 = 1 + alpha;
        a1 = -2 * cosw0;
        a2 = 1 - alpha;

        for (int i = 0; i <= xSize; i++)
        {
            float freq = (i / (float)(xSize - 1)) * (samplingFreq / 2f);
            float w = 2f * Mathf.PI * freq / samplingFreq;

            float cosw = Mathf.Cos(w);
            float sinw = Mathf.Sin(w);
            float cos2w = Mathf.Cos(2f * w);
            float sin2w = Mathf.Sin(2f * w);

            float numRe = b0 + b1 * cosw + b2 * cos2w;
            float numIm = -b1 * sinw - b2 * sin2w;

            float denRe = 1f + a1 * cosw + a2 * cos2w;
            float denIm = -a1 * sinw - a2 * sin2w;

            float numMag = Mathf.Sqrt(numRe * numRe + numIm * numIm);
            float denMag = Mathf.Sqrt(denRe * denRe + denIm * denIm);

            float mag = numMag / Mathf.Max(1e-12f, denMag);
            float dB = 20f * Mathf.Log10(Mathf.Max(1e-12f, mag));

            vertices[i + xSize + 1].y = dB * yScale * A;

        }
        UpdateMesh();
    }
    private void ApplyHPF()
    {
        float w0 = 2f * Mathf.PI * significantFreq / samplingFreq;
        float cosw0 = Mathf.Cos(w0);
        float sinw0 = Mathf.Sin(w0);
        float cos2w0 = Mathf.Cos(2 * w0);
        float sin2w0 = Mathf.Sin(2 * w0);

        float A = Mathf.Pow(10, dBgain / 40);


        float alpha = sinw0 / (2 * Q);

        b0 = (1 + cosw0) / 2;
        b1 = -1 - cosw0;
        b2 = (1 + cosw0) / 2;
        a0 = 1 + alpha;
        a1 = -2 * cosw0;
        a2 = 1 - alpha;

        for (int i = 0; i <= xSize; i++)
        {
            float freq = (i / (float)(xSize - 1)) * (samplingFreq / 2f);
            float w = 2f * Mathf.PI * freq / samplingFreq;

            float cosw = Mathf.Cos(w);
            float sinw = Mathf.Sin(w);
            float cos2w = Mathf.Cos(2f * w);
            float sin2w = Mathf.Sin(2f * w);

            float numRe = b0 + b1 * cosw + b2 * cos2w;
            float numIm = -b1 * sinw - b2 * sin2w;

            float denRe = 1f + a1 * cosw + a2 * cos2w;
            float denIm = -a1 * sinw - a2 * sin2w;

            float numMag = Mathf.Sqrt(numRe * numRe + numIm * numIm);
            float denMag = Mathf.Sqrt(denRe * denRe + denIm * denIm);

            float mag = numMag / Mathf.Max(1e-12f, denMag);
            float dB = 20f * Mathf.Log10(Mathf.Max(1e-12f, mag));

            vertices[i + xSize + 1].y = dB * yScale * A;

        }
        UpdateMesh();
    }
    /*
        private void OnDrawGizmos()
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }
        }*/
}
