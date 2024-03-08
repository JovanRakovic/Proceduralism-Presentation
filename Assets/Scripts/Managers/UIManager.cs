using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    MeshGenerator generator;

    private void Awake()
    {
        generator = GameObject.FindObjectOfType<MeshGenerator>();
    }

    public void SetResolution(System.Single value)
    {
        generator.resolution = (int)value;
        generator.BuildMesh();
    }

    public void SetNoiseOffsetX(System.Single value)
    {
        generator.noiseOffset.x = value;
        generator.BuildMesh();
    }

    public void SetNoiseOffsetY(System.Single value)
    {
        generator.noiseOffset.y = value;
        generator.BuildMesh();
    }

    public void SetNoiseScale(System.Single value)
    {
        generator.baseRoughness = value;
        generator.BuildMesh();
    }

    public void SetNoiseIntensity(System.Single value)
    {
        generator.noiseIntensity = value;
        generator.BuildMesh();
    }

    public void SetNoiseMin(System.Single value)
    {
        generator.minMax.x = value;
        generator.BuildMesh();
    }

    public void SetNoiseMax(System.Single value)
    {
        generator.minMax.y = value;
        generator.BuildMesh();
    }

    public void SetNoisePersistance(System.Single value)
    {
        generator.persistance = value;
        generator.BuildMesh();
    }

    public void SetNoiseRoughness(System.Single value)
    {
        generator.roughness = value;
        generator.BuildMesh();
    }

    public void SetNoiseDepth(System.Single value)
    {
        generator.depth = (int)value;
        generator.BuildMesh();
    }

    public void SetNoisePower(System.Single value)
    {
        generator.power = value;
        generator.BuildMesh();
    }

    public void ToggleColor(bool toggle)
    {
        generator.toggleColor = toggle;
        generator.BuildMesh();
    }
}
