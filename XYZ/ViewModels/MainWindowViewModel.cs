using XYZ.ViewModels.Base;

namespace XYZ.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Title : string - Заголовок окна
        private string _title = "Анализ статистики XYZ";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Status : string - Статус программы
        private string _status = "Готов!";
        /// <summary>Статус Програмы</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }
        #endregion
    }
}
