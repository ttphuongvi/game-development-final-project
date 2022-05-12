using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public GameObject LeftCrossBow;
    public GameObject RightCrossBow;
    public GameObject MainCrossBow;
    public LineRenderer Line;

    public GameObject SelectedBird;

    public Vector3 middle;

    // Start is called before the first frame update
    void Start()
    {
        Line.positionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 realLeft = (LeftCrossBow.transform.position - this.transform.position) / 20 * 100;
        Vector3 realRight = (RightCrossBow.transform.position - this.transform.position) / 20 * 100;
        Line.SetPosition(0, realLeft);
        Line.SetPosition(1, (realLeft + realRight) / 2);
        Line.SetPosition(2, realRight);
    }
}
