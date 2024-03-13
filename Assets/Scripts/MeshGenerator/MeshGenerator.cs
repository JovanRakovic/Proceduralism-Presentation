using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [Range(1,255)]
    public int resolution = 64;
    public float scale = 1;
    [Header("Noise Settings")]
    public Vector2 noiseOffset = Vector2.zero;
    public float baseRoughness = 1f;
    public float noiseIntensity = 1f;
    public Vector2 minMax = Vector2.zero;
    public float persistance = .3f;
    public float roughness = 2f;
    public float power = 1f;
    [Range(1,10)]
    public int depth;
    [Header("Material Settings")]
    public bool toggleColor = false;


    private Mesh m;
    private Material mat;

    void Start()
    {
        m = new Mesh();
        mat = GetComponent<MeshRenderer>().sharedMaterial;
        GetComponent<MeshFilter>().sharedMesh = m;

        BuildMesh();
    }

    public void BuildMesh()
    {
        int vertCount = resolution+1;
        Vector3[] verts = new Vector3[vertCount*vertCount];
        int[] tris = new int[resolution * resolution * 6];
        Color[] colors = new Color[verts.Length];

        for(int i = 0; i < vertCount; i++)
        {
            for(int j = 0; j < vertCount; j++)
            {
                Vector2 pos = new Vector2(-i/(float)resolution+.5f, j/(float)resolution-.5f);

                float y = 0;

                float noise = 0;
                float currentPersistance = 1;
                float persistance = this.persistance;
                for(int k = 0; k < depth; k++)
                {
                    float roughness = MathF.Pow(this.roughness, k) * baseRoughness;

                    noise += Mathf.Pow(currentPersistance * Mathf.PerlinNoise((pos.x + noiseOffset.x) * roughness, (pos.y + noiseOffset.y) * roughness), power);
                    currentPersistance *= persistance;
                }
                noise = Mathf.Clamp(noise, minMax.x, minMax.y);

                if(noiseIntensity > 0)
                    y =  noiseIntensity * (noise - .5f);

                colors[i*vertCount + j] = new Color(noise,noise,noise);

                verts[i*vertCount + j] = new Vector3(pos.x, y, pos.y) * scale;
            }
        }

        int vert = 0;
        int tri = 0;
        for(int i = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++)
            {
                tris[tri] = vert;
                tris[tri + 1] = vert + resolution + 1;
                tris[tri + 2] = vert + 1;
                tris[tri + 3] = vert + 1;
                tris[tri + 4] = vert + resolution + 1;
                tris[tri + 5] = vert + resolution + 2;

                vert++;
                tri += 6;
            }
            vert++;
        }

        m.Clear();

        m.SetVertices(verts);
        m.SetTriangles(tris, 0);
        m.SetColors(colors);

        m.RecalculateNormals();

        mat.SetVector("_minMax", minMax);
        mat.SetFloat("_intensity", noiseIntensity);
        mat.SetFloat("_toggleColor", toggleColor? 1f : 0f);
    }
}