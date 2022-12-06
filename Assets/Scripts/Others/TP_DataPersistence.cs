using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_DataPersistence : MonoBehaviour
{
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public bool GetBool(string key)
    {
        return bool.Parse(PlayerPrefs.GetString(key));
    }


    // S E T T E R S

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public void SetBool(string key, bool value)
    {
        
        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
    }

    ///////////////

    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
