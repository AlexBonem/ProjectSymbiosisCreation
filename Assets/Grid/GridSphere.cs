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

    private MeshRenderer meshRenderer;
    private bool selected;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (selected)
            {
                selected = false; // deseleciona a esfera
                meshRenderer.material.color = Color.cyan;
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
        if (!Input.GetMouseButton(0)) // quando o botão esquerdo do mouse não estiver sendo apertado
        { 
            if (page == 0)
                display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString();
            else
                display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString() + "x" + page.ToString();
            display.text += "\n" + gameObject.transform.position.x + "x " + gameObject.transform.position.y + "y " + gameObject.transform.position.z + "z";
            meshRenderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0)) // quando o botão esquerdo do mouse não estiver sendo apertado
        { 
            display.text = "";
            if (selected) meshRenderer.material.color = Color.green;
            else meshRenderer.material.color = Color.cyan;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Selector")
        {
            if (!Input.GetKey(KeyCode.LeftControl)) // se Control não estiver sendo apertado
            {
                selected = true;
                meshRenderer.material.color = Color.green;
            }
            else 
            {
                if (Input.GetKey(KeyCode.LeftShift)) // inverte a seleção
                {
                    selected = false;
                    meshRenderer.material.color = Color.cyan;
                }
                else
                {
                    if (selected)
                    {
                        selected = false;
                        meshRenderer.material.color = Color.cyan;
                    }
                    else
                    {
                        selected = true;
                        meshRenderer.material.color = Color.green;
                    }
                }
            }
        }

        SelectionHighLight(other);
    }

    private void OnTriggerStay(Collider other) // altera a cor das esferas de acordo com o shift e control
    {
        SelectionHighLight(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SelectorHighLight")
        {
            if (selected) meshRenderer.material.color = Color.green;
            else meshRenderer.material.color = Color.cyan;
        }
    }

    //refactoration

    private void SelectionHighLight(Collider other)
    {
        if (other.tag == "SelectorHighLight")
        {
            if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
                if (selected) meshRenderer.material.color = Color.cyan;
                else meshRenderer.material.color = new Color(0.25f, 1, 0.5f);
            else
                if (Input.GetKey(KeyCode.LeftControl)) meshRenderer.material.color = Color.cyan;
                else
                    if (meshRenderer.material.color != new Color(0.25f, 1, 0.5f)) meshRenderer.material.color = new Color(0.25f, 1, 0.5f);
        }
    }
}
