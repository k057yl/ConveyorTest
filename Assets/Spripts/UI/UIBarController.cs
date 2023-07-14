using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class UIBarController : MonoBehaviour
{
    [SerializeField] private Text _targetText;
    [SerializeField] private Text _currentText;
    [SerializeField] private Text _winText;
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.gameObject.SetActive(false);
        _winText.gameObject.SetActive(false);
    }

    public void UpdateText(string target, int current)
    {
        _targetText.text = target;
        _currentText.text = current.ToString();
    }
    
    public void UpdateScore(int current)
    {
        _currentText.text = current.ToString();
    }

    public void Win()
    {
        _winText.gameObject.SetActive(true);
        _button.gameObject.SetActive(true);
    }
}
