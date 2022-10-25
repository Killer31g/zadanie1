using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace zadanie_na_ocene
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region prop
        public string nameValue;
        public string ageValue;
        private string _nameValueProp;
        public string NameValueProp
        {
            get
            {
                return _nameValueProp;
            }
            set
            { 
                _nameValueProp = value;
            }
        }
        private string _ageValueProp;
        public string AgeValueProp
        {
            get
            {
                return _ageValueProp;
            }
            set
            {
                _ageValueProp = value;
            }
        }
        private string _result;
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        private string _ageIsLegal;
        public string AgeIsLegal
        {
            get
            {
                return _ageIsLegal;
            }
            set
            {
                _ageIsLegal = value;
                OnPropertyChanged(nameof(AgeIsLegal));
            }
        }
        private string _validationNameProp;
        public string ValidationNameProp
        {
            get
            {
                return _validationNameProp;
            }
            set
            {
                _validationNameProp = value;
                OnPropertyChanged(nameof(ValidationNameProp));
            }
        }
        private string _validationAgeProp;
        public string ValidationAgeProp
        {
            get
            {
                return _validationAgeProp;
            }
            set
            {
                _validationAgeProp = value;
                OnPropertyChanged(nameof(ValidationAgeProp));
            }
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        #region buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nameValue = TextBoxNameValue.Text;
            ageValue = TextBoxAgeValue.Text;
            if (Validate(ageValue, nameValue, out string message))
            {
                ShowResult(ageValue, nameValue);
                TextBlockValidationAge.Text = "";
                TextBlockValidationName.Text = "";
            }
            else
            {
                TextBlockValidationName.Text = message;
                TextBlockAgeLegalResult.Text = "";
                TextBlockResult.Text = "";
            }
        }
        private void Button_Click_Bind(object sender, RoutedEventArgs e)
        {
            if (Validate(AgeValueProp, NameValueProp,out string message))
            {
                ShowResultBinding(AgeValueProp, NameValueProp);
                ValidationAgeProp = "";
                ValidationNameProp = "";
            }
            else
            {
                ValidationNameProp = message;
                AgeIsLegal = "";
                Result = "";
            }
        }
        #endregion
        #region valida
        public bool Validate(string age, string name,out string message)
        {
            if (CheckNameValue(name, out message) && CheckAgeValue(age, out message))
                return true;
            else
                return false;

        }
        public void ShowResultBinding(string age, string name)
        {
            if (IsAgeLegal(age))
                AgeIsLegal = "jestes pelnoletni";
            else
                AgeIsLegal = "jestes nie pelnoletni";
            Result= "Witaj " + name + "\nMasz " + age + " lat";

        }
        public void ShowResult(string age, string name)
        {
            if (IsAgeLegal(age))
                TextBlockAgeLegalResult.Text = "Jestes pelnoletni";
            else
                TextBlockAgeLegalResult.Text = "Jestes nie pelnoletni";
            TextBlockResult.Text = "Witaj " + name + "\nMasz " + age + " lat";
        }
        public bool IsAgeLegal(string age)
        {
            int ageInt = int.Parse(age);
            if (ageInt > 17)
                return true;
            else
                return false;
        }
        public bool CheckNameValue(string nameValue,out string message)
        {
            if (CheckValueIsNotEmpty(nameValue,out message) && CheckNameValueIsCorrect(nameValue,out message))
                return true;
            else
                return false;
        }
        public bool CheckValueIsNotEmpty(string value,out string message)
        {
            message = "";
            if (value == null)
            {
                message = "sprawdz czy wszytskie pola są uzupelnione";
                return false;
            }
            value = value.Trim();
            if (value.Length == 0)
            {
                message = "sprawdz czy wszytskie pola są uzupelnione";
                return false;
            }
                 
            return true;
        }
        public bool CheckNameValueIsCorrect(string nameValue,out string message)
        {
            message = "";
            char[] nameValueSigns = nameValue.ToCharArray();
            for (int nameValueSingsLength = 0; nameValueSingsLength < nameValueSigns.Length; nameValueSingsLength++)
            {
                if (char.IsDigit(nameValueSigns[nameValueSingsLength]))
                {
                    message = "nieprawidlowa wartosc imienia";
                    return false;
                }
            }
            return true;
        }
        public bool CheckAgeValue(string ageValue,out string message)
        {
            if (CheckValueIsNotEmpty(ageValue,out message)&& CheckAgeValueIsCorrect(ageValue,out message)&& CheckAgeValueIsPossible(ageValue,out message))
                return true;
            else
                return false;
        }
        public bool CheckAgeValueIsCorrect(string ageValue,out string message)
        {
            message = "";
            char[] ageValueSigns = ageValue.ToCharArray();
            int numberOfValidatedAgeSigns = 0;

            for (int ageValueSingsLength = 0; ageValueSingsLength < ageValueSigns.Length; ageValueSingsLength++)
            {
                if (char.IsDigit(ageValueSigns[ageValueSingsLength]))
                {
                    numberOfValidatedAgeSigns++;
                }
            }
            if (numberOfValidatedAgeSigns == ageValueSigns.Length)
                return true;
            else
            {
                message = "nie prawidlowa wartosc wieku";
                return false;
            }
        }
        public bool CheckAgeValueIsPossible(string ageValue,out string message)//spr wiek jest z przedziału
        {
            message = "";
            int intAgeValue = int.Parse(ageValue);
            if (intAgeValue < 0)
            {
                message = "Twój wiek musi wynosić minimum 0lat";
                return false;
            }
            else if (intAgeValue > 150)
            {
                message = "Maksymalny przewidziany wiek wynosi 150 lat";
                return false;
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
