using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// classe que cria grid 2D
/// </summary>
public class MyGrid : MonoBehaviour
{
    public List<Vector3> navegation; //lista com posições referentes a linha e coluna na grid
    public int[,] index;
    public bool[,] isIndexActive;
    public Vector3 startPosition;
    public int colSize;
    public int rowSize;
    public float slotSizeX;
    public float slotSizeY;
    public float gizmoSize = 0.4f;
    private GameObject sphereUI; // objetos esferas que marcam as posições espaciais na grid
    private GameObject planeSelection;
    //private List<GameObject> selectedSpheresUI; // lista de esferas selecionadas
    private Text display;

    private void FixedUpdate() //destroy selection object after select the spheres
    {
        if (planeSelection)
        {
            Destroy(planeSelection);
        }
    }

    /// <summary>
    /// grid com posição inicial igual ao gameobject
    /// </summary>
    /// <param name="rowSize">número total de linhas</param>
    /// <param name="colSize">número total de colunas</param>
    /// <param name="slotSizeX">largura de cada slot da grid, para que a grid seja construída da esquerda para a direita então multiplique o slotSizeX por -1</param>
    /// <param name="slotSizeY">altura de cada slot da grid, para que a grid seja construída de cima para baixo então multiplique o slotSizeY por -1</param>
    /// <param name="display">texto de saída para método Showgrid</param>
    public void Create(int rowSize, int colSize, float slotSizeX, float slotSizeY, Text display)
    {
        startPosition = gameObject.transform.localPosition;
        index = new int[rowSize, colSize];
        isIndexActive = new bool[rowSize, colSize];
        navegation = new List<Vector3>();

        this.rowSize = rowSize;
        this.colSize = colSize;
        this.slotSizeX = slotSizeX;
        this.slotSizeY = slotSizeY;
        this.display = display;

        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                navegation.Add(new Vector3(j * slotSizeX + startPosition.x, i * slotSizeY + startPosition.y)); //i que representa a linha multiplicado pela altura e j que representa a coluna multiplicado pela largura
                index[i, j] = navegation.IndexOf(navegation[navegation.Count - 1]); // grava o index do último elemento adicionado na lista
                isIndexActive[i, j] = true; // grava o index como ativo
            }
        }
    }

    /// <summary>
    /// grid com posição inicial informada por parâmetro
    /// </summary>
    /// <param name="rowSize">número total de linhas</param>
    /// <param name="colSize">número total de colunas</param>
    /// <param name="slotSizeX">largura de cada slot da grid, para que a grid seja construída da esquerda para a direita então multiplique o slotSizeX por -1</param>
    /// <param name="slotSizeY">altura de cada slot da grid, para que a grid seja construída de cima para baixo então multiplique o slotSizeY por -1</param>
    /// <param name="startPosition">posição inicial da grid</param>
    /// <param name="display">texto de saída para método Showgrid</param>
    public void Create(int rowSize, int colSize, float slotSizeX, float slotSizeY, Vector3 startPosition, Text display)
    {
        this.startPosition = startPosition;
        index = new int[rowSize, colSize];
        isIndexActive = new bool[rowSize, colSize];
        navegation = new List<Vector3>();

        this.rowSize = rowSize;
        this.colSize = colSize;
        this.slotSizeX = slotSizeX;
        this.slotSizeY = slotSizeY;
        this.display = display;

        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                navegation.Add(new Vector3(j * slotSizeX + startPosition.x, i * slotSizeY + startPosition.y)); //i que representa a linha multiplicado pela altura e j que representa a coluna multiplicado pela largura
                index[i, j] = navegation.IndexOf(navegation[navegation.Count - 1]); // grava o index do último elemento adicionado na lista
                isIndexActive[i, j] = true; // grava o index como ativo
            }
        }
    }

    /// <summary>
    /// a partir da primeira até a segunda linha informada todas as linhas são movidas
    /// </summary>
    /// <param name="startRow">linha inicial afetada, index começa em 0</param>
    /// <param name="numRows">número de linhas afetadas, 0 para afetar até a última linha a partir da linha inicial informada</param>
    /// <param name="distanceY">distância movida para cada linha</param>
    public void DislocateRows(int startRow, int numRows, float distanceY)
    {
        if (startRow < 0) startRow *= -1; // validation, only positive numbers
        if (startRow >= rowSize) startRow = rowSize - 1; // validation, max value restriction
        if (numRows < 0) numRows *= -1; // validation, only positive numbers
        if (numRows == 0 || numRows > rowSize - startRow) numRows = rowSize - startRow; // configuration, makes consider all of the rows from the start row when numRows equals 0
        if (slotSizeY < 0) distanceY *= -1; // configuration, makes the rows move closer to the origin instead away when slotSizeY is negative

        while (numRows > 0)
        {
            for (int i = 0; i < colSize; i++)
            {
                navegation[index[startRow, i]] += new Vector3(0, distanceY);
            }
            startRow++; //next row to move
            numRows--; //iteration up
        }
    }

    /// <summary>
    /// a partir da primeira até a segunda coluna informada todas as colunas são movidas
    /// </summary>
    /// <param name="startCol">coluna inicial afetada, index começa em 0</param>
    /// <param name="numCols">número de colunas afetadas, 0 para afetar até a última coluna a partir da coluna inicial informada</param>
    /// <param name="distanceY">distância movida para cada coluna</param>
    public void DislocateCols(int startCol, int numCols, float distanceX)
    {
        if (startCol < 0) startCol *= -1; // validation, only positive numbers
        if (startCol >= colSize) startCol = colSize - 1; // validation, max value restriction
        if (numCols < 0) numCols *= -1; // validation, only positive numbers
        if (numCols == 0 || numCols > colSize - startCol) numCols = colSize - startCol; // configuration, makes consider all of the cols from the start col when numCols equals 0
        if (slotSizeY < 0) distanceX *= -1; // configuration, makes the cols move closer to the origin instead away when slotSizeX is negative

        while (numCols > 0)
        {
            for (int i = 0; i < rowSize; i++)
            {
                navegation[index[i, startCol]] += new Vector3(distanceX, 0);
            }
            startCol++; //next col to move
            numCols--; //iteration up
        }
    }



    //DislocateCols, DislocateOnRow, DislocateOnCol
    public void ShowIndex()
    {
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                sphereUI = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphereUI.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Specular"))
                {
                    color = Color.cyan
                };
                sphereUI.transform.localScale = new Vector3(gizmoSize, gizmoSize, gizmoSize);
                sphereUI.transform.position = navegation[index[i, j]];
                sphereUI.AddComponent<Rigidbody>().isKinematic = true;
                sphereUI.GetComponent<SphereCollider>().isTrigger = true;
                sphereUI.AddComponent<GridSphere>().Create(index[i, j], i, j, 0, display);
                //print(navegation[index[i, j]] + "value = " + index[i, j].ToString());
            }
        }
    }

    public void SelectIndexes(Rect rect)
    {
        planeSelection = GameObject.CreatePrimitive(PrimitiveType.Quad);
        planeSelection.GetComponent<MeshRenderer>().enabled = false; // makes the object invisible

        planeSelection.transform.localScale = new Vector3(rect.width, rect.height, 1);
        planeSelection.transform.localPosition = new Vector3(rect.x, rect.y, 0);
        planeSelection.tag = "Selector";
        //Destroy(planeSelection);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (int iteration in index)
        {
            Gizmos.DrawSphere(navegation[iteration], gizmoSize);
        }
    }
    */
}