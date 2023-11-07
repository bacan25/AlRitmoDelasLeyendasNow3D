using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Song
{
    public string Name;
    public AudioClip audioClip;
    public Note[] notes;
    // ...otros miembros y métodos...
}

[System.Serializable]
public class Note
{
    public float time;
    public Direction direction;  // Cambio de int a Direction
    /*...*/
}

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class SongAsset : ScriptableObject
{
    public Song song;
    public AudioClip audioClip;
    public Note[] notes;
    // ...otros miembros y métodos...
}
