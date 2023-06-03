using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject background;
    public GameObject logo;
    private string userInput;

    private bool showUserInputText = true;
    private bool checkForPassword = true;
    private String password = "111718"; 

    public void Start()
    {
        background.SetActive(true);
        logo.SetActive(true);
        userInput = String.Empty;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        background.SetActive(true);
        logo.SetActive(true);
        userInput = "90";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (userInput.Length > 0)
                {
                    userInput = userInput.Substring(0, userInput.Length - 1);
                }
            }
            else
            {
                char keyPressed = Input.inputString[0];
                if (char.IsNumber(keyPressed) && userInput.Length < 12)
                {
                    userInput += keyPressed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (checkForPassword)
                {
                    if (userInput == password)
                    {
                        checkForPassword = false;
                        userInput = "90";
                    }
                }
                else
                {
                    GameManager.Instance.StartGame(userInput);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            userInput = string.Empty;
        }
    }

    void OnGUI()
    {
        if (showUserInputText)
        {
            // Define the style for the label
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.font = Resources.Load<Font>("Fonts/YourDesiredFont");
            style.fontSize = 30;
            style.alignment = TextAnchor.MiddleCenter;

            // Calculate the position for the label to be centered on the screen
            Rect labelRect = new Rect(0, 0, Screen.width, Screen.height);
            labelRect.center = new Vector2(Screen.width / 2, Screen.height / 2 + 130);

            // Display the user input on the screen with the updated style and position
            if (checkForPassword)
            {
                GUI.Label(labelRect, "Şifre\n" + userInput, style);
            }
            else
            {
                GUI.Label(labelRect, "Araba Simulasyonu\n\nLütfen Telefon numaranızı giriniz\n" + userInput, style);
            }
        }
    }
}
