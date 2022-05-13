using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public enum CrossBowStateEnum
    {
        Idle,
        UserPulling
    }


    public GameObject LeftCrossBow;
    public GameObject RightCrossBow;
    public GameObject MainCrossBow;
    public LineRenderer Line;

    public GameObject SelectedBird;

    public Vector3 middle, localLeft, localRight, realMidlle, offset;


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
        realMidlle = (LeftCrossBow.transform.position + RightCrossBow.transform.position) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CrossBowState)
        {
            case CrossBowStateEnum.Idle:
                if (Input.GetMouseButtonDown(0))
                {
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
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 pos = mousePosition + offset;
                    Vector2 limitMove = SelectedBird.GetComponent<Bird>().limitMove;
                    float newX = Mathf.Clamp(pos.x, this.transform.position.x - limitMove.x, this.transform.position.x + limitMove.x);
                    float newY = Mathf.Clamp(pos.y, this.transform.position.y - limitMove.y, this.transform.position.y + limitMove.y);
                    SelectedBird.transform.position = new Vector3(newX, newY, pos.z);
                }

                if (Input.GetMouseButtonUp(0) && SelectedBird)
                {
                    SelectedBird.GetComponent<Bird>().Thrown();
                    // selectedBird = null;
                }
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
}
