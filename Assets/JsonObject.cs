using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JsonObject  {


    public float version;
    public JsonObjectInner[] people;  
    

}
[System.Serializable]
public class JsonObjectInner
{


    public float[] pose_keypoints_2d;


}
