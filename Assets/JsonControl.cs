using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public  class JsonControl : MonoBehaviour {


    public JsonObject jsonFile;
    public GameObject prefab;
    public GameObject prefab2;
    public int count;
    public static int numberOfFrames =343;
 
    public static SoFrame[] frame = new SoFrame[numberOfFrames];


    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 15;


        SetFrames();
        NormalizeVel();
        for (int i = 0; i < numberOfFrames; i++)
        {
            frame[i].CalculateSim(frame, i);

         //   Debug.Log(frame[i].ClosestFrame(frame));
        }
       


    }


    private void Update()
    {

   

     Instantiate(prefab, new Vector3(frame[count].features[1].x, frame[count].features[1].y), Quaternion.identity);


        Debug.Log(frame[count].ClosestFrame(frame));

        count = frame[count].ClosestFrame(frame);
    }



    void SetFrames(){


        jsonFile = new JsonObject();
        string content;



        for (int i = 0; i < numberOfFrames; i++)
        {
            content = System.IO.File.ReadAllText("Assets/output2/testVideo" + i.ToString("0000") + "_keypoints.json");
            JsonUtility.FromJsonOverwrite(content, jsonFile);

            if (jsonFile.people.Length == 1)
            {

                frame[i] = ScriptableObject.CreateInstance<SoFrame>();

                frame[i].features[0] = new Vector2(jsonFile.people[0].pose_keypoints_2d[0], jsonFile.people[0].pose_keypoints_2d[1]); // head
                frame[i].features[1] = new Vector2(jsonFile.people[0].pose_keypoints_2d[3], jsonFile.people[0].pose_keypoints_2d[4]); // neck
                frame[i].features[2] = new Vector2(jsonFile.people[0].pose_keypoints_2d[6], jsonFile.people[0].pose_keypoints_2d[7]); //leftshoulder
                frame[i].features[3] = new Vector2(jsonFile.people[0].pose_keypoints_2d[9], jsonFile.people[0].pose_keypoints_2d[10]); // left elobw
                frame[i].features[4] = new Vector2(jsonFile.people[0].pose_keypoints_2d[12], jsonFile.people[0].pose_keypoints_2d[13]); // left hand
                frame[i].features[5] = new Vector2(jsonFile.people[0].pose_keypoints_2d[15], jsonFile.people[0].pose_keypoints_2d[16]); // right shoulder
                frame[i].features[6] = new Vector2(jsonFile.people[0].pose_keypoints_2d[18], jsonFile.people[0].pose_keypoints_2d[19]); //right elbow
                frame[i].features[7] = new Vector2(jsonFile.people[0].pose_keypoints_2d[21], jsonFile.people[0].pose_keypoints_2d[22]); //right hand
                frame[i].features[8] = new Vector2(jsonFile.people[0].pose_keypoints_2d[24], jsonFile.people[0].pose_keypoints_2d[25]); // left hip
                frame[i].features[9] = new Vector2(jsonFile.people[0].pose_keypoints_2d[27], jsonFile.people[0].pose_keypoints_2d[28]); // kelft knee
                frame[i].features[10] = new Vector2(jsonFile.people[0].pose_keypoints_2d[30], jsonFile.people[0].pose_keypoints_2d[31]); // left foot
                frame[i].features[11] = new Vector2(jsonFile.people[0].pose_keypoints_2d[33], jsonFile.people[0].pose_keypoints_2d[34]); //right hip
                frame[i].features[12] = new Vector2(jsonFile.people[0].pose_keypoints_2d[36], jsonFile.people[0].pose_keypoints_2d[37]); // right knee
                frame[i].features[13] = new Vector2(jsonFile.people[0].pose_keypoints_2d[39], jsonFile.people[0].pose_keypoints_2d[40]); // right foot
                frame[i].features[14] = new Vector2(jsonFile.people[0].pose_keypoints_2d[42], jsonFile.people[0].pose_keypoints_2d[43]); // left face
                frame[i].features[15] = new Vector2(jsonFile.people[0].pose_keypoints_2d[45], jsonFile.people[0].pose_keypoints_2d[46]); // right face
                frame[i].features[16] = new Vector2(jsonFile.people[0].pose_keypoints_2d[48], jsonFile.people[0].pose_keypoints_2d[49]); // left ear
                frame[i].features[17] = new Vector2(jsonFile.people[0].pose_keypoints_2d[51], jsonFile.people[0].pose_keypoints_2d[52]); //right ear

            } else{
                frame[i] = ScriptableObject.CreateInstance<SoFrame>();
                frame[i].isUsed = false;

            }


        }
    }


    public void NormalizeVel(){
        float maxVel = 0;
   
        for (int i = 1; i < frame.Length; i++)
        {
            if(frame[i].isUsed && maxVel<frame[i].CalculateVelo(frame[i - 1]))
            maxVel = frame[i].CalculateVelo(frame[i - 1]);
          
        }
        Debug.Log(maxVel);

        for (int i = 0; i < frame.Length; i++)
        {
            if (frame[i].isUsed)
            {
                frame[i].normVelo = frame[i].velo/maxVel ;
               
            }
        }

    }

}
