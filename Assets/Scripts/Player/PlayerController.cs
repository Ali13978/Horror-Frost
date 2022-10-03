using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public void Awake()
    {
        instance = this;
    }

    [SerializeField] public int Range = 6;

    [SerializeField] LayerMask ObjsMask;

    [SerializeField] bool mobilecontrol;
    [SerializeField] public Transform viewPoint;
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

    [SerializeField] public GameObject grabbingObject;
    public string GrabbedObjectName;

    [HideInInspector] public bool TakingDamage = false;

    [HideInInspector] public bool VenomDrinked = false;
    [SerializeField] float VenomTimer = 180;
    float VenomTimeCounter;

    bool TimerStarted;
    [SerializeField] int StartTimer;
    [SerializeField] GameObject TimerStartedPlane;
    [SerializeField] GameObject TimerEndPlane;
    float TimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        GrabbedObjectName = null;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        VenomTimeCounter = VenomTimer;
        TimeCounter = StartTimer;
        activeMoveSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (VenomDrinked)
        {
            if (!UIController.instance.timerText.gameObject.activeInHierarchy)
            { UIController.instance.timerText.gameObject.SetActive(true); }
            VenomTimeCounter -= Time.deltaTime;
            var timeToDisplay = System.TimeSpan.FromSeconds(VenomTimeCounter);

            UIController.instance.timerText.text = "anti-venom timer: " + timeToDisplay.Minutes.ToString("00") + ":" + timeToDisplay.Seconds.ToString("00");
            if (VenomTimeCounter <= 0)
            {
                VenomDrinked = false;
                VenomTimeCounter = VenomTimer;
                UIController.instance.timerText.gameObject.SetActive(false);
            }
        }

        if (TimerStarted)
        {
            if (!UIController.instance.timerText.gameObject.activeInHierarchy)
            { UIController.instance.timerText.gameObject.SetActive(true); }
            TimeCounter -= Time.deltaTime;
            var timeToDisplay = System.TimeSpan.FromSeconds(TimeCounter);

            UIController.instance.timerText.text = "timer: " + timeToDisplay.Minutes.ToString("00") + ":" + timeToDisplay.Seconds.ToString("00");
            if (TimeCounter <= 0)
            {
                LevelManager.instance.TakeDamage();
                UIController.instance.LifeLostScreen.SetActive(true);
                TimerStarted = false;
                TimeCounter = StartTimer;
                UIController.instance.timerText.gameObject.SetActive(false);
            }
        }


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

        moveDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0f, CrossPlatformInputManager.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
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

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpForce;
        }

        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        charCon.Move(movement * Time.deltaTime);

        ShootViewRay();

        //Drop Grabbed Object

        if (grabbingObject != null || GrabbedObjectName != null)
        {
            if (CrossPlatformInputManager.GetButtonDown("Drop"))
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

            if (hit.collider.gameObject.tag == "GrabableObject")
            {
                GameObject Object = hit.collider.gameObject;
                Object.GetComponent<GrabableObject>().ShowGrabInfo();
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == TimerStartedPlane)
        {
            TimerStarted = true;
        }

        else if (other.gameObject == TimerEndPlane)
        {
            TimerStarted = false;
            TimeCounter = StartTimer;
            UIController.instance.timerText.gameObject.SetActive(false);
        }
    }
    
    private void LateUpdate()
    {
        if (!mobilecontrol)
        {
            mouseInput = new Vector2(CrossPlatformInputManager.GetAxisRaw("Mouse X"), CrossPlatformInputManager.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        }

        else
        {

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    mouseInput = new Vector2(touch.deltaPosition.x, touch.deltaPosition.y) * mouseSensitivity;
                }

                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    mouseInput = Vector2.zero;
                }
                
            }
        }
    }
}