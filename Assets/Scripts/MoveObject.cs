using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject cross_bow;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(selectedObject);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject; // red
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            Vector3 pos = mousePosition + offset;
            float limitMove = selectedObject.GetComponent<Config>().limitMove;
            // float distance = Vector2.Distance(pos, cross_bow.transform.position);
            float newX = Mathf.Clamp(pos.x, cross_bow.transform.position.x - limitMove, cross_bow.transform.position.x + limitMove);
            float newY = Mathf.Clamp(pos.y, cross_bow.transform.position.y - limitMove, cross_bow.transform.position.y + limitMove);
            selectedObject.transform.position = new Vector3(newX, newY, pos.z);

        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
}
