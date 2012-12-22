using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 文字欄位搜尋
/// </summary>
public class Search_Textfield : BackendSearchControl
{
    private string _title { get; set; }
    private string _ID { get; set; }
    private string _fieldName { get; set; }
    private string _placeHolder { get; set; }
    private StateBag _viewState { get; set; }
    private Panel _container { get; set; }

    /// <summary>
    /// 文字搜尋控制項設定
    /// </summary>
    /// <param name="vs">ViewState</param>
    /// <param name="pl">搜尋 Panel 控制項</param>
    /// <param name="Title">搜尋項目標題</param>
    /// <param name="ID">控制項 ID</param>
    /// <param name="FieldName">目標欄位名稱</param>
    /// <param name="PlaceHolder">浮水印提示文字 (選填)</param>
    public Search_Textfield(StateBag vs, Panel pl, string Title, string ID, string FieldName, string PlaceHolder = "")
    {
        _title = Title;
        _ID = ID;
        _fieldName = FieldName;
        _placeHolder = PlaceHolder;

        _viewState = vs;
        _container = pl;

        Generate();

        SearchItems.Add(new SearchItem(_ID, string.Format(" AND {0}=@0", _fieldName)));                
    }

    /// <summary>
    /// 產生控制項
    /// </summary>
    public void Generate()
    {
        _container.Controls.Add(new LiteralControl(_title));

        TextBox tb = new TextBox();
        tb.ID = _ID;
        _viewState[_ID] = tb.Text;

        if (_placeHolder != "")
        {
            tb.Attributes["placeholder"] = _placeHolder;
            tb.Attributes["class"] = "input";
        }

        _container.Controls.Add(tb);
    }

    /// <summary>
    /// 產生 SQL Statement
    /// </summary>
    /// <returns></returns>
    public override PetaPoco.Sql SQLStatement()
    {
        if (_viewState[_ID].ToString().Length > 0)
            SQL.Append(string.Format(" AND {0}=@0", _fieldName), _viewState[_ID].ToString());

        return SQL;
    }
}