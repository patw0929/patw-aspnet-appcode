using System.Web.UI;
using System.Web.UI.WebControls;
using System;

/// <summary>
/// 日期範圍搜尋
/// </summary>
public class Search_Date : BackendSearchControl
{
    private string _title { get; set; }
    private string _dateToolTip { get; set; }
    private string _datetimeFieldName { get; set; }
    private string _startDateID { get; set; }
    private int _startDateMaxLength { get; set; }
    private string _endDateID { get; set; }
    private int _endDateMaxLength { get; set; }
    private StateBag _viewState { get; set; }
    private Panel _container { get; set; }

    /// <summary>
    /// 日期搜尋控制項設定
    /// </summary>
    /// <param name="vs">ViewState</param>
    /// <param name="pl">搜尋 Panel 控制項</param>
    /// <param name="Title">搜尋項目標題</param>
    /// <param name="ToolTip">用於產生 jQuery DatePicker 的 title 值</param>
    /// <param name="DatetimeFieldName">目標欄位名稱</param>
    /// <param name="StartDateID">起始日期控制項名稱</param>
    /// <param name="EndDateID">結束日期控制項名稱</param>
    /// <param name="StartDateMaxLength">起始日期最大長度限制，預設為 10</param>
    /// <param name="EndDateMaxLength">結束日期最大長度限制，預設為 10</param>
    public Search_Date(StateBag vs, Panel pl, string Title, string ToolTip, string DatetimeFieldName, string StartDateID, string EndDateID, int StartDateMaxLength = 10, int EndDateMaxLength = 10)
    {
        _title = Title;
        _dateToolTip = ToolTip;
        _datetimeFieldName = DatetimeFieldName;
        _startDateID = StartDateID;
        _endDateID = EndDateID;
        _startDateMaxLength = StartDateMaxLength;
        _endDateMaxLength = EndDateMaxLength;

        _viewState = vs;
        _container = pl;

        Generate();
    }

    /// <summary>
    /// 產生控制項
    /// </summary>
    public void Generate()
    {
        _container.Controls.Add(new LiteralControl(_title));
        
        // Start Date
        _container.Controls.Add(new TextBox
        {
            ID = _startDateID,
            ToolTip = _dateToolTip,
            MaxLength = _startDateMaxLength
        });

        // ~
        _container.Controls.Add(new Label { Text = "~" });

        // End Date
        _container.Controls.Add(new TextBox
        {
            ID = _endDateID,
            ToolTip = _dateToolTip,
            MaxLength = _endDateMaxLength
        });

        SearchItems.Add(new SearchItem(_startDateID, string.Format(" AND {0} >= @0", _datetimeFieldName)));
        SearchItems.Add(new SearchItem(_endDateID, string.Format(" AND {0} < DateAdd(d, 1, @0)", _datetimeFieldName)));
    }

    /// <summary>
    /// 產生 SQL Statement
    /// </summary>
    /// <returns></returns>
    public override PetaPoco.Sql SQLStatement()
    {
        if (ValidatorFuncs.IsValidDate((string)_viewState[_startDateID])) // 起始日期
        {
            SQL.Append(string.Format(" AND {0} >= @0", _datetimeFieldName), _viewState[_startDateID].ToString());
        }

        if (ValidatorFuncs.IsValidDate((string)_viewState[_endDateID])) // 結束日期
        {
            SQL.Append(string.Format(" AND {0} < DateAdd(d, 1, @0)", _datetimeFieldName), _viewState[_endDateID].ToString());
        }

        return SQL;
    }
}