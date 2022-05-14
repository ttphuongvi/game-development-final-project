using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public enum CrossBowStateEnum
    {
        Idle,
        UserPulling,
        BirdFlying
    }


    public GameObject LeftCrossBow;
    public GameObject RightCrossBow;
    public GameObject MainCrossBow;
    public LineRenderer Line;
    public LineRenderer TrajectoryLineRenderer;

    public GameObject SelectedBird;
    [HideInInspector]
    public Vector3 middle, localLeft, localRight, realMiddle, offset;
    public float ThrowSpeed;


    [HideInInspector]
    public CrossBowStateEnum CrossBowState;

    // Start is called before the first frame update
    void Start()
    {
        CrossBowState = CrossBowStateEnum.Idle;

        Line.positionCount = 3;
        localLeft = (LeftCrossBow.transform.position - this.transform.position) / this.transform.localScale.x;
        localRight = (RightCrossBow.transform.position - this.transform.position) / this.transform.localScale.x;
        middle = (localLeft + localRight) / 2;
        realMiddle = (LeftCrossBow.transform.position + RightCrossBow.transform.position) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CrossBowState)
        {
            case CrossBowStateEnum.Idle:
                if (Input.GetMouseButtonDown(0))
                {
                    // Nhân mút sẽ chuyển sang trạng thái UserPulling
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                    if (targetObject && targetObject == SelectedBird.GetComponent<CircleCollider2D>())
                    {
                        CrossBowState = CrossBowStateEnum.UserPulling;
                        offset = SelectedBird.transform.position - mousePosition;
                    }
                }
                break;

            case CrossBowStateEnum.UserPulling:
                if (Input.GetMouseButton(0))
                {
                    // Di chuyển chim theo vị trí chuột
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 pos = mousePosition + offset;
                    Vector2 limitMove = SelectedBird.GetComponent<Bird>().limitMove;
                    float newX = Mathf.Clamp(pos.x, realMiddle.x - limitMove.x, realMiddle.x + limitMove.x);
                    float newY = Mathf.Clamp(pos.y, realMiddle.y - limitMove.y, realMiddle.y + limitMove.y);
                    SelectedBird.transform.position = new Vector3(newX, newY, pos.z);

                    float distance = Vector3.Distance(realMiddle, SelectedBird.transform.position);
                    DisplayTrajectoryLineRenderer(distance);
                }

                if (Input.GetMouseButtonUp(0) && SelectedBird)
                {
                    // Ném chuột khi người dùng thả
                    float distance = Vector3.Distance(realMiddle, SelectedBird.transform.position);
                    CrossBowState = CrossBowStateEnum.BirdFlying;
                    ThrowBird(distance);
                }
                break;
            default:
                break;
        }

        Line.SetPosition(0, localLeft);
        if (SelectedBird && SelectedBird.GetComponent<Bird>().State == Bird.BirdState.BeforeThrown)
        {
            Line.SetPosition(1, (SelectedBird.transform.position - this.transform.position) / this.transform.localScale.x);
        }
        else
        {
            Line.SetPosition(1, middle);
        }
        Line.SetPosition(2, localRight);
    }

    private void ThrowBird(float distance)
    {
        //get velocity
        Vector3 velocity = realMiddle - SelectedBird.transform.position;
        SelectedBird.GetComponent<Bird>().Thrown(); //make the bird aware of it
        SelectedBird.GetComponent<Rigidbody2D>().velocity = velocity * ThrowSpeed * distance;

        //notify interested parties that the bird was thrown
        if (BirdThrown != null)
            BirdThrown(this, EventArgs.Empty);
    }
    public event EventHandler BirdThrown;

    public void DisplayTrajectoryLineRenderer(float distance)
    {
        // SetTrajectoryLineRenderesActive(true);
        Vector2 velocity = realMiddle - SelectedBird.transform.position;
        int segmentCount = 15;
        float segmentScale = 2;
        Vector2[] segments = new Vector2[segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = SelectedBird.transform.position;

        // The initial velocity
        Vector2 segVelocity = velocity * ThrowSpeed * distance;

        float angle = Vector2.Angle(segVelocity, new Vector2(1, 0));
        float time = segmentScale / segVelocity.magnitude;
        for (int i = 1; i < segmentCount; i++)
        {
            //x axis: spaceX = initialSpaceX + velocityX * time
            //y axis: spaceY = initialSpaceY + velocityY * time + 1/2 * accelerationY * time ^ 2
            //both (vector) space = initialSpace + velocity * time + 1/2 * acceleration * time ^ 2
            float time2 = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
        }

        TrajectoryLineRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            Vector2 v2position = this.transform.position;
            TrajectoryLineRenderer.SetPosition(i, (segments[i] - v2position) / this.transform.localScale.x);

        }
    }
}
