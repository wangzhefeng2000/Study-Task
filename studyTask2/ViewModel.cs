using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace studyTask2
{
    class ViewModel : BindableBase
    {
        private int _step;

        public int Step
        {
            get { return _step; }
            set { SetProperty(ref _step, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message;}
            set { SetProperty(ref _message, value); }
        }
    }
}
