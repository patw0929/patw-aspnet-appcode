using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SearchItem
{
    public SearchItem(string ControlName, string SQLStatement)
    {
        this._controlName = ControlName;
        this._sqlStatement = SQLStatement;
    }

    private string _controlName;
    public string ControlName
    {
        get { return _controlName; }
        set { _controlName = value; }
    }

    private string _sqlStatement;
    public string SQLStatement
    {
        get { return _sqlStatement; }
        set { _sqlStatement = value; }
    }
}
