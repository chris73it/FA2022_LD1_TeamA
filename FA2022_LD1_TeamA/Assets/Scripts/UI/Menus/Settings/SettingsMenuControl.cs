using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuControl : MonoBehaviour
{
    public int PreviousMenu;
    public SliderInt Music;
    public SliderInt Sound;
    public Button Exit;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Music = root.Q<SliderInt>("Music");
        Sound = root.Q<SliderInt>("Sound");
        Exit = root.Q<Button>("ExitGame");

        Music.value = GameManager.Instance.MusicVolume;
        Sound.value = GameManager.Instance.SoundVolume;

        Music.RegisterValueChangedCallback(v =>
        {
            GameManager.Instance.MusicVolume = v.newValue;
        });

        Sound.RegisterValueChangedCallback(v =>
        {
            GameManager.Instance.SoundVolume = v.newValue;
        });

        Exit.clicked += ExitPressed;
    }

    private void ExitPressed()
    {
        GameManager.Instance.MenuState = (GameManager.MenuStates)PreviousMenu;
        Destroy(gameObject);
    }
}
