using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Information about the position in the grid
/// </summary>
public class GridSphere : MonoBehaviour {
    private int index;
    private int row;
    private int col;
    private int page;
    private Text display;

    private bool selected;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (selected)
            {
                selected = false; // deseleciona a esfera
                GetComponent<MeshRenderer>().material.color = Color.cyan;
            }
        }
    }

    public void Create(int index, int row, int col, int page, Text display)
    {
        this.index = index;
        this.row = row;
        this.col = col;
        this.page = page;
        this.display = display;

        selected = false;
    }

    private void OnMouseEnter()
    {
        if (page == 0)
            display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString();
        else
            display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString() + "x" + page.ToString();
        display.text += "\n" + gameObject.transform.position.x + "x " + gameObject.transform.position.y + "y " + gameObject.transform.position.z + "z";
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        display.text = "";
        if (selected) GetComponent<MeshRenderer>().material.color = Color.green;
        else GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Selector")
        {
            selected = true;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
