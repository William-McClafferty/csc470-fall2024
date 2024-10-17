using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public Renderer cubeRenderer;
    public enum ColorState {Purple, Green, Red}
    public ColorState currentState = ColorState.Purple;
    public Color purpleColor = Color.magenta;
    public Color greenColor = Color.green;
    public Color redColor = Color.red;
    public int xIndex = -1;
    public int yIndex = -1;
    public GameManager gameManager;  
    private Vector3 initialPosition;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        initialPosition = transform.position;
        UpdateColorAndHeight(); 
    }

    public void CycleState()
    {
        if (currentState == ColorState.Purple)
        {
            currentState = ColorState.Green;
        }
        else if (currentState == ColorState.Green)
        {
            currentState = ColorState.Red;
        }
        else if (currentState == ColorState.Red)
        {
            currentState = ColorState.Purple;
        }
        UpdateColorAndHeight();
    }

    public void UpdateColorAndHeight()
    {
        if (currentState == ColorState.Purple)
        {
            cubeRenderer.material.color = purpleColor;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (currentState == ColorState.Green)
        {
            cubeRenderer.material.color = greenColor;
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        else if (currentState == ColorState.Red)
        {
            cubeRenderer.material.color = redColor;
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }
    }

    void Update()
    {
        if (transform.position.y >= 3)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            currentState = ColorState.Purple;
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            currentState = ColorState.Purple;
        }

        UpdateColorAndHeight();
    }

    void OnMouseDown()
    {
        CycleState();
        AffectChildren();
    }

    private void AffectChildren()
    {
        foreach (Transform child in transform)
        {
            CellScript childScript = child.GetComponent<CellScript>();
            if (childScript != null)
            {
                childScript.CycleState();
            }
        }
    }
}
