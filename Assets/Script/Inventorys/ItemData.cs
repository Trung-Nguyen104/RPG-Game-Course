using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material, 
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public string itemID;

    protected StringBuilder sb = new();

    public virtual string GetDescription()
    {
        return "";
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
