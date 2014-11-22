using UnityEngine;
using System.Collections;

public class trackScore : MonoBehaviour {

	// Use this for initialization
    [HideInInspector]
    int score = 0;
    public int target = 0;
    string finalString;
    string prefix;

    UILabel label;

    public bool checkTargetAchieved()
    {
        bool flagTargetScore = false;

        if (score == target)
        {
            flagTargetScore = true;
        }

        return flagTargetScore;
    }

    public void setScoreZero()
    {
        score = 0;
        updateLabel();
        
    }

    public void updateScore()
    {
        score = score + 1;
        updateLabel();
        
    }

    public void updateLabel()
    {
        label.text = score.ToString() + " / " + target.ToString();
    }

	void Start () {

        label = transform.GetComponent<UILabel>();

        if (gameObject.name == "xLabel")
        {
            prefix = "X ";
        }
        else
        {
            prefix = "O ";

        }

        updateLabel();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
