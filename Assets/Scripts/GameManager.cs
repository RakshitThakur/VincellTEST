using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int count;
    [SerializeField] GameObject[] pieces;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Solve()
    {
        count++;
        if(count >= 18)
        {
            Debug.Log("Win");
        }
        Debug.Log(count);
        
    }
    void UnSolve()
    {
        count--;
        Debug.Log(count);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Piece"))
        {
            Solve();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Piece"))
        {
            UnSolve();
        }
    }
    public void ResetLevel()
    {
        foreach(GameObject obj in pieces)
        {
            obj.GetComponent<Piece>().Restart();
        }
    }
}
