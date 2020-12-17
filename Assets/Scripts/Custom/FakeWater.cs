using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FakeWater : MonoBehaviour {
    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    [SerializeField] private float waveAmplitude = 1;
    [SerializeField] private float waveFrequency = 1;
    [SerializeField] private float waveStrength = 1;
    [SerializeField] private int topVertices = 10;
    private float phase = 1;
    private float currentFrequency;
    [SerializeField] private Material material;
    private void Start () {
        currentFrequency = waveFrequency;
        meshRenderer = gameObject.AddComponent<MeshRenderer> ();
        meshFilter = gameObject.AddComponent<MeshFilter> ();

        var meshInfo = GenerateMeshInfo ();

        meshFilter.mesh = CreateMesh (meshInfo.vertices, meshInfo.uv, meshInfo.triangles);
        meshRenderer.material = material;
    }
    private void Update () {
        if (currentFrequency != waveFrequency) { CalculateNewFrequency (); }
        Wave ();
    }
    private void CalculateNewFrequency () {
        float curr = (Time.time * currentFrequency + phase) % (2.0f * Mathf.PI);
        float next = (Time.time * waveFrequency) % (2.0f * Mathf.PI);
        phase = curr - next;
        currentFrequency = waveFrequency;
    }
    private float CalculateSineWaveValue (float time) {
        var sinValue = Mathf.Sin (time * Time.deltaTime * currentFrequency + phase) * waveAmplitude;
        Debug.Log ($"sin value: {sinValue}");
        return sinValue;
    }
    private void Wave () {
        var meshVetice = meshFilter.mesh.vertices;
        for (int i = 0; i < meshVetice.Length; i++) {
            if (meshVetice[i].y > 0) {
                meshVetice[i].y = Mathf.Abs (waveStrength * CalculateSineWaveValue (i));
                Debug.Log ($"{i}: {meshVetice[i]}");
            }
        }
        meshFilter.mesh.vertices = meshVetice;
        meshFilter.mesh.RecalculateNormals ();
    }
    private dynamic GenerateMeshInfo () {
        var vertices = new Vector3[topVertices + 2];
        var uv = new Vector2[topVertices + 2];
        var triangles = new int[topVertices * 3];

        vertices[0] = new Vector3 (0, 0);
        vertices[1] = new Vector3 (0, 1);
        vertices[topVertices] = new Vector3 (1, 1);
        vertices[topVertices + 1] = new Vector3 (1, 0);

        uv[0] = new Vector2 (0, 0);
        uv[1] = new Vector2 (0, 1);
        uv[topVertices] = new Vector2 (1, 1);
        uv[topVertices + 1] = new Vector2 (1, 0);

        for (int i = 2; i < topVertices; i++) {
            vertices[i] = new Vector3 ((1f / topVertices) * i, 1);
            uv[i] = new Vector2 ((1f / topVertices) * i, 1);
        }

        for (int i = 1, triIndex = 0; i <= topVertices; i++, triIndex += 3) {
            triangles[triIndex] = 0;
            triangles[triIndex + 1] = i;
            triangles[triIndex + 2] = i + 1;
        }
        return new { vertices, uv, triangles };
    }
    private Mesh CreateMesh (Vector3[] vertices, Vector2[] uv, int[] triangles) {
        var mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        return mesh;
    }
}