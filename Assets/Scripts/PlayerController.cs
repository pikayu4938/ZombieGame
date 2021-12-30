using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float walkSpeed ,runSpeed;
    float moveSpeed;
    float rotSpeed;
    float stamina;
    public Camera cam;
    Animator animator;
    public GameObject cameraArm;
    CapsuleCollider collider;

    float hAxis, vAxis;
    bool isRunning;
    Vector3 moveVec;

    private float currentCameraRotationX;
    public float interactDiastance = 10f;

    void Start()
    {
        walkSpeed = 3.0f;
        runSpeed = 6.0f;
        moveSpeed = 5.0f;
        rotSpeed = 3.0f;
        stamina = 100f;
        cam = cameraArm.GetComponentInChildren<Camera>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }
    
    void Update()
    {
        MoveController();
        RotController();

       
        Interaction();

        if (!isRunning && stamina < 100) //달리는 중이 아닐경우 스태미너 회복
            stamina += Time.deltaTime * 20;

        if (stamina > 100)
            stamina = 100;
        else if (stamina < 0)
            stamina = 0;
    }

    void MoveController()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        if (stamina <= 0)
            isRunning = false;

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isRunning && stamina > 0)
        {
            transform.Translate(moveVec * runSpeed * Time.deltaTime);
            
            stamina -= Time.deltaTime * 20; // 달리는중이면 스태미너 감소
        }
        else
        {
            transform.Translate(moveVec * walkSpeed * Time.deltaTime);
            
        }
    }

    void RotController()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        currentCameraRotationX -= rotX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -80, 70);

        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }

void Interaction()
{
    if (Input.GetKeyDown(KeyCode.E))
        {
            //Ray ray = new Ray(cameraArm.transform.position, cameraArm.transform.forward);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.CompareTag("Door"))
                {
                    hit.transform.gameObject.GetComponent<DoorController>().ChangeDoorState();
                    Debug.Log("문열기 코드");
                }
                else if (hit.collider.CompareTag("Drawer"))
                {
                    hit.transform.gameObject.GetComponent<DrawerContorller>().ChangeDrawerState();
                    Debug.Log("서랍열기 코드");
                }
                else if (hit.collider.CompareTag("Key"))
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}
