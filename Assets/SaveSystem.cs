using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
      public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveRoom data = new SaveRoom(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveRoom LoadPlayer()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveRoom data = formatter.Deserialize(stream) as SaveRoom;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Sem Save File na pasta:" + path);
            return null;
        }
    }

}
