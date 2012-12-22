using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 抽獎方法結果
/// </summary>
public class DrawResult
{
    private bool _result;
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Result
    {
        get { return _result; }
        set { _result = value; }
    }

    private string _msg;
    /// <summary>
    /// 返回值
    /// </summary>
    public string Msg
    {
        get { return _msg; }
        set { _msg = value; }
    }
}
