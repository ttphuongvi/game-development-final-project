using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Level() {
        HighScore = 0;
        CurrentScore = 0;
        Unlocked = false;
        OneStarReq = 10000;
        TwoStarReq = 20000;
        ThreeStarReq = 30000;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int HighScore, CurrentScore;
    public static bool Unlocked;
    public static int OneStarReq, TwoStarReq, ThreeStarReq;

    public static bool Defeated
    {
        get
        {
            if (HighScore > OneStarReq)
                return true;
            return false;
        }
    }
    
    public static bool CurrentDefeated
    {
        get
        {
            if (CurrentScore >= OneStarReq)
                return true;
            return false;
        }
    }

    public static int CurrStarScore
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

    public static int HighStarScore
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
