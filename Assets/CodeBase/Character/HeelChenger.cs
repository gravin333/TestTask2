using UnityEngine;

public class HeelChenger : MonoBehaviour
{
    public GameObject heel;

    public GameObject RightFoot;

    public GameObject LeftFoot;

    public Quaternion rotation;
    public Vector3 shiftPosition;
    public Vector3 scale;
    private CharacterController _characterController;

    private GameObject LeftHeel;
    private GameObject RightHeel;

    // Start is called before the first frame update
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (heel)
        {
            //InstantiateHeels();
        }
    }

    private void Update()
    {
        if (LeftHeel != null)
        {
            var child = LeftHeel.transform.GetChild(0);
            child.localPosition = shiftPosition;
            child.localRotation = rotation;
            child.localScale = scale;
        }

        if (RightHeel != null)
        {
            var child = RightHeel.transform.GetChild(0);
            child.localPosition = shiftPosition;
            child.localRotation = rotation;
            child.localScale = scale;
        }
    }

    public void AddHeels(Mesh mesh, Material material)
    {
        if (LeftHeel != null) Destroy(LeftHeel);

        if (RightHeel != null) Destroy(RightHeel);

        var baseMesh = InstantiateBaseObject(mesh, material);

        RightHeel = InstantiateParentForHeel(RightFoot.transform, "rf");
        InstantiateHeelAndSetOnCharacter(baseMesh, RightHeel.transform);

        LeftHeel = InstantiateParentForHeel(LeftFoot.transform, "lf");
        InstantiateHeelAndSetOnCharacter(baseMesh, LeftHeel.transform);

        Destroy(baseMesh);
        ChangeHeight(mesh);
    }

    private void InstantiateHeelAndSetOnCharacter(GameObject baseMesh, Transform heelTransform)
    {
        var instantiate = Instantiate(baseMesh, heelTransform);
        instantiate.name = "obj";
        instantiate.transform.localPosition = Vector3.zero;
    }

    private GameObject InstantiateParentForHeel(Transform heelTransform, string objName)
    {
        var right = Instantiate(heel, heelTransform);
        right.name = objName;
        return right;
    }

    private static GameObject InstantiateBaseObject(Mesh mesh, Material material)
    {
        var obj = new GameObject
        {
            name = "baseMesh"
        };
        var meshFilter = obj.AddComponent<MeshFilter>();
        var meshRenderer = obj.AddComponent<MeshRenderer>();
        meshFilter.mesh = mesh;
        meshRenderer.material = material;
        return obj;
    }

    private void ChangeHeight(Mesh mesh)
    {
        var meshBindposes = mesh.bounds;
        Debug.Log(meshBindposes.size);
        _characterController.height = 1 + meshBindposes.size.y * 0.1f * 2;
    }
}