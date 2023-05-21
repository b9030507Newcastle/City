using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Randon = UnityEngine.Random;

public class Terrian : MonoBehaviour
{
    public float width = 0.1f;
    public int N = 10;

    public float noiseParam = 0.06f;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    List<Vector3> verts;
    List<int> indices;

    private void Awake()
    {
    }

    private void Start()
    {
        verts = new List<Vector3>();
        indices = new List<int>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        Generate();
    }

    public void Generate()
    {
        ClearMeshData();
        AddMeshData();
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void ClearMeshData()
    {
        verts.Clear();
        indices.Clear();
    }

    void AddMeshData()
    {
        for (int z = 0; z < N; z++)
        {
            for (int x = 0; x < N; x++)
            {
                float y = Mathf.PerlinNoise(Random.Range(1,1000) * noiseParam,Random.Range(1,1000) * noiseParam) * Random.Range(1,3);
                Vector3 p = new Vector3(x, y, z) * width;
                verts.Add(p);
            }
        }

        for (int z = 0; z < N-1; z++)
        {
            for (int x = 0; x < N-1; x++)
            {
                int index = z*N+x;              //left lower
                int index1 = (z+1)*N+x;   //up
                int index2 = (z+1)*N+x+1;   //upper right
                int index3 = z*N+x+1;         //right
                
                indices.Add(index); indices.Add(index1); indices.Add(index2);
                indices.Add(index); indices.Add(index2); indices.Add(index3);
            }
        }
    }

    // private float lastUpdateTime = 0;
    // private void Update()
    // {
    //     if (Time.time >= lastUpdateTime + 0.1f)
    //     {
    //      Generate();
    //      lastUpdateTime = Time.time;
    //     }
    // }
}
