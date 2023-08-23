using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;                   // Reference to the Button component.
    [SerializeField] private TextMeshProUGUI buttonText;      // Reference to the TextMeshProUGUI component for button text.
    [SerializeField] private TextMeshProUGUI text;            // Reference to the TextMeshProUGUI component for header text.
    [SerializeField] private Sprite playSprite;               // Sprite for the play button state.
    [SerializeField] private Sprite lockedSprite;             // Sprite for the locked button state.
    private bool disablePlayButton;                          // Flag to disable play button functionality.

    private const string playButtonString = "PLAY";          // Text for the play button.
    private const string lockedButtonString = "LOCKED";      // Text for the locked button.

    // Initialize the LevelButton.
    public void Init(Action onClick, int LevelID, bool isInteractable, LevelController.LevelData info)
    {
        string headerText = $"LEVEL {LevelID + 1}";
        text.text = headerText;

        if (isInteractable && !disablePlayButton)
        {
            // Add a listener to the button's onClick event.
            button.onClick.AddListener(() => onClick?.Invoke());
            SetButtonProps(playSprite, playButtonString);
        }
        else
        {
            SetButtonProps(lockedSprite, lockedButtonString);
        }
    }

    // Set the properties of the button.
    private void SetButtonProps(Sprite sprite, string text)
    {
        button.image.sprite = sprite;
        buttonText.text = text;
    }

    // Remove all listeners when the button is disabled.
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
