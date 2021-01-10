using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private MojiPlate grabbingMojiPlate;
    
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Input.mousePosition;

    }

    public MojiPlate GetMojiPlateData()
    {
        MojiPlate oldMojiPlate = grabbingMojiPlate;
        grabbingMojiPlate = null;
        return oldMojiPlate;
    }

    public void SetGrabbingMojiPlate(MojiPlate mojiPlate)
    {
        grabbingMojiPlate = mojiPlate;
    }

    public void Ray()
    {

    }
    
}
