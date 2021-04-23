using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] float gridSize = 0.5f;
    [SerializeField] LayerMask draggable;
    Vector3 mousePos;
    public bool isDragged = false;
    public bool isOverlapping = false;
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
            Collider2D mouseRay = Physics2D.OverlapBox(mousePos, new Vector2(0.5f, 1), 90f);
            if(mouseRay!=null)
            {
                if(mouseRay.gameObject.CompareTag("Piece") && mouseRay.gameObject.GetComponent<Renderer>().sortingLayerName == "NotPicked")
                {
                    isOverlapping = true;
                }

            }
        }
        if(isOverlapping)
        {
            if (Vector3.Distance(transform.position, startPosition) > 0.05f)
            {
                GetComponent<Renderer>().sortingLayerName = "Starting";
                transform.position = Vector3.Lerp(transform.position, startPosition, 2f * Time.deltaTime);
            }
            else
            {
                transform.position = startPosition;
                
                isOverlapping = false;
            }
        }
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDragged = true;
            isOverlapping = false;
            GetComponent<Renderer>().sortingLayerName = "Picked";
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnMouseUp()
    {
        isDragged = false;
        GetComponent<Renderer>().sortingLayerName = "NotPicked";
        GetComponent<Collider2D>().enabled = true;
    }
    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         if(collision.gameObject.CompareTag("Piece") && collision.gameObject.GetComponent<Renderer>().sortingLayerName == "NotPicked")
         {
             isOverlapping = true;
         }
     }*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(mousePos, 0.6f);
    }

}
