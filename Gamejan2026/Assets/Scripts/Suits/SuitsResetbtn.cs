using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuitsResetbtn : MonoBehaviour
{
    [SerializeField] private SuitsManager suitsManager;
    private GameObject[] _suits;

    public void ResetSuits()
    {
        suitsManager.AllSuitsOnReach(-4);
        for(int i = 0; _suits[i]; i++)
        {
            _suits[i].GetComponent<SuitCollision>();
            _suits[i].transform.position = Vector3.zero;
        }
    }
}
