using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad  {
    public static PlayerProgression m_progresson;

    public void Save(PlayerProgression progresson)
    {
        Debug.Log("Saving");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/progression.gd");
        bf.Serialize(file, progresson);
        file.Close();
    }

    public PlayerProgression Load()
    {
        if (File.Exists(Application.persistentDataPath + "/progression.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/progression.gd", FileMode.Open);
            SaveLoad.m_progresson = (PlayerProgression)bf.Deserialize(file);
            file.Close();

            return m_progresson;
        }
        else
        {
            Debug.Log("File not Found");
            return null;
        }
    }
}
