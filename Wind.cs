using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

    tk2dTiledSprite sprite;

    //public float targetHeight;
    public float Breadth;
    string dimension;

    public string color;
    public float windSpeed;

    float actualWindSpeed;

	void Start () {

	}

    void stretch(float d)
    {

        Vector2 di = new Vector2();

        if (dimension == "x")
        {
             di  = new Vector2(d, sprite.dimensions.y);
             
        }

        if (dimension == "y")
        {
             di  = new Vector2(sprite.dimensions.x, d);

        }

        sprite.dimensions = di;

        markBlocks();

    }

    void markBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("collect");

        //Debug.Log(blocks.Length);

        foreach (GameObject g in blocks)
        {

            bool check = transform.renderer.bounds.Intersects(g.renderer.bounds);
            //bool check = transform.renderer.bounds.Contains(g.renderer.bounds.center);


            if (check)
            {
                Debug.Log(check);

                if (g.GetComponent<triggerWind>().color == transform.tag)
                {
                    //Correct
                    if (!g.transform.GetChild(1).gameObject.activeSelf)
                    {

                        g.transform.GetChild(1).gameObject.SetActive(true);
                        g.transform.GetChild(1).GetComponent<smoothFade>().fadeIn();

                        GameObject[] colorObj = GameObject.FindGameObjectsWithTag(g.GetComponent<triggerWind>().color);

                        foreach (GameObject G in colorObj)
                        {
                            if (G.name == "oLabel")
                            {
                                G.GetComponent<trackScore>().updateScore();

                                Vector3 amount = new Vector3(0.5f, 0.5f, 0.5f);
                                float time = GameObject.Find("gameController").GetComponent<anims>().counterPunchSpeed;
                                iTween.PunchScale(G, amount, time);

                            }
                        }

                        g.GetComponent<triggerWind>().marked = true;
                        GameObject.Find("sorter").GetComponent<sortOrderStore>().updateMarkedBlockCount();
                    }
     
                }
                else
                {
                    //Incorrect

                    if (!g.transform.GetChild(0).gameObject.activeSelf)
                    {
 
                        g.transform.GetChild(0).gameObject.SetActive(true);
                        g.transform.GetChild(0).GetComponent<smoothFade>().fadeIn();

                        GameObject[] colorObj = GameObject.FindGameObjectsWithTag(g.GetComponent<triggerWind>().color);

                        foreach (GameObject G in colorObj)
                        {
                            if (G.name == "xLabel")
                            {
                                G.GetComponent<trackScore>().updateScore();

                                Vector3 amount = new Vector3(0.5f, 0.5f, 0.5f);
                                float time = GameObject.Find("gameController").GetComponent<anims>().counterPunchSpeed;
                                iTween.PunchScale(G, amount, time);

                            }
                        }

                        g.GetComponent<triggerWind>().marked = true;
                        GameObject.Find("sorter").GetComponent<sortOrderStore>().updateMarkedBlockCount();

                    }

                }

            }
        }
    }

    void finish()
    {

        GameObject.Find("sorter").GetComponent<sortOrderStore>().checkForOver();
 
    }

    void unroll(float startValue , float targetSize , float timeFactor)
    { 
        actualWindSpeed = windSpeed * timeFactor; //Store this for Unwind Speeds
        iTween.ValueTo(gameObject, iTween.Hash(iT.ValueTo.time, windSpeed*timeFactor, iT.ValueTo.from, startValue, iT.ValueTo.to, targetSize, iT.ValueTo.onupdate, "stretch"  , iT.ValueTo.oncomplete , "finish"));

    }

    void destroyRibbon()
    {
        Destroy(gameObject);
    }

    void shrink(float d)
    {
        if (dimension == "x")
        {
            sprite.dimensions = new Vector2(d ,sprite.dimensions.y);

        }

        else
        {
            sprite.dimensions = new Vector2(sprite.dimensions.x, d);
        } 
    }

    public void rollBack()
    {
        //if dimension.
        float startValue = 0f;

        //Debug.Log("rollback");

        if (dimension == "x")
        {
            startValue = sprite.dimensions.x;
        }

        if (dimension == "y")
        {
            startValue = sprite.dimensions.y;

        }

        iTween.ValueTo(gameObject, iTween.Hash(iT.ValueTo.time, actualWindSpeed, iT.ValueTo.from, startValue, iT.ValueTo.to, 0f, iT.ValueTo.onupdate, "shrink",iT.ValueTo.oncomplete , "destroyRibbon"));


    }


    public void initialise(tk2dBaseSprite.Anchor targetAnchor ,Vector3 targetPos, float targetSize , float time)
    {
        sprite = transform.GetComponent<tk2dTiledSprite>();
        sprite.anchor = targetAnchor;

        GameObject.Find("sorter").GetComponent<sortOrderStore>().updateIndex();
        sprite.SortingOrder = GameObject.Find("sorter").GetComponent<sortOrderStore>().index;

        transform.position = targetPos;

        if (sprite.anchor == tk2dBaseSprite.Anchor.LowerCenter || sprite.anchor == tk2dBaseSprite.Anchor.UpperCenter)
        {
            Vector2 di = new Vector2(Breadth, sprite.dimensions.y);
            sprite.dimensions = di;
            dimension = "y";

            unroll(sprite.dimensions.y, targetSize,time);

        }

        if (sprite.anchor == tk2dBaseSprite.Anchor.MiddleRight || sprite.anchor == tk2dBaseSprite.Anchor.MiddleLeft)
        {

            Vector2 di = new Vector2(sprite.dimensions.x, Breadth);
            sprite.dimensions = di;
            dimension = "x";

            unroll(sprite.dimensions.x, targetSize ,time);
        }

    }
	
	// Update is called once per frame

	void Update () {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rollBack();
        //}
        //Debug.Log(transform.renderer.bounds.max.y);
	}
}
