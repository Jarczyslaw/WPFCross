using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCross.UI.GlobalExceptions
{
    public interface IGlobalExceptionHandler
    {
        void HandleException(string source, Exception exception);
    }
}
