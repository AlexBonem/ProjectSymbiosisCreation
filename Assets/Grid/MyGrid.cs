using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe que cria grid 2D
/// </summary>
public class MyGrid : MonoBehaviour
{

    public List<Vector3> navegation; //lista com posições referentes a linha e coluna na grid
    public int[,] index;
    public Vector3 startPosition;
    public int colSize;
    public int rowSize;
    public float slotSizeX;
    public float slotSizeY;

    /// <summary>
    /// grid com posição inicial igual ao gameobject
    /// </summary>
    /// <param name="rowSize">número total de linhas</param>
    /// <param name="colSize">número total de colunas</param>
    /// <param name="slotSizeX">largura de cada slot da grid, para que a grid seja construída da esquerda para a direita então multiplique o slotSizeX por -1</param>
    /// <param name="slotSizeY">altura de cada slot da grid, para que a grid seja construída de cima para baixo então multiplique o slotSizeY por -1</param>
    public void Create(int rowSize, int colSize, float slotSizeX, float slotSizeY)
    {
        startPosition = gameObject.transform.localPosition;
        index = new int[colSize, rowSize];

        navegation = new List<Vector3>();
        this.rowSize = rowSize;
        this.colSize = colSize;
        this.slotSizeX = slotSizeX;
        this.slotSizeY = slotSizeY;

        for (int i = 0; i < colSize; i++)
        {
            for (int j = 0; j < rowSize; j++)
            {
                navegation.Add(new Vector3(i * slotSizeX + startPosition.x, j * slotSizeY + startPosition.y)); //i que representa a linha multiplicado pela altura e j que representa a coluna multiplicado pela largura
                index[i, j] = navegation.IndexOf(navegation[navegation.Count - 1]); // grava o index do último elemento da lista
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
    public void Create(int rowSize, int colSize, float slotSizeX, float slotSizeY, Vector3 startPosition)
    {
        this.startPosition = startPosition;
        index = new int[colSize, rowSize];

        navegation = new List<Vector3>();
        this.colSize = colSize;
        this.rowSize = rowSize;
        this.slotSizeX = slotSizeX;
        this.slotSizeY = slotSizeY;

        for (int i = 0; i < colSize; i++)
        {
            for (int j = 0; j < rowSize; j++)
            {
                navegation.Add(new Vector3(i * slotSizeX + startPosition.x, j * slotSizeY + startPosition.y)); //i que representa a linha multiplicado pela altura e j que representa a coluna multiplicado pela largura
                index[i, j] = navegation.IndexOf(navegation[navegation.Count - 1]); // grava o index do último elemento da lista
            }
        }
    }

    public void DislocateRows(int firstRowToDisalocate, float distanceY)
    {
        if (firstRowToDisalocate < 0) firstRowToDisalocate = 0;
        if (slotSizeY < 0) distanceY *= -1;

        while (firstRowToDisalocate < rowSize)
        {
            for (int i = 0; i < colSize; i++)
            {
                navegation[index[i, firstRowToDisalocate]] += new Vector3(0, distanceY);
            }
            firstRowToDisalocate++;
        }
    }

//DislocateCols, DislocateOnRow, DislocateOnCol
    public void ShowIndex()
    {
        foreach (int iteration in index)
        {
            print(navegation[iteration]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (int iteration in index)
        {
            Gizmos.DrawSphere(navegation[iteration], 0.1f);
        }
    }
}