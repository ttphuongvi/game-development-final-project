using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

    public Level(int numLevel) {
        HighScore = 0;
        CurrentScore = 0;
        Unlocked = (numLevel == 1 ? true : false);
        OneStarReq = 10000;
        TwoStarReq = 20000;
        ThreeStarReq = 30000;
        numLevel = numLevel;
        Debug.Log("Level " + numLevel + " created");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int numLevel;
    public int HighScore, CurrentScore;
    public bool Unlocked;
    public  int OneStarReq, TwoStarReq, ThreeStarReq;

    public  bool Defeated
    {
        get
        {
            if (HighScore > OneStarReq)
                return true;
            return false;
        }
    }
    
    public bool CurrentDefeated
    {
        get
        {
            if (CurrentScore >= OneStarReq)
                return true;
            return false;
        }
    }

    public int CurrStarScore
    {
        get
        {
            if (CurrentScore >= ThreeStarReq)
                return 3;
            if (CurrentScore >= TwoStarReq)
                return 2;
            if (CurrentScore >= OneStarReq)
                return 1;
            return 0;
        }
    }

    public int HighStarScore
    {
        get
        {
            if (HighScore >= ThreeStarReq)
                return 3;
            if (HighScore >= TwoStarReq)
                return 2;
            if (HighScore >= OneStarReq)
                return 1;
            return 0;
        }
    }

}
