using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData[] itemDropList;

    public void GenerateDrop()
    {
        for(int i = 0; i < itemDropList.Length; i++)
        {
            DropItem(itemDropList[i]);
        }
    }

    private void DropItem(ItemData _itemData)
    {
        Debug.Log(_itemData.name);
        var instantiateDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        int xRandomRange = Random.Range(-5, 5);
        int yRandomRange = Random.Range(10, 15);
        Vector2 randomVelocity = new Vector2(xRandomRange, yRandomRange);
        instantiateDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
