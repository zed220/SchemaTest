using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SchemeTester.Common;
using SchemeTester.Data;
using SchemeTester.Logic;
using SchemeTester.TestDataHelper;

namespace SchemeTester.ViewModels {
    internal sealed class MainWindowViewModel : INotifyPropertyChanged {
        private Geometry _pathScheme;
        private IReadOnlyList<Tuple<string, Geometry>> _fills;
        private Tuple<string, Geometry> _selectedFill;
        private const string TestDataFileName = @".\test.tst";

        public MainWindowViewModel() {
            SampleDataHelper.MakeTestData(TestDataFileName);
            LoadDataCommand = new SimpleCommand(LoadTestData);
        }

        public ICommand LoadDataCommand { get; }

        public Geometry PathScheme {
            get => _pathScheme;
            private set {
                _pathScheme = value;
                OnPropertyChanged();
            }
        }

        public IReadOnlyList<Tuple<string, Geometry>> Fills {
            get => _fills;
            private set {
                _fills = value;
                OnPropertyChanged();
            }
        }

        public Tuple<string, Geometry> SelectedFill {
            get => _selectedFill;
            set {
                _selectedFill = value;
                OnPropertyChanged();
            }
        }

        private void LoadTestData() {
            using var file = File.OpenText(TestDataFileName);
            var data = (Scheme)JsonSerializer.CreateDefault().Deserialize(file, typeof(Scheme));
            PathScheme = PathBuilder.DataToGeometry(data);
            Fills = PathBuilder.DataToGeometryFill(data).Select(x => new Tuple<string, Geometry>(x.Key, x.Value)).ToList();
            SelectedFill = Fills.FirstOrDefault();
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
