using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/frame", order = 1)]
[System.Serializable]
public class SoFrame : ScriptableObject
{

    public Vector2[] features = new Vector2[18];
    public float velo;
    public bool isUsed = true;
    public float normVelo;
    public float aCC;
    public Vector2[] simi = new Vector2[1000];
    public Vector2 orginialRoot = new Vector2(0,0);
    public bool isTurnFrame = false;


    public int[] best = new int[] { 999, 999, 999 ,999};
    public int[] bestTurn = new int[] { 999, 999, 999, 999 };


    public void ResetRootMotion()
    {
        orginialRoot = features[1];


        for (int i = 0; i < features.Length; i++)
        {

            if (0 >= features[i].x - Mathf.Epsilon && 0 <= features[i].x + Mathf.Epsilon && 0 >= features[i].y - Mathf.Epsilon && 0 <= features[i].y + Mathf.Epsilon){}
            else
            {
                features[i] = features[i] - orginialRoot;
            }
        }


    }

    public float CalculateVelo(SoFrame prevFrame)
    {
        //calculate BEFORE root motion reset!
        if (prevFrame.isUsed)
            return velo = ((features[1].x - prevFrame.features[1].x));
        else return 0;
    }

    public void CalculateACC(SoFrame prevFrame)
    {
        //calculate BEFORE root motion reset!
        if (prevFrame.isUsed)
             aCC = velo - prevFrame.velo;
       
    }


    public void CalculateSim(SoFrame[] frameArray, int index)
    {
        
        //calculate AFTER root motion reset!

        for (int i = 0; i < frameArray.Length; i++)
            {

            if (frameArray[i].isUsed && index !=i)
            {

                simi[i] += features[0] - frameArray[i].features[0];
                simi[i] += features[1] - frameArray[i].features[1];
                simi[i] += features[2] - frameArray[i].features[2];
                simi[i] += features[3] - frameArray[i].features[3];
                simi[i] += features[4] - frameArray[i].features[4];
                simi[i] += features[5] - frameArray[i].features[5];
                simi[i] += features[6] - frameArray[i].features[6];
                simi[i] += features[7] - frameArray[i].features[7];
                simi[i] += features[8] - frameArray[i].features[8];
                simi[i] += features[9] - frameArray[i].features[9];
                simi[i] += features[10] - frameArray[i].features[10];
                simi[i] += features[11] - frameArray[i].features[11];
                simi[i] += features[12] - frameArray[i].features[12];
                simi[i] += features[13] - frameArray[i].features[13];
                simi[i] += features[14] - frameArray[i].features[14];
                simi[i] += features[15] - frameArray[i].features[15];
                simi[i] += features[16] - frameArray[i].features[16];
                simi[i] += features[17] - frameArray[i].features[17];
            } else
            {
                simi[i] = new Vector2(100, 100);
            }
        }

       
     

        for (int i = 0; i < simi.Length; i++)
        {
            if (simi[best[0]].magnitude > simi[i].magnitude && !frameArray[i].isTurnFrame)
            {
                best[0] = i;   
            } else if (simi[best[1]].magnitude > simi[i].magnitude && !frameArray[i].isTurnFrame)
            {
                best[1] = i;   
            } else if (simi[best[2]].magnitude > simi[i].magnitude && !frameArray[i].isTurnFrame)
            {
                best[2] = i;
            }else if (simi[best[3]].magnitude > simi[i].magnitude && !frameArray[i].isTurnFrame)
            {
                best[3] = i;
            }

        }

        for (int i = 0; i < simi.Length; i++)
        {
            if (simi[bestTurn[0]].magnitude > simi[i].magnitude && frameArray[i].isTurnFrame && Mathf.Sign(normVelo)==Mathf.Sign(frameArray[i].normVelo) )
            {
                bestTurn[0] = i;
            }
            else if (simi[bestTurn[1]].magnitude > simi[i].magnitude && frameArray[i].isTurnFrame && Mathf.Sign(normVelo) == Mathf.Sign(frameArray[i].normVelo))
            {
                bestTurn[1] = i;
            }
            else if (simi[bestTurn[2]].magnitude > simi[i].magnitude && frameArray[i].isTurnFrame && Mathf.Sign(normVelo) == Mathf.Sign(frameArray[i].normVelo))
            {
                bestTurn[2] = i;
            }
            else if (simi[bestTurn[3]].magnitude > simi[i].magnitude && frameArray[i].isTurnFrame && Mathf.Sign(normVelo) == Mathf.Sign(frameArray[i].normVelo))
            {
                bestTurn[3] = i;
            }

        }

    }

    public int RandomClosestFrame(){

    
        return bestTurn[Random.Range(0, 4)];
 
    }


    public int RandomClosestFrameNoTurn()
    {


        return best[Random.Range(0, 4)];

    }

   
    

    public void TurnFrame(SoFrame[] frameArray, int index)
    {
        
        if(Mathf.Sign(normVelo) != Mathf.Sign(frameArray[index + 10].normVelo) && normVelo != 0 && frameArray[index + 10].normVelo !=0)
        {
            isTurnFrame = true;

        }


    }



    public void PrintSimiVelo(SoFrame[] frameArray){

        Debug.Log(frameArray[bestTurn[0]].normVelo + " " + frameArray[bestTurn[1]].normVelo + 
                  " " + frameArray[bestTurn[2]].normVelo + " " + frameArray[bestTurn[3]].normVelo);
    }

}