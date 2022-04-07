using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Destructible : MonoBehaviour
{
    [SerializeField] ProBuilderMesh _copyMesh;

    // Start is called before the first frame update
    void Start()
    {
        ProBuilderMesh pbm = ShapeGenerator.GenerateCube(PivotLocation.FirstCorner, new Vector3(1, 1, 1));
        pbm.transform.position = _copyMesh.transform.position;
        pbm.GetComponent<MeshRenderer>().sharedMaterial = _copyMesh.GetComponent<MeshRenderer>().material;
        pbm.ToMesh();
        pbm.Refresh();
    }

    string state;

    // Update is called once per frame
    void Update()
    {
        state = "Walking";

        switch (state)
        {
            default:
                break;
        }
    }
}
