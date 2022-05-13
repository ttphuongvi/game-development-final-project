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
    public GameObject TrajectoryLineRenderer;

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
        localLeft = (LeftCrossBow.transform.position - this.transform.position) / 20 * 100;
        localRight = (RightCrossBow.transform.position - this.transform.position) / 20 * 100;
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
            Line.SetPosition(1, (SelectedBird.transform.position - this.transform.position) / 20 * 100);
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
        //old and alternative way
        //BirdToThrow.GetComponent<Rigidbody2D>().AddForce
        //    (new Vector2(v2.x, v2.y) * ThrowSpeed * distance * 300 * Time.deltaTime);
        //set the velocity
        SelectedBird.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) * ThrowSpeed * distance;


        //notify interested parties that the bird was thrown
        if (BirdThrown != null)
            BirdThrown(this, EventArgs.Empty);
    }
    public event EventHandler BirdThrown;
}
