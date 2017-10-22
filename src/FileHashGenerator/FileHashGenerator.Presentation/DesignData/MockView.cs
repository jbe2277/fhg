using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;

namespace Waf.FileHashGenerator.Presentation.DesignData
{
    public class MockView : IView
    {
        public object DataContext { get; set; }
    }
}
