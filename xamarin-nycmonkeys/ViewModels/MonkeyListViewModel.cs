using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

using xamarinnycmonkeys.Models;

namespace xamarinnycmonkeys.ViewModels
{
    public class MonkeyListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Monkey> MonkeyList { get; set; }

        public MonkeyListViewModel()
        {
            MonkeyList = new ObservableCollection<Monkey>();
        }

        private bool _busy = false;

        public bool IsBusy
        {
            get { return _busy; }

            set
            {
                if (_busy == value)
                    return;

                _busy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public async Task GetMonkeysAsync()
        {
            try
            {
                IsBusy = true;

                // Get some data from montemagno.com
                var client = new HttpClient();
                var json = await client.GetStringAsync("http://montemagno.com/monkeys.json");

                // Deserialize that data
                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Monkey>>(json);

                // Put monkeys in the list
                foreach(var item in items)
                {
                    MonkeyList.Add(item);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;   

        #endregion

        public void OnPropertyChanged(string name)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(name));
        }
    }
}

