using UnityEngine;
using System.Collections;

public class triggerWind : MonoBehaviour {

	// Use this for initialization
    public LayerMask mask;
	void Start () {

        color = ribbon.tag;
        //Debug.Log();
	}

    public int Type;

   public GameObject ribbon;

   [HideInInspector] public string color;

    //bool cast = false;
  public bool marked = false;

    Vector2[] raycastDirections = new Vector2[2];
    Vector2[] upDown = new Vector2[]{Vector2.up, -Vector2.up};
    Vector2[] leftRight = new Vector2[]{Vector2.right , -Vector2.right};
    
    enum SwipeDirection { Up, Down, Left, Right };

    SwipeDirection currentSwipeDirection;
    SwipeDirection rayTravelDirection;

    Vector3 windStart;
    Vector3 windEnd;

    [HideInInspector] public bool Horizontal = false;
    [HideInInspector] public bool Vertical = false;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    bool check = false;
    bool up = false;
     
    Vector3 camRect;    

    public float swipeSensitivity = 0.5f;

    void grow()
    {
        //GameObject.Find("turnLabel").GetComponent<turns>().moves > 0
        if (check )
        {
            //check = false;
            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -swipeSensitivity && currentSwipe.x < swipeSensitivity)
            {
                raycastDirections = upDown;
                currentSwipeDirection = SwipeDirection.Up;
            }

            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -swipeSensitivity && currentSwipe.x < swipeSensitivity)
            {
                raycastDirections = upDown;
                currentSwipeDirection = SwipeDirection.Down;
            }

            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -swipeSensitivity && currentSwipe.y < swipeSensitivity)
            {
                raycastDirections = leftRight;
                currentSwipeDirection = SwipeDirection.Left;
            }

            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -swipeSensitivity && currentSwipe.y < swipeSensitivity)
            {
                raycastDirections = leftRight;
                currentSwipeDirection = SwipeDirection.Right;
            }

            foreach (Vector2 k in raycastDirections)
            {

                RaycastHit d;

                if (k == Vector2.up)
                {
                    rayTravelDirection = SwipeDirection.Up;
                }
                else if (k == -Vector2.up)
                {
                    rayTravelDirection = SwipeDirection.Down;
                }
                else if (k == Vector2.right)
                {
                    rayTravelDirection = SwipeDirection.Right;
                }
                else
                {
                    rayTravelDirection = SwipeDirection.Left;
                }


                if (rayTravelDirection == currentSwipeDirection)
                {

                    if (Physics.Raycast(transform.position, k, out d, Mathf.Infinity,mask ))
                    {
                        Debug.DrawLine(transform.position, d.collider.transform.position, Color.green, 20, false);

                        if (currentSwipeDirection == SwipeDirection.Up )
                        {
                            //windEnd = new Vector3(transform.position.x, d.collider.transform.position.y, transform.position.z);
                            windEnd = new Vector3(transform.position.x,d.collider.bounds.min.y, transform.position.z);

                        }

                        if (currentSwipeDirection == SwipeDirection.Down)
                        {
                            windEnd = new Vector3(transform.position.x, d.collider.bounds.max.y, transform.position.z);
                        }

                        if (currentSwipeDirection == SwipeDirection.Left )
                        {

                            //windEnd = new Vector3(d.collider.transform.position.x, transform.position.y, transform.position.z);
                            windEnd = new Vector3(d.collider.bounds.max.x, transform.position.y, transform.position.z);
                        }

                        if (currentSwipeDirection == SwipeDirection.Right)
                        {
                            windEnd = new Vector3(d.collider.bounds.min.x, transform.position.y, transform.position.z);

                        }

                    }

                    else
                    {
                        if (rayTravelDirection == SwipeDirection.Up)
                        {

                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.max);
                            windEnd = new Vector3(transform.position.x, camRect.y, transform.position.z);

                        }

                        if (rayTravelDirection == SwipeDirection.Down)
                        {

                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.min);
                            windEnd = new Vector3(transform.position.x, camRect.y, transform.position.z);


                        }

                        if (rayTravelDirection == SwipeDirection.Left)
                        {
                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.min);
                            windEnd = new Vector3(camRect.x, transform.position.y, transform.position.z);

                        }

                        if (rayTravelDirection == SwipeDirection.Right)
                        {
                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.max);
                            windEnd = new Vector3(camRect.x, transform.position.y, transform.position.z);

                        }

                  

                    }

                    if (marked)
                    {
                        windEnd = transform.position;
                    }
                }

                else
                {


                    if (Physics.Raycast(transform.position, k, out d, Mathf.Infinity,mask ))
                    {
                        Debug.DrawLine(transform.position, d.collider.transform.position, Color.green, 20, false);
                        windStart = new Vector3(transform.position.x, camRect.y, transform.position.z);

                        if (currentSwipeDirection == SwipeDirection.Up)
                        {
                            //windStart = new Vector3(transform.position.x, d.collider.transform.position.y, transform.position.z);
                            windStart = new Vector3(transform.position.x, d.collider.bounds.max.y, transform.position.z);

                        }

                        if (currentSwipeDirection == SwipeDirection.Down)
                        {
                            windStart = new Vector3(transform.position.x, d.collider.bounds.min.y, transform.position.z);
                        }

                        if (currentSwipeDirection == SwipeDirection.Left)
                        {

                            //windStart = new Vector3(d.collider.transform.position.x, transform.position.y, transform.position.z);
                            windStart = new Vector3(d.collider.bounds.min.x, transform.position.y, transform.position.z);
                        }

                        if (currentSwipeDirection == SwipeDirection.Right)
                        {
                            windStart = new Vector3(d.collider.bounds.max.x, transform.position.y, transform.position.z);

                        }

                    }

                    else
                    {
                        if (rayTravelDirection == SwipeDirection.Up)
                        {
                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.max);
                            windStart = new Vector3(transform.position.x, camRect.y, transform.position.z);

                        }

                        if (rayTravelDirection == SwipeDirection.Down)
                        {
                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.min);
                            windStart = new Vector3(transform.position.x, camRect.y, transform.position.z);

                        }

                        if (rayTravelDirection == SwipeDirection.Left)
                        {
                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.min);
                            windStart = new Vector3(camRect.x, transform.position.y, transform.position.z);
                        }

                        if (rayTravelDirection == SwipeDirection.Right)
                        {
                            camRect = Camera.main.ViewportToWorldPoint(Camera.main.rect.max);
                            windStart = new Vector3(camRect.x, transform.position.y, transform.position.z);
                        }

                    }


                }

            }


            if (currentSwipeDirection == SwipeDirection.Up || currentSwipeDirection == SwipeDirection.Down)
            {
                float size = Mathf.Abs(windStart.y - windEnd.y) * 100;
                float timeFactor = ( size /Camera.main.GetComponent<tk2dCamera>().NativeResolution.y );

                //if (!Vertical && !Horizontal)
                if(!marked)
                {          
                    GameObject r = (GameObject)Instantiate(ribbon, windStart, Quaternion.identity);

                    if (currentSwipeDirection == SwipeDirection.Up)
                        r.GetComponent<Wind>().initialise(tk2dBaseSprite.Anchor.LowerCenter, windStart, size , timeFactor);
                    else
                        r.GetComponent<Wind>().initialise(tk2dBaseSprite.Anchor.UpperCenter, windStart, size , timeFactor);

                    Vertical = true;
                    marked = true;

                    //GameObject.Find("turnLabel").GetComponent<turns>().updateMoves();

                }

            }

            if (currentSwipeDirection == SwipeDirection.Left || currentSwipeDirection == SwipeDirection.Right)
            {
                float size = Mathf.Abs(windStart.x - windEnd.x) * 100;
                float timeFactor = (size / Camera.main.GetComponent<tk2dCamera>().NativeResolution.x );

                //if (!Vertical && !Horizontal)
                if(!marked)
                {
                    GameObject r = (GameObject)Instantiate(ribbon, windStart, Quaternion.identity);
                    
                    if (currentSwipeDirection == SwipeDirection.Right)
                        r.GetComponent<Wind>().initialise(tk2dBaseSprite.Anchor.MiddleLeft, windStart, size , timeFactor);
                    else
                        r.GetComponent<Wind>().initialise(tk2dBaseSprite.Anchor.MiddleRight, windStart, size , timeFactor);

                    Horizontal = true;
                    marked = true;

                    //check = false;

                    //GameObject.Find("turnLabel").GetComponent<turns>().updateMoves();


                }

            }

        }
    }



