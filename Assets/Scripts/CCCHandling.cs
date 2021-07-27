using HTC.UnityPlugin.Vive;
using UnityEngine;
public class CCCHandling : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject ccc;
    [SerializeField] private GameObject selectionCursor;
    [SerializeField] public GameObject lastCollidedObject;
    [SerializeField] public GameObject secondaryHandController;
    [SerializeField] public GameObject secondaryHandPointer;
    [SerializeField] public GameObject primaryHandPointer;

    private GameObject lastPointedGameObject;
    private Vector3 lastPointedPosition;

    bool fixPosition = false;

    float maxX;
    float maxY;
    float maxZ;
    float minX;
    float minY;
    float minZ;

    // Start is called before the first frame update
    void Start()
    {
        ccc.SetActive(false);
        selectionCursor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ViveRaycaster ray = primaryHandPointer.GetComponentInChildren<ViveRaycaster>();
        if(ray != null)
        {
            if(ray.FirstRaycastResult().gameObject != null )
            {
                if (ray.FirstRaycastResult().gameObject.tag != "Plane")
                {
                    lastPointedGameObject = ray.FirstRaycastResult().gameObject;
                }
                lastPointedPosition = ray.FirstRaycastResult().worldPosition;
            }
        }

        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Trigger))
        {
            secondaryHandController.SetActive(false);
            secondaryHandPointer.SetActive(false);
            ccc.SetActive(true);
            selectionCursor.SetActive(true);
            fixPosition = true;
            CalculateBounds();
        }
        if (ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Trigger))
        {
            ccc.SetActive(false);
            selectionCursor.SetActive(false);
            secondaryHandController.SetActive(true);
            secondaryHandPointer.SetActive(true);
            fixPosition = false;
            DoAction();
        }

        if (!fixPosition)
        {
            Vector3 mouseWorldPosition = VivePose.GetPose(HandRole.LeftHand).pos;
            ccc.transform.position = mouseWorldPosition;


            // war dazu da die Ausrichtung des CCCs an die Kamera anzupassen, gab leider nur Schwierigkeiten mit der Boundingbox
            //var n = mainCamera.transform.position - ccc.transform.position;
            //ccc.transform.rotation = Quaternion.LookRotation(n) * Quaternion.Euler(0, 90, 0);

        }
        else
        {
            Vector3 mouseWorldPosition = VivePose.GetPose(HandRole.LeftHand).pos;
            

            if (mouseWorldPosition.x < minX)
            {
                mouseWorldPosition.x = minX;
            }
            if (mouseWorldPosition.y < minY)
            {
                mouseWorldPosition.y = minY;
            }
            if (mouseWorldPosition.z < minZ)
            {
                mouseWorldPosition.z = minZ;
            }
            if (mouseWorldPosition.x > maxX)
            {
                mouseWorldPosition.x = maxX;
            }
            if (mouseWorldPosition.y > maxY)
            {
                mouseWorldPosition.y = maxY;
            }
            if (mouseWorldPosition.z > maxZ)
            {
                mouseWorldPosition.z = maxZ;
            }

            if (mouseWorldPosition.z > maxZ - (maxZ - minZ) * 0.3333)
            {
                MakeLowerPlaneVisible();
            }
            else if (mouseWorldPosition.z < minZ + (maxZ - minZ) * 0.3333)
            {
                MakeUpperPlaneVisible();
            }
            else
            {
                MakeMiddlePlaneVisible();
            }
            selectionCursor.GetComponent<Rigidbody>().MovePosition(mouseWorldPosition);
            var n = mainCamera.transform.position - selectionCursor.transform.position;
            selectionCursor.transform.rotation = Quaternion.LookRotation(n) * Quaternion.Euler(0, 90, 0);
        }

    }

    void CalculateBounds()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject PlaneChild = ccc.transform.GetChild(i).gameObject;

            for (int j = 0; j < 9; j++)
            {

                GameObject child = PlaneChild.transform.GetChild(j).gameObject;

                Vector3 position = child.transform.position;

                if (i == 0 && j == 0)
                {
                    minX = position.x;
                    minY = position.y;
                    minZ = position.z;
                    maxX = position.x;
                    maxY = position.y;
                    maxZ = position.z;
                }
                else
                {
                    if (position.x < minX)
                    {
                        minX = position.x;
                    }
                    if (position.x > maxX)
                    {
                        maxX = position.x;
                    }
                    if (position.y < minY)
                    {
                        minY = position.y;
                    }
                    if (position.y > maxY)
                    {
                        maxY = position.y;
                    }
                    if (position.z < minZ)
                    {
                        minZ = position.z;
                    }
                    if (position.z > maxZ)
                    {
                        maxZ = position.z;
                    }
                }
            }
        }
    }

    void MakeLowerPlaneVisible()
    {
        ccc.transform.GetChild(0).gameObject.SetActive(true);
        ccc.transform.GetChild(1).gameObject.SetActive(false);
        ccc.transform.GetChild(2).gameObject.SetActive(false);
    }
    void MakeMiddlePlaneVisible()
    {
        ccc.transform.GetChild(0).gameObject.SetActive(false);
        ccc.transform.GetChild(1).gameObject.SetActive(true);
        ccc.transform.GetChild(2).gameObject.SetActive(false);
    }
    void MakeUpperPlaneVisible()
    {
        ccc.transform.GetChild(0).gameObject.SetActive(false);
        ccc.transform.GetChild(1).gameObject.SetActive(false);
        ccc.transform.GetChild(2).gameObject.SetActive(true);
    }
    void DoAction()
    {
        switch (lastCollidedObject.name)
        {
            case "BottomAction1":
                if(lastPointedGameObject != null)
                lastPointedGameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                break;            
            case "BottomAction2":
                print("lastCollidedObject.name");
                break;            
            case "BottomAction3":
                print("lastCollidedObject.name");
                break;            
            case "BottomAction4":
                print("lastCollidedObject.name");
                break;            
            case "BottomAction5":
                if (lastPointedGameObject != null)
                    lastPointedGameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                break;            
            case "BottomAction6":
                print("lastCollidedObject.name");
                break;            
            case "BottomAction7":
                print("lastCollidedObject.name");
                break;            
            case "BottomAction8":
                print("lastCollidedObject.name");
                break;            
            case "BottomAction9":
                if (lastPointedGameObject != null)
                    lastPointedGameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                break;            
            case "MiddleAction1":
                print("lastCollidedObject.name");
                break;            
            case "MiddleAction2":
                print("lastCollidedObject.name");
                break;            
            case "MiddleAction3":
                print("lastCollidedObject.name");
                break;            
            case "MiddleAction4":
                print("lastCollidedObject.name");
                break;            
            case "MiddleAction5":
                print("EXIT");
                break;            
            case "MiddleAction6":
                if (lastPointedGameObject != null)
                {
                    Destroy(lastPointedGameObject);
                    lastPointedGameObject = null;
                }
                break;
            case "MiddleAction7":
                print("lastCollidedObject.name");
                break;            
            case "MiddleAction8":
                print("lastCollidedObject.name");
                break;            
            case "MiddleAction9":
                print("lastCollidedObject.name");
                break;
            case "UpperAction1":
                print("lastCollidedObject.name");
                break;
            case "UpperAction2":
                print("lastCollidedObject.name");
                break;            
            case "UpperAction3":
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = lastPointedPosition;
                break;            
            case "UpperAction4":
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = lastPointedPosition;
                break;            
            case "UpperAction5":
                print("lastCollidedObject.name");
                break;            
            case "UpperAction6":
                print("lastCollidedObject.name");
                break;           
            case "UpperAction7":
                print("lastCollidedObject.name");
                break;            
            case "UpperAction8":
                print("lastCollidedObject.name");
                break;            
            case "UpperAction9":
                GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                capsule.transform.position = lastPointedPosition;
                break;
            default:
                break;

        }
    }
}
