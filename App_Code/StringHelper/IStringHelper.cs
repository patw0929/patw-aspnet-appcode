using System;
using System.Collections.Generic;

using System.Text;

namespace tw.patw.StringHelper
{
    public interface IStringHelper
    {
        int getLength(char value);
        int getLength(string value);
        string Left(string value, int length);
    }
}