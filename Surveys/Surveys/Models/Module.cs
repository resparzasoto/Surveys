using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Surveys.Models
{
    public class Module
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public ICommand LoadModuleCommand { get; set; }
    }
}
