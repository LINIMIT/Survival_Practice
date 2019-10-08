using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;  // Image는 canvas안에서만 사용가능 Sprite는 월드상에 띄울수있음
    public GameObject itemPrefab;

    public string weaponType;//무기 유형

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }


    void Start()
    {
      
    }

    void Update()
    {

    }
}
