using TMPro;
using UnityEngine;

public class ReportMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Initialize(string message)
    {
        _text.text = message;
    }
    
    private void Start()
    {
        Destroy(gameObject, 2.4f);
    }
}