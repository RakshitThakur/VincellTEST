using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int count;
    [SerializeField] GameObject[] pieces;
    [SerializeField] Canvas won;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        won.enabled = false;
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
            Invoke("WON", 2f);
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
    void WON()
    {
        won.enabled = true;
    }
}
