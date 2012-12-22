using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;

public class BackendSearchControl
{
    public PetaPoco.Sql SQL = PetaPoco.Sql.Builder;

    public virtual PetaPoco.Sql SQLStatement()
    {
        return SQL;
    }

    /// <summary>
    /// 搜尋的集合控制項
    /// </summary>
    public IList<SearchItem> SearchItems = new List<SearchItem>();

    /// <summary>
    /// 將搜尋控制項的值轉換為 SQL Statement
    /// </summary>
    /// <param name="sql">PetaPoco SQL</param>
    /// <param name="SearchItems">用於搜尋的控制項集合</param>
    /// <param name="plSearch">搜尋區塊控制項</param>
    /// <param name="ViewState">ViewState</param>
    public static void ConvertControlToSQL(PetaPoco.Sql sql, IList<SearchItem> SearchItems, Panel plSearch, StateBag ViewState)
    {
        foreach (SearchItem item in SearchItems)
        {
            if (((Panel)plSearch).FindControl(item.ControlName).GetType() == typeof(CheckBox))
            {
                if ((bool)ViewState[item.ControlName])
                {
                    sql.Append(item.SQLStatement, ViewState[item.ControlName]);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState[item.ControlName])))
                {
                    sql.Append(item.SQLStatement, ViewState[item.ControlName]);
                }
            }
        }
    }


    /// <summary>
    /// 將控制項的值放至 ViewState
    /// </summary>
    /// <param name="ViewState"></param>
    /// <param name="sender"></param>
    public static void Control_Binding(StateBag ViewState, object sender)
    {
        Panel Search = (Panel)sender;

        foreach (Control Control in Search.Controls)
        {
            if (Control is TextBox) // 文字輸入框
            {
                TextBox Object = (TextBox)Control;
                ViewState[Object.ID] = Object.Text;
            }
            else if (Control is DropDownList) // 下拉選單
            {
                DropDownList Object = (DropDownList)Control;
                ViewState[Object.ID] = Object.SelectedValue;
            }
            else if (Control is CheckBox) // 核取方塊
            {
                CheckBox Object = (CheckBox)Control;
                ViewState[Object.ID] = Object.Checked;
            }
        }
    }
}
