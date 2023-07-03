using Binding3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachineBinding2
{
    public class MensajeManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _clave;
        private string _msgCifrado;
        private string _msgClaro;
        public EnigmaManager enigmaManager = new EnigmaManager();

        public string Clave
        {
            get 
            {
                if (_clave==null || _clave.Length != 3)
                {
                    _clave = "aaa";
                }
                   
                return _clave; 
            }
            set { _clave = value; OnPropertyChanged("MsgCifrado");}
        }
        
        public string MsgClaro
        {
            get { return _msgClaro; }
            set { _msgClaro = value; OnPropertyChanged("MsgCifrado"); }
        }

        public string MsgCifrado
        {
            get { return enigmaManager.MiCifradorEnigma(MsgClaro, Clave.ToString().ToLower()); }
            set 
            { 
                _msgClaro = enigmaManager.MiCifradorEnigma(MsgClaro, Clave.ToString().ToLower());
            }
            
        }




        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
