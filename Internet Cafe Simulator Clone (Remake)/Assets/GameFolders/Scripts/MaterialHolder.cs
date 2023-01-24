using UnityEngine;

public class MaterialHolder : Singleton<MaterialHolder>
{
    public Material GreenPreview , RedPreview;

    public Material Monitor_Off;
    public Material GetRandomMonitorMaterial() => Monitor_On_List[Random.Range(0, Monitor_On_List.Length)];


    public Material[] Monitor_On_List;
}