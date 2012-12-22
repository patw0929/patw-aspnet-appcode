using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 排除重複
/// </summary>
public class Search_Distinct : BackendSearchControl
{
    private string _title { get; set; }
    private string _tableName { get; set; }
    private string _pkFieldName { get; set; }
    private string _ID { get; set; }
    private string _fieldName { get; set; }
    private StateBag _viewState { get; set; }
    private Panel _container { get; set; }

    /// <summary>
    /// 排除重複控制項設定
    /// </summary>
    /// <param name="vs">ViewState</param>
    /// <param name="pl">搜尋 Panel 控制項</param>
    /// <param name="Title">搜尋項目標題</param>
    /// <param name="TableName">表名</param>
    /// <param name="ID">控制項 ID</param>
    /// <param name="FieldName">目標欄位名稱</param>
    public Search_Distinct(StateBag vs, Panel pl, string Title, string TableName, string PKFieldName, string ID, string FieldName)
    {
        _title = Title;
        _tableName = TableName;
        _pkFieldName = PKFieldName;
        _ID = ID;
        _fieldName = FieldName;

        _viewState = vs;
        _container = pl;

        Generate();

        SearchItems.Add(new SearchItem(_ID, string.Format(" AND {1} IN (SELECT MAX({1}) FROM {0} GROUP BY {2})", _tableName, _pkFieldName, _fieldName)));
    }

    /// <summary>
    /// 產生控制項
    /// </summary>
    public void Generate()
    {
        CheckBox cb = new CheckBox();
        cb.ID = _ID;
        cb.Text = _title;
        _viewState[_ID] = cb.Checked;

        _container.Controls.Add(cb);
    }

    /// <summary>
    /// 產生 SQL Statement
    /// </summary>
    /// <returns></returns>
    public override PetaPoco.Sql SQLStatement()
    {
        if (_container.FindControl(_ID) != null)
        {
            if ((bool)_viewState[_ID])
            {
                SQL.Append(string.Format(" AND {1} IN (SELEDCT MAX({1}) FROM {0} GROUP BY {2}", _tableName, _pkFieldName, _fieldName), _viewState[_ID].ToString());
            }
        }

        return SQL;
    }
}