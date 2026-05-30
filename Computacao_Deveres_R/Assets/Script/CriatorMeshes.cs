using UnityEngine;

public class CriatorMeshes : MonoBehaviour
{
    #region Variaveis
    Mesh mesh;

    [SerializeField] int gridSize;

    [SerializeField] int cellSize = 1;

    public float Offsetx, Offsety;

    int[] triangles;

    Vector3[] vertices;

    [SerializeField] float height = 1;
    #endregion

    //Para que um pedaço de mesh/malha seja criado, você precisa de ter 3 pontos, um Vector3/Vetor de 3 pontos e numerar qual é a ordem de renderinização da mesh

    //Acho que entendi o meu erro, estou mandando aplicar a textura do normal map, mas não estou mandano os vertices se moverem, acho que como foi gerado via código  os tries, eles dependam de comandos escritos, valeu chatGPT

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
        CreateNormal();
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

    void CreateNormal()
    {
        int v = 0;

        Texture2D hei = new Texture2D(gridSize, gridSize);

        for(int i = 0; i <= gridSize; i++)
        {
            for(int j = 0; j <= gridSize; j++)
            {
                float xCoord = i / (float)gridSize * height + Offsetx;
                float yCoord = j / (float)gridSize * height + Offsety;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                hei.SetPixel(i, j, new Color(sample, sample, sample));
            }
        }

        hei.Apply();

        for(int x = 0; x <= gridSize; x++)
        {
            for(int y = 0; y <= gridSize; y++)
            {
                float u = x / (float)gridSize;
                float t = y / (float)gridSize;

                Color pixel = hei.GetPixelBilinear(u, t);

                float h = pixel.grayscale * height;

                vertices[v] = new Vector3(x * cellSize, h, y * cellSize);

                v++;
            }
        }

        CreateMesh();
    }
}
