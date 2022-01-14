using OxyPlot;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using XYZ.Infrastructure.Commands;
using XYZ.Models;
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

        #region SelectedPageIndex : int - Номер выбранной вкладки

        private int _selectedPageIndex = 0;
        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex 
        { 
            get => _selectedPageIndex; 
            set => Set(ref _selectedPageIndex, value); 
        }

        #endregion

        #region TestDataPoints : IEnumerable<DataPoint> - Тестовый набор данных для визуализации

        private IEnumerable<DataPoint> _testDataPoints;
        /// <summary>
        /// Тестовый набор данных для визуализации
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }



        #endregion

        #region PlotModel : PlotModel - Модель графика

        private PlotModel _plotModel;
        /// <summary>
        /// Модель графика
        /// </summary>
        public PlotModel PlotModel { get => _plotModel; set => Set(ref _plotModel, value);}

        #endregion


        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => _selectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            if (_selectedPageIndex == 0 && Convert.ToInt32(p) < 0) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand   = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            #endregion


            var dataPoints = new List<DataPoint>((int)(360 / 0.1));
            for (double x = 0; x <= 10; x += 0.1)
            {
                var y = Math.Sin(x);
                dataPoints.Add(new DataPoint(x, y));
            }

            PlotModel = new Plot("Prikol", dataPoints).GetPlot();

        }
    }
}
