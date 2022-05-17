using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_bird : MonoBehaviour
{
    Vector3 InitiaPos;
    // Start is called before the first frame update
    void Start()
    {
        InitiaPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    private void OnMouseUp()
    {
        Vector3 vectorForce = InitiaPos - transform.position;
        GetComponent<Rigidbody2D>().AddForce(vectorForce * 300);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
