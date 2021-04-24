using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class GameManager : MonoBehaviour
{
    int count;
    Bloom bloom;
    bool isWinning = false;


    [SerializeField] GameObject[] pieces;
    [SerializeField] GameObject globalLight;
    [SerializeField] Volume post;
    [SerializeField] GameObject caveDoor;
    [SerializeField] Canvas reset;
    [SerializeField] GameObject boundary;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        reset.enabled = true;
        if (post.profile.TryGet<Bloom>(out bloom))
        {
            bloom.intensity.value = 0;
        }
        boundary.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWinning)
        {
            globalLight.GetComponent<Light2D>().intensity = Mathf.Lerp(globalLight.GetComponent<Light2D>().intensity,1.8f,0.5f * Time.deltaTime);
            bloom.intensity.value = 100;
        }
    }
    void Solve()
    {
        count++;
        if(count >= 18)
        {
            isWinning = true;
            reset.enabled = false;
            Invoke("Won",2f);
            caveDoor.GetComponent<CaveDoor>().Invoke("MoveCave", 3f);
        }
    }
    void UnSolve()
    {
        count--;
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
            obj.GetComponent<Piece>().Restart(isWinning);
        }
        isWinning = false;
        globalLight.GetComponent<Light2D>().intensity = 1;
        bloom.intensity.value = 0;
    }
    void Won()
    {
        if(isWinning)
        {
            boundary.SetActive(false);
            foreach (GameObject obj in pieces)
            {
                obj.GetComponent<Piece>().Restart(isWinning);
            }
        }
        
    }
   
}
