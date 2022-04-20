using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Media;
using JetBrains.Annotations;
using SchemaTester.Common;
using SchemaTester.Data;
using SchemaTester.Logic;
using SchemaTester.Models;
using SchemaTester.TestDataHelper;

namespace SchemaTester.ViewModels {
    internal sealed class MainWindowViewModel : INotifyPropertyChanged {
        private Geometry _pathSchema;
        private IReadOnlyList<FillIModel> _fills;
        private FillIModel _selectedFill;
        private const string TestDataFileName = @".\test.tst";

        public MainWindowViewModel() {
            SampleDataHelper.MakeTestData(TestDataFileName);
            LoadDataCommand = new SimpleCommand(LoadTestData);
        }

        public ICommand LoadDataCommand { get; }

        public Geometry PathSchema {
            get => _pathSchema;
            private set {
                _pathSchema = value;
                OnPropertyChanged();
            }
        }

        public IReadOnlyList<FillIModel> Fills {
            get => _fills;
            private set {
                _fills = value;
                OnPropertyChanged();
            }
        }

        public FillIModel SelectedFill {
            get => _selectedFill;
            set {
                _selectedFill = value;
                OnPropertyChanged();
            }
        }

        private void LoadTestData() {
            using var file = File.OpenRead(TestDataFileName);
            var data = JsonSerializer.Deserialize<Schema>(file);
            var (lines, fills) = data.ToGeometry();
            PathSchema = lines;
            Fills = fills;
            SelectedFill = Fills.FirstOrDefault();
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
