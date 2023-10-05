using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JellyType
{
    basic,
    yellowbear,
    pinkbear,
    magnet,
    giant,
    boost,
    healing
}
[CreateAssetMenu(fileName = "Jelly Name", menuName = "Scriptable Object/Jelly Data", order = int.MaxValue)]
public class JellyData : ScriptableObject
{
    public string JellyID => name;
    [SerializeField]
    private uint score;
    public uint Score => score;
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;
    public JellyType type;
}
