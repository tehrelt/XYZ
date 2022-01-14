using XYZ.ViewModels.Base;

namespace XYZ.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _title = "Анализ статистики XYZ";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
