using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

// NOTE : Using the Newtonsoft.Json because the Unity's JsonUtility does not support deserialization of dictionaries 
using Newtonsoft.Json;


public class APIHandeler : Singelton<APIHandeler>
{
    public string apiUrl;

    public JsonData data = new JsonData();

    private void Start()
    {
        MakeAPICall();
    }

    public void MakeAPICall()
    { 
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(apiUrl);
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());

        data = JsonConvert.DeserializeObject<JsonData>(reader.ReadToEnd());

        if (data.data == null) print("dictionary is null");
        else print(data.data.Count);

        UIManagerTO.Instance.PopulateTheScrollbar(data);
    }

}

# region ROOT CLASSES FOR JSON 

[System.Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
    public string name; 
}

[System.Serializable]
public class Data
{
    public string address;
    public string name;
    public int points;
}

[System.Serializable]
public class JsonData
{
    public List<Client> clients;
    public Dictionary<string, Data> data;
    public string label;
}

#endregion
