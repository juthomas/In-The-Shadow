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

    public Vector3 originDirectionUp;
    public Vector3 originDirectionForward;
    public Vector3 originDirectionRight;

    public float positionPrecision = 0.1F;

    void Start()
    {
        Debug.Log(ResetPos.transform.position);
        Debug.Log(transform.position);
        Debug.Log(gameObject.name);
        originDirectionUp =  this.transform.up;
        originDirectionForward =  this.transform.forward;
        originDirectionRight =  this.transform.right;
        Camera camera = Camera.main;
        Vector3 right = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 up = Vector3.Cross(transform.position - camera.transform.position, right);
        
        this.transform.Rotate(Random.Range(42.0f, 84.0f), Random.Range(42.0f, 84.0f), Random.Range(42.0f, 84.0f));
        // myRigidbody.AddTorque(up * 420);
        // myRigidbody.AddTorque(right * 420);

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

     private void DrawHelperAtCenter(
                        Vector3 direction, Color color, float scale)
     {
         Gizmos.color = color;
         Vector3 destination = transform.position + direction * scale;
         Gizmos.DrawLine(transform.position, destination);
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
        Debug.Log("Up :" + this.transform.up);
        //DrawHelperAtCenter(this.transform.up, Color.green, 2f);
        Debug.Log("Forward :" + this.transform.forward);
        //DrawHelperAtCenter(this.transform.forward, Color.green, 2f);

        Debug.Log("Right :" + this.transform.right);
        //DrawHelperAtCenter(this.transform.right, Color.green, 2f);

        Camera camera = Camera.main;

        Vector3 right = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 up = Vector3.Cross(transform.position - camera.transform.position, right);

        myRigidbody.AddTorque(up * -rotX);
        myRigidbody.AddTorque(right * rotY);
        }
    }

    void Update () {
        if (Mathf.Abs(this.transform.up.x - originDirectionUp.x) < positionPrecision
            && Mathf.Abs(this.transform.up.y - originDirectionUp.y) < positionPrecision 
            && Mathf.Abs(this.transform.up.z - originDirectionUp.z) < positionPrecision 
            && Mathf.Abs(this.transform.right.x - originDirectionRight.x) < positionPrecision
            && Mathf.Abs(this.transform.right.y - originDirectionRight.y) < positionPrecision 
            && Mathf.Abs(this.transform.right.z - originDirectionRight.z) < positionPrecision 
            && Mathf.Abs(this.transform.forward.x - originDirectionForward.x) < positionPrecision
            && Mathf.Abs(this.transform.forward.y - originDirectionForward.y) < positionPrecision 
            && Mathf.Abs(this.transform.forward.z - originDirectionForward.z) < positionPrecision 
            )
        {
            Debug.Log("DONE !");
        }
        else
        {
            Debug.Log("Pas Done!");
        }
    }

}

