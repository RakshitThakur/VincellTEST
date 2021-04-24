using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] float gridSize = 0.5f;
    Vector3 mousePos;
    public bool isDragged = false;
    public bool isOverlapping = false;
    public bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isDragged)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(Mathf.Round(mousePos.x / gridSize) * gridSize, Mathf.Round(mousePos.y / gridSize) * gridSize);
        }
        if(isOverlapping)
        {
            if (Vector3.Distance(transform.position, startPosition) > 0.01f)
            {
                if(!completed)
                {
                    GetComponent<Renderer>().sortingLayerName = "Starting";
                }
                transform.position = Vector3.Lerp(transform.position, startPosition, 2f * Time.deltaTime);
            }
            else
            {
                transform.position = startPosition;
                isOverlapping = false;
            }
        }
    }
    public void Restart(bool completed)
    {
        isOverlapping = true;
        this.completed = completed;
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDragged = true;
            isOverlapping = false;
            GetComponent<Renderer>().sortingLayerName = "Picked";
            GetComponent<Renderer>().sortingOrder = 99;
        }
    }
    private void OnMouseUp()
    {
        isDragged = false;
        GetComponent<Renderer>().sortingLayerName = "NotPicked";
        GetComponent<Renderer>().sortingOrder = 0;
    }
     private void OnTriggerStay2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Boundary") && isDragged == false)
        {
            isOverlapping = true;
        }
        if (collision.gameObject.CompareTag("Piece") && GetComponent<Renderer>().sortingLayerName == "NotPicked")
        {
            collision.gameObject.GetComponent<Piece>().isOverlapping = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Piece") && GetComponent<Renderer>().sortingLayerName == "NotPicked")
        {
            if(collision.gameObject.GetComponent<Piece>().isDragged == true)
            {
                collision.gameObject.GetComponent<Piece>().isOverlapping = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Piece") && GetComponent<Renderer>().sortingLayerName == "NotPicked")
        {
            collision.gameObject.GetComponent<Piece>().isOverlapping = true;
        }
        if (collision.gameObject.CompareTag("Boundary") && isDragged == false)
        {
            isOverlapping = true;
        }
    }
    public void Done()
    {
        completed = true;
    }
    
}
