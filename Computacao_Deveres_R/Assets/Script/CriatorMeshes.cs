using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CriatorMeshes : MonoBehaviour
{
    #region Variaveis
    Mesh mesh;

    [SerializeField] int gridSize;

    [SerializeField] int cellSize = 1;

    Vector3 gridOffset;

    int[] triangles;

    Vector3[] vertices;

    float time = 0.0f;

    [Range(0, 1)]
    [SerializeField] float height;
    #endregion

    //Para que um pedaço de mesh/malha seja criado, você precisa de ter 3 pontos, um Vector3/Vetor de 3 pontos e numerar qual é a ordem de renderinização da mesh

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        MakeProcGrid();
        CreateMesh();
    }

    void Update()
    {
        int v = 0;

        time += Time.deltaTime;

        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                float h = Mathf.Sin(x / (float)gridSize * 2 * Mathf.PI + time) * height;

                vertices[v] = new Vector3(x * cellSize, h, y * cellSize);

                v++;
            }
        }

        CreateMesh();
    }

    void MakeProcGrid()
    {
        CreateVertices();
        CreateTriangles();
    }

    void CreateVertices()
    {
        //Aqui será creiado os vertices, vamos definir um vector3 com uma única conta que será o valor de todos, depois criar um for que fará a posição dos vertices

        int v = 0;

        vertices = new Vector3[((gridSize + 1) * (gridSize + 1))];

        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3(x * cellSize, 0, y * cellSize);

                v++;
            }
        }
    }

    void CreateTriangles()
    {
        int v = 0, t = 0;
        triangles = new int[6 * gridSize * gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = v + 1;
                triangles[t + 2] = v + (gridSize + 1);
                triangles[t + 4] = v + 1;
                triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
    }
}
