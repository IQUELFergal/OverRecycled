using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private Score scoreLevel;

    private void Awake()
    {
        scoreLevel = GameObject.FindObjectOfType<Score>();
    }
    public void Save()
    {
        Debug.Log("Saving");
        //create a file or open a file to save to
        FileStream file = new FileStream(Application.persistentDataPath + "/Score.dat", FileMode.OpenOrCreate);
        //allows us to write data to a file
        BinaryFormatter formatter = new BinaryFormatter();
        //serialization method to write to the file
        //formatter.Serialize(file, scoreLevel.score);

        file.Close();
    }

    public void Load() 
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Score.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        //scoreLevel.score = (Score) formatter.Deserialize(file);
        file.Close();

    }
}
