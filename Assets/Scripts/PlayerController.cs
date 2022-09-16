using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public void Awake()
    {
        instance = this;
    }

    [SerializeField] LayerMask ObjsMask;

    [SerializeField] Transform viewPoint;
    [SerializeField] float mouseSensitivity = 1f;
    private float verticalRotStore;
    private Vector2 mouseInput;

    public bool invertLook;

    public float moveSpeed = 5f, runSpeed = 8f;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;

    public CharacterController charCon;

    public float jumpForce = 12f, gravityMod = 2.5f;

    public Transform groundCheckPoint;
    private bool isGrounded;
    public LayerMask groundLayers;

    [SerializeField] Camera cam;
    [SerializeField] float rayLength;
    [HideInInspector] public GameObject OnTargetGameObject;

    [HideInInspector] public GameObject grabbingObject;
    public string GrabbedObjectName;

    // Start is called before the first frame update
    void Start()
    {
        GrabbedObjectName = null;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(EnableAudioListner());
    }

    private IEnumerator EnableAudioListner()
    {
        yield return new WaitForSeconds(2);
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        verticalRotStore += mouseInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f);

        if (invertLook)
        {
            viewPoint.rotation = Quaternion.Euler(verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
        }
        else
        {
            viewPoint.rotation = Quaternion.Euler(-verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
        }

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        }
        else
        {
            activeMoveSpeed = moveSpeed;
        }

        float yVel = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        movement.y = yVel;

        if (charCon.isGrounded)
        {
            movement.y = 0f;
        }

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpForce;
        }

        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        charCon.Move(movement * Time.deltaTime);

        ShootViewRay();

        //Drop Grabbed Object

        if (grabbingObject != null && GrabbedObjectName != null)
        {
            if (Input.GetButtonDown("Drop"))
            {
                grabbingObject.transform.position = transform.position;
                grabbingObject.SetActive(true);
                UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                grabbingObject = null;
                GrabbedObjectName = null;
            }
        }
    }

    private void ShootViewRay()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        ray.origin = cam.transform.position;
        
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, ObjsMask))
        {
            OnTargetGameObject = hit.collider.gameObject;

            if(hit.collider.gameObject.tag == "GrabableObject")
            {
                GameObject Object = hit.collider.gameObject;
                Object.GetComponent<GrabableObject>().ShowGrabInfo();
                if(Input.GetButtonDown("UseButton"))
                {
                    Object.GetComponent<GrabableObject>().GrabObject();
                }
            }
        }
        else
        {
            if (UIController.instance.infoText.gameObject.activeInHierarchy || OnTargetGameObject != null)
            {
                OnTargetGameObject = null;
                UIController.instance.infoText.gameObject.SetActive(false);
            }
        }
    }
}