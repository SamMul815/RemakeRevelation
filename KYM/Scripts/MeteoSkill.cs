using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSkill : MonoBehaviour {

    public float widthSize = 10.0f;
    public float heightSize = 10.0f;
    public int width = 10;
    public int height = 10;

    public List<Vector3> MeteoPosList;
    public List<Vector3> rndList;
    //public List
	// Use this for initialization
	void Start ()
    {
        MeteoPosList = new List<Vector3>();

        Vector3 pos = this.transform.position;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float x = i * widthSize + widthSize * 0.5f;
                float z = j * heightSize + heightSize * 0.5f;
                MeteoPosList.Add(pos + new Vector3(x, 0.0f, z));
            }
        }
        //RndInput();
    }

    public Vector3 GetRndPos()
    {

        if(rndList.Count <= 0)
        {
            RndInput();
        }

        int rnd = Random.Range(0, rndList.Count - 1);

        Vector3 retPos = rndList[rnd];
        rndList.RemoveAt(rnd);

        return retPos;
    }

    int GetPlayerIndex()
    {
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 startPos = this.transform.position;

        float x = playerPos.x - startPos.x;
        float z = playerPos.z - startPos.z;

        if (x > widthSize * width || x < 0) return -1;
        if (z > heightSize * height || z < 0) return -1;
 
        int index = (int)(x / widthSize) * height + (int)(z / heightSize);
        return index;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = this.transform.position;
        for(int i = 0; i<width; i++)
        {
            for(int j = 0; j<height; j++)
            {
                float x = i * widthSize + widthSize * 0.5f;
                float z = j * heightSize + heightSize * 0.5f;

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(pos + new Vector3(x, 0, z),new Vector3(widthSize * 0.99f,0.5f,heightSize*0.99f));
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(pos + new Vector3(x, 0, z), 2.0f);
                
            }
        }
    }

    void RndInput()
    {
        for(int i = 0; i<MeteoPosList.Count; i++)
        {
            rndList.Add(MeteoPosList[i]);
        }
    }
}