//void processTouch(Touch t , int registerId)
void processTouch(Touch t )

{
        if (t.phase == TouchPhase.Began)
        {

            firstPressPos = new Vector2(t.position.x, t.position.y);

        }

        if (t.phase == TouchPhase.Ended)
        {

            secondPressPos = new Vector2(t.position.x, t.position.y);
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            grow();
            check = false;

        }

}

public void Swipe()
{

#if UNITY_EDITOR || UNITY_STANDALONE

            if (Input.GetMouseButtonDown(0))
            {

                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            }

            if (Input.GetMouseButtonUp(0))
            {

                //save ended touch 2d point
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                //Debug.Log(currentSwipe);

                //normalize the 2d vector
                currentSwipe.Normalize();
                //Debug.Log(currentSwipe);

                grow();

                check = false;

            }

#endif

#if UNITY_ANDROID || UNITY_IPHONE

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            processTouch(t);

        }

#endif

}




   


void Update () {
    
    Swipe();

#if UNITY_EDITOR || UNITY_STANDALONE

    if (Input.GetMouseButtonDown(0))
    {
        //RaycastHit2D hit;

        //hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //if (hit.collider != null && hit.collider.gameObject == gameObject)
        //{
        //    check = true;
        //    Debug.Log("work");
        //}

        RaycastHit hit;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            //Debug.Log("Hello");
            //Debug.Log(hit.collider.transform.position);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
                check = true;

        }


    }

#endif


#if UNITY_ANDROID || UNITY_IPHONE

          //if (Input.GetTouch(0).phase == TouchPhase.Began)
          //{

          //    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Vector3.zero.z)), Vector2.zero);
          //    if (hit.collider != null && hit.collider.gameObject == gameObject)
          //    {
          //        check = true;
          //        //Debug.Log("still working");
          //    }
          //}

    //if (Input.GetTouch(0).phase == TouchPhase.Began)
    //{

    //    RaycastHit hit;
    //    Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(r, out hit, Mathf.Infinity))
    //    {
    //        if (hit.collider != null && hit.collider.gameObject == gameObject)
    //        {
    //            check = true;

    //        }

    //    }
    //}

#endif


        //Debug.Log(check);


    }


}
