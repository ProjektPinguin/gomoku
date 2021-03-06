﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GomokuBoardManager : MonoBehaviour {

    [SerializeField]
    private GameObject pionPrefab;
    [SerializeField]
    private GameObject parent;
    public List<GameObject> PionList = new List<GameObject>();
    public List<int> PionListTmp = new List<int>();
    // Use this for initialization
    void Start () {
        CreateBoard();


    }
    public void CreateBoard()
    {
        int id = 0;
        for (float i = 0.5f; i >= -0.5f; i -= 0.0555f)
        {
            for (float j = -0.5f; j <= 0.5f; j += 0.0555f)
            {
                GameObject tmp;
                tmp = (GameObject)GameObject.Instantiate(pionPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0));
                tmp.transform.parent = parent.transform;
                tmp.transform.localPosition = new Vector3(j, i, -0.6f);
                tmp.transform.localScale = pionPrefab.transform.localScale;
                tmp.transform.localRotation = Quaternion.Euler(90, 0, 0);
                PionList.Add(tmp);
                tmp.GetComponent<GomokuPion>().id = id++;
            }
        }
    }

    public void CreatePion(int x, int y,int player)
    {
        PionList[(x * 19) + y].GetComponent<GomokuPion>().InvokePion(player);
    }
    public void CreatePion(int pos, int player)
    {
        PionList[pos].GetComponent<GomokuPion>().InvokePion(player);
    }
    public void DeletePion(int x, int y)
    {
        PionList[(x * 19) + y].GetComponent<GomokuPion>().KillPion();
    }
    public void DeletePion(int pos)
    {
        PionList[pos].GetComponent<GomokuPion>().KillPion();
    }

    public void ReturnToTmp()
    {
        int i = 0;
        if (PionListTmp.Count > 0)
        {
            foreach (GameObject val in PionList)
            {
                if (PionListTmp[i] >= 0 && !val.GetComponent<GomokuPion>()._isOnBoard)
                {
                    val.GetComponent<GomokuPion>().InvokePion(PionListTmp[i]);
                }
                else if (PionListTmp[i] == -1 && val.GetComponent<GomokuPion>()._isOnBoard)
                {
                    val.GetComponent<GomokuPion>().KillPion();
                }
                i++;
            }
        }
    }
    // Update is called once per frame
    void Update () {

	}
}
