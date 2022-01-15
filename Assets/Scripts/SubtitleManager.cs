using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour {
  public static SubtitleManager instance;

  private TextMeshProUGUI subtitleText;

  private void Awake() {
    instance = this;
  }

  private void Start() {
    subtitleText = GameObject.Find("SubtitleText").GetComponent<TextMeshProUGUI>();
  }

  public void ShowSubtitle(string subtitle) {
    subtitleText.text = subtitle;
    subtitleText.gameObject.SetActive(true);
  }

  public void HideSubtitle() {
    subtitleText.gameObject.SetActive(false);
  }
}