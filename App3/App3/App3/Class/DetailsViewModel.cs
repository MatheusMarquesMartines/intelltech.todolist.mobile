using System.ComponentModel;

public class DetailsViewModel : INotifyPropertyChanged
{
    string _title, surname;

    public string title
    {
        get
        {
            return _title;
        }
        set
        {
            if (_title != value)
            {
                _title = value;
                OnPropertyChanged("title");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        var changed = PropertyChanged;
        if (changed != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}