using UnityEngine;
using System.Collections;

public class sortOrderStore : MonoBehaviour {

	// Use this for initialization
   [HideInInspector] public int index = 0;

   int blockCount;
   int markedBlockCount;

   public GameObject block;
   
    public float fadeSpeed; //Fade Speed for X-O
	
   public void updateIndex()
   {
         index = index - 1;
   }

   public void updateMarkedBlockCount()
   {
       markedBlockCount = markedBlockCount + 1;
   }

   void checkForWin()
   {
       trackScore[] counters = GameObject.FindObjectsOfType<trackScore>();

       bool win = true;

       foreach (trackScore c in counters)
       {
           if (!c.checkTargetAchieved())
           {
               win = false;
           }
       }

       if (win)
       {
           Debug.Log("Win");
       }

   }
    public void checkForOver()
    {
        if (blockCount == markedBlockCount)
        {
            Debug.Log("Game Over");
            checkForWin();
        }
    }

    public void reset() //Reset Game
    {
        Debug.Log("Game Reset");

        //Unmark All Blocks
        GameObject[] blockArray = GameObject.FindGameObjectsWithTag("collect");

        foreach (GameObject b in blockArray)
        {
            for (int i = 0; i < b.transform.childCount; i++)
            {
                GameObject child = b.transform.GetChild(i).gameObject;

                if (child.activeSelf)
                {
                    //child.SetActive(false);

                    //if (child.transform.parent.GetComponent<triggerWind>().marked)
                    //    child.transform.parent.GetComponent<triggerWind>().marked = false;

                    child.GetComponent<smoothFade>().fadeOut();

                }
            }

        }

        //Reset All Counters

        trackScore[] counters = GameObject.FindObjectsOfType<trackScore>();
        
        foreach(trackScore c in counters)
        {
            c.setScoreZero();
        }

        //Debug.Log(counters.Length);

        //Unwind All Ribbons
        Wind[] ribbons = GameObject.FindObjectsOfType<Wind>();
        
        foreach(Wind r in ribbons)
        {
            r.rollBack();
        }
        
    }

    GameObject[] blocks;

    void Start () {

        blocks = GameObject.FindGameObjectsWithTag(block.tag);
        foreach ( GameObject g in blocks )
        {
            blockCount += 1;
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            reset();
        }
	
	}
}
