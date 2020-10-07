using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour 
{
    //float rotSpeed = 2000;
    [SerializeField]
    Rigidbody myRigidbody;

    public GameObject ResetPos;

    float rotSpeed = 10f;

    private Vector3 mOffset;
    private float mZCoord;

    public bool isMovable = false;

    void Start()
    {
        Debug.Log(ResetPos.transform.position);
        Debug.Log(transform.position);
        Debug.Log(gameObject.name);
    }

    void OnDisable()
    {
        transform.position = ResetPos.transform.position;
        transform.rotation = ResetPos.transform.rotation;
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    void OnMouseDrag()
    {
        if(Input.GetKey(KeyCode.LeftControl) && isMovable)
        {
            Debug.Log("Reset : " + ResetPos.transform.position.y.ToString() + " New Pos : " + (GetMouseAsWorldPoint().y + mOffset.y).ToString());
            if(GetMouseAsWorldPoint().y + mOffset.y < ResetPos.transform.position.y + 0.5f &&
                GetMouseAsWorldPoint().y + mOffset.y > ResetPos.transform.position.y - 0.5f)
                    transform.position = new Vector3(transform.position.x, GetMouseAsWorldPoint().y + mOffset.y, transform.position.z);
        }
        else
        {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed;
        Debug.Log("Mouse drag");
        Camera camera = Camera.main;

        Vector3 right = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 up = Vector3.Cross(transform.position - camera.transform.position, right);

        myRigidbody.AddTorque(up * -rotX);
        myRigidbody.AddTorque(right * rotY);
        }
    }
}

