using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	public string itemName;
	public int width = 1;
	public int height = 1;

	public Sprite itemIcon;

	public bool canBeRotated;

	public int moneyValue;

	public GameObject itemPrefab;
	public Vector3 oldItemPosition;

	public void InitItem(string itemName, int width, int height, Sprite itemIcon, bool canBeRotated, int moneyValue, GameObject itemPrefab)
	{
		this.itemName = itemName;
		this.width = width;
		this.height = height;
		this.itemIcon = itemIcon;
		this.canBeRotated = canBeRotated;
		this.moneyValue = moneyValue;
	
		this.itemPrefab = itemPrefab;
	}
}
