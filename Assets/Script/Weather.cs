using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weather : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        fetchTemperatureBtnClick();
    }

    public void fetchTemperatureBtnClick()
    {
        StartCoroutine(getLocation());
    }

    IEnumerator getLocation()
    {
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            ToastShow.ShowToastMsg("Location not enabled by user");
            Debug.Log("Location not enabled by user");
            yield break;
        }

        // Start location service
        Input.location.Start(1f, 1f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            ToastShow.ShowToastMsg("Location timeout");
            Debug.Log("Location timeout");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            ToastShow.ShowToastMsg("Unable to determine device location");
            Debug.Log("Unable to determine device location");
            yield break;
        }

        // ✅ Success
        LocationInfo location = Input.location.lastData;

        Debug.Log("Latitude: " + location.latitude);
        Debug.Log("Longitude: " + location.longitude);
        Debug.Log("Altitude: " + location.altitude);
        Debug.Log("Accuracy: " + location.horizontalAccuracy);
        Debug.Log("Timestamp: " + location.timestamp);

        StartCoroutine(GetWeather(location.latitude, location.longitude));
        // Stop service if not needed continuously
        Input.location.Stop();
    }

    IEnumerator GetWeather(float latitude,float longitude)
    {
        string url = "https://api.open-meteo.com/v1/forecast?latitude="+latitude+ "&longitude=" + longitude + "&timezone=auto&daily=temperature_2m_max";
           UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Response: " + request.downloadHandler.text);

            // Parse JSON
            WeatherResponse data =
                JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);

            float[] TodayTemp= data.daily.temperature_2m_max;
            System.Array.Sort(TodayTemp);
            float MinTemp = TodayTemp[0];
            float MaxTemp = TodayTemp[TodayTemp.Length - 1];
            ToastShow.ShowToastMsg("Min Temperature: " + MinTemp + "°C"+ " and Max Temperature: "+ MaxTemp + " at you location");
        }
        else
        {
            ToastShow.ShowToastMsg("API Error: " + request.error);
           Debug.LogError("API Error: " + request.error);
        }
    }
}

[System.Serializable]
public class WeatherResponse
{
    public Daily daily;
}

[System.Serializable]
public class Daily
{
    public float[] temperature_2m_max;
}

