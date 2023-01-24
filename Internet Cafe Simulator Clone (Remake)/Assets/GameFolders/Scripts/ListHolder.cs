using UnityEngine;
using System.Collections.Generic;

public class ListHolder : MonoBehaviour
{
    #region Singleton
    public static ListHolder Instance { get; private set; }
    #endregion

    #region Lists 

    public List<DeskManager> AvailableDesks = new();
    public int AvailableDesksCount => AvailableDesks.Count;


    //public List<DeskManager> NotavailableDesk = new();
    //public int NotavailableCount => NotavailableDesk.Count;

    #endregion

    private void Awake()
    {
        Instance = this;
    }
}