using _Source.Configs;
using _Source.Enums;
using UnityEngine;
using Zenject;

namespace _Source.UiControllers
{
    public class Reporter : MonoBehaviour
    {
        [Inject] private ReportsConfig _reportsConfig;
        [SerializeField] private ReportMessage _reportMessagePrefab;
        [SerializeField] private Transform _root;
        
        public void Report(ReportType type)
        {
            var messageObj = Instantiate(_reportMessagePrefab, _root);
            messageObj.Initialize(_reportsConfig.GetMessage(type).GetLocalizedString());
        }
    }
}