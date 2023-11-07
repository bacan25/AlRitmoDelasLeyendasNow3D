#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SongParser : MonoBehaviour
{
    public string filePath;  // Ruta del archivo para leer
    public SongAsset songAsset;  // Referencia a tu ScriptableObject

    void Start()
    {

        ParseSong(filePath);

    }

    void ParseSong(string path)
    {
        string fileContents;
        AudioClip audioClip = null;  // Reemplaza null con tu AudioClip

        using (StreamReader reader = new StreamReader(path))
        {
            fileContents = reader.ReadToEnd();
        }

        // Para extraer el nombre de la canción
        Match nameMatch = Regex.Match(fileContents, @"Name\s*=\s*""(.*?)""");
        string songName = "";
        if (nameMatch.Success)
        {
            songName = nameMatch.Groups[1].Value;
            Debug.Log("Name: " + songName);
        }

        // Para extraer la sección ExpertSingle y obtener los valores necesarios
        Match expertSingleMatch = Regex.Match(fileContents, @"\[ExpertSingle\]\s*\{(.*?)\}", RegexOptions.Singleline);
        List<Note> notesList = new List<Note>();
        if (expertSingleMatch.Success)
        {
            string expertSingleSection = expertSingleMatch.Groups[1].Value;

            foreach (Match lineMatch in Regex.Matches(expertSingleSection, @"(\d+)\s*=\s*N\s*(\d+)"))
            {
                int timeInTicks = int.Parse(lineMatch.Groups[1].Value);
                int directionValue = int.Parse(lineMatch.Groups[2].Value);
                Direction direction = (Direction)(directionValue);

                // Asumiendo que bpm y resolution están definidos y son correctos
                float bpm = 80;  // Ajusta esto según tu canción
                float resolution = 192;  // Ajusta esto según tu canción

                float timeInSeconds = (timeInTicks / resolution) * (60 / bpm);

                Note newNote = new Note
                {
                    time = timeInSeconds,
                    direction = direction
                };
                notesList.Add(newNote);
            }
        }

        // Crear un nuevo SongAsset
        SongAsset newSongAsset = ScriptableObject.CreateInstance<SongAsset>();

        // Configurar las propiedades del nuevo SongAsset
        newSongAsset.song = new Song
        {
            Name = songName,
            //audioClip = audioClip,
            notes = notesList.ToArray()
        };

        // Guardar el nuevo SongAsset
        //SaveSongAsset(newSongAsset);
    }
#if UNITY_EDITOR
    void SaveSongAsset(SongAsset asset)
{
    Debug.Log("Testeo");
    string path = "Assets/Resources/Songs/" + asset.song.Name + ".asset";
    // ...resto del código...

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}