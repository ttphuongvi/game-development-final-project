using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector]
    public bool IsFollowing;
    public float minCameraX = 0;
    public float maxCameraX = 15;
    public GameObject BirdToFollow;
    // Start is called before the first frame update
    void Start()
    {
        IsFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFollowing)
        {
            if (BirdToFollow != null) //bird will be destroyed if it goes out of the scene
            {
                var birdPosition = BirdToFollow.transform.position;
                float x = Mathf.Clamp(birdPosition.x, minCameraX, maxCameraX);
                //camera follows bird's x position
                transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
