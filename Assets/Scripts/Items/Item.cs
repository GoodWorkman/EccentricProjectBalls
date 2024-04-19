using UnityEngine;

public enum ItemType
{
    Empty,
    Ball,
    Barrell,
    Stone,
    Box,
    Dynamite,
    Star
}

public class Item : MonoBehaviour
{
    public ItemType ItemType;
}
