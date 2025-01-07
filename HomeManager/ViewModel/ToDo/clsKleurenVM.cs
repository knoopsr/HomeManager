using HomeManager.Common;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace HomeManager.ViewModel;

public class clsKleurenVM : clsCommonModelPropertiesBase
{
    private object _selectedColor;

    // ObservableCollection voor databinding met de ComboBox
    public ObservableCollection<ColorItem> Colors { get; set; }

    // Geselecteerde kleur
    public object SelectedColor
    {
        get => _selectedColor;
        set
        {
            _selectedColor = value;
            OnPropertyChanged(); // Notificeer de View over de wijziging
        }
    }

    public clsKleurenVM()
    {
        LoadColors();
    }

    private void LoadColors()
    {
        Colors = new ObservableCollection<ColorItem>(
            typeof(Colors).GetProperties()
                          .Select(c => new ColorItem
                          {
                              Name = c.Name,
                              Color = (Color)c.GetValue(null)
                          }));
    }
}

public class ColorItem
{
    public string Name { get; set; }
    public Color Color { get; set; }
}