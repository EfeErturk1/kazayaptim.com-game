using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSceneManager : MonoBehaviour
{
    public GameObject busPrefab;
    private List<GameObject> Busses;
    public Player player;
    private int busCount;
    private List<float> busPositionsX;
    private float busPositionsZ;
    private float spawnInterval = 10f;
    private float spawnTimer = 0f;
    private String userInput;

    private const string apiUrl = "https://api.bulutfon.com/v2/pbx/call-and-bridge?apikey=OnQWrm79w8560gZkjjaw6YEOYw-eKe8yfJUe7JrFwxw3L-_ZswDUfTEG-RYXPFbMqhw";

    public static GameSceneManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        busCount = 4;
        busPositionsZ = 175.0f;
        busPositionsX = new List<float>();
        busPositionsX.Add(43.0f);
        busPositionsX.Add(34.0f);
        busPositionsX.Add(-43.0f);
        busPositionsX.Add(-34.0f);

        Busses = new List<GameObject>();
        player.Init();
        SpawnBuses();
    }
    
    public void StartGame(String userInput)
    {
        gameObject.SetActive(true);
        this.userInput = userInput;
        spawnTimer = 0f;
        
        player.Init();
        ReSpawnBuses();
    }

    public void EndGame()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.Init();
            GameManager.Instance.EndGame();
        }
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            ReSpawnBuses();
            spawnTimer = 0f;
        }
    }

    void SpawnBuses()
    {
        for (int i = 0; i < busCount; i++)
        {
            GameObject bus = Instantiate(busPrefab);
            bus.SetActive(true);
            bus.transform.position = new Vector3(busPositionsX[i], 0, busPositionsZ+i);
            bus.transform.rotation = Quaternion.Euler(0, 180, 0);
            Busses.Add(bus);
        }
    }

    void ReSpawnBuses()
    {
        for (int i = 0; i < busCount; i++)
        {
            Busses[i].transform.position = new Vector3(busPositionsX[i], 0, busPositionsZ);
            Busses[i].transform.rotation = Quaternion.Euler(0, 180, 0);
            Busses[i].SetActive(true);

        }
    }

    public void StopBusses()
    {
        foreach (var bus in Busses)
        {
            //
        }
    }
    
    public void MakeApiCall()
    {
        // Create the request body with the updated "callee" field
        string requestBody = $@"{{
        ""caller"": ""908508854145"",
        ""callee"": ""{userInput}"",
        ""destination"": ""7171"",
        ""timeout"": 0,
        ""callback_url"": ""https://webhook.site/b2dec0a0-3eaf-4a31-9203-e7fc2b4a0f22""
    }}";

        StartCoroutine(SendApiRequest(requestBody));
    }


    private IEnumerator SendApiRequest(string requestBody)
    {
        // Create the web request
        using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"API request error: {request.error}");
            }
            else
            {
                Debug.Log("API request successful");
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}
