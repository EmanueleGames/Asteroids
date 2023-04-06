using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    Toggle toggle;

    private const string AUDIO_TOGGLE = "SwitchToggle Sound";
    private const string DEBUG_TOGGLE = "SwitchToggle Debug";

    Image slideImage;
    Image dotImage;

    [SerializeField] Color slideOffColor;
    [SerializeField] Color dotOffColor;

    [SerializeField] Color slideOnColor;
    [SerializeField] Color dotOnColor;

    [SerializeField] RectTransform dotRectTransform;

    Vector2 dotPosition;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        slideImage  = dotRectTransform.parent.GetComponent<Image>();
        dotImage    = dotRectTransform.GetComponent<Image>();
        dotPosition = dotRectTransform.anchoredPosition;

        slideImage.color = slideOffColor;
        dotImage.color = dotOffColor;

        toggle.onValueChanged.AddListener(OnSwitchUI);
        
        switch (this.gameObject.name)
        {
            case AUDIO_TOGGLE:
                if (AudioManager.instance.GameMuted())
                    toggle.isOn = true;
                break;
            case DEBUG_TOGGLE:
                if (GameManager.instance.InDebugMode())
                    toggle.isOn = true;
                break;
            default:
                break;
        }

        toggle.onValueChanged.AddListener(OnSwitchValue);

        if (toggle.isOn)
            OnSwitchUI(true);
    }

    void OnSwitchUI(bool on)
    {
        dotRectTransform.anchoredPosition = on ? dotPosition * -1 : dotPosition;
        slideImage.color = on ? slideOnColor : slideOffColor;
        dotImage.color = on ? dotOnColor : dotOffColor;
    }

    void OnSwitchValue(bool on)
    {
        switch (this.gameObject.name)
        {
            case AUDIO_TOGGLE:
                AudioManager.instance.ToggleSound(on);
                break;
            case DEBUG_TOGGLE:
                GameManager.instance.ToggleDebugMode(on);
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitchUI);
        toggle.onValueChanged.RemoveListener(OnSwitchValue);
    }
}
