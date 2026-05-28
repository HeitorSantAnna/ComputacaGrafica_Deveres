using UnityEngine;
using UnityEngine.InputSystem;

public class CriatorMeshes : MonoBehaviour
{
    Mesh mesh;

    Vector3[] zone;

    [SerializeField] int gridSize;

    [SerializeField] int cellSize = 1;

    [SerializeField] Vector3 gridOffset;

    int[] triangles;

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

    public void SetPosition(InputAction.CallbackContext value)
    {
        if(!value.performed)
        {
            return;
        }
    }

    void MakeProcGrid()
    {
        zone = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];
        int v = 0, t = 0;
        float vertexOffset = cellSize * 0.5f;

        for(int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);
                zone[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                zone[v+1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                zone[v+2] = new Vector3( vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                zone[v+3] = new Vector3( vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                triangles[t + 0] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;
                v += 4;
                t += 6;
            }
        }
    }

    //Para que o código fique com a animação do professor é necessário uma função de remodele o calculo da posição dos triangulos e no Update vai chamar essa função e fazer a alteração da posição dos triangulos

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = zone;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
