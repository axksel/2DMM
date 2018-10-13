using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/frame", order = 1)]
public class SoFrame : ScriptableObject
{

    public Vector2[] features = new Vector2[18];
    public float velo;




    public float CalculateVelo(Vector2 index)
    {


        //velo = Vector2.SqrMagnitude(features[1]-index);


        if (features[1].x - index.x <= 0)
            velo = -((features[1] - index).magnitude);
        else velo = (features[1] - index).magnitude;


        return velo;
    }


}