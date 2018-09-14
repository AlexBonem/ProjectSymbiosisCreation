using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// classe que cria grid 2D
/// </summary>
public class OOOZZZBBB : MonoBehaviour {
    public List<Vector3> navegation; //lista com posições referentes a linha e coluna na grid
    public int[,] index;
    public Vector3 startPosition;
    public int colSize;
    public float slotSizeX;
    public float slotSizeY;
    public float gizmoSize = 0.4f;
    private GameObject sphereFromGizmo; // objeto usado para criar esferas nas posições onde se encontrariam os gizmos
    private Text display;



    /// <summary>
    /// a partir da primeira até a segunda linha informada todas as linhas são movidas
    /// </summary>
    /// <param name="startCol">linha inicial afetada, index começa em 0</param>
    /// <param name="numCols">número de linhas afetadas, 0 para afetar até a última linha a partir da linha inicial informada</param>
    /// <param name="distanceY">distância movida para cada linha</param>
    public void DislocateCols(int startCol, int numCols, float distanceY)
    {
        if (startCol < 0) startCol *= -1; // validation, only positive numbers
        if (startCol >= colSize) startCol = colSize - 1; // validation, max value restriction
        if (numCols < 0) numCols *= -1; // validation, only positive numbers
        if (numCols == 0 || numCols > colSize - startCol) numCols = colSize - startCol; // configuration, makes consider all of the rows from the start row when endRow equals 0
        if (slotSizeY < 0) distanceY *= -1; // configuration, makes the rows move closer to the origin instead away when slotSizeY is negative

        while (numCols > 0)
        {
            for (int i = 0; i < colSize; i++)
            {
                navegation[index[startCol, i]] += new Vector3(0, distanceY);
            }
            startCol++; //next row to move
            numCols--; //iteration up
        }
    }
}
