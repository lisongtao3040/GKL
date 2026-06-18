using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using System.Threading;

public partial class t_check_ms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblMsg.Text = "";

        Dictionary<string, string> dicPjnms = new Dictionary<string, string>();

        if (!IsPostBack)
        {

            ViewState["menu_line_id"] = Common.NullToEmpty(Context.Items["menu_line_id"]);
            ViewState["menu_user_cd"] = Common.NullToEmpty(Context.Items["menu_user_cd"]);
            ViewState["menu_user_name"] = Common.NullToEmpty(Context.Items["menu_user_name"]);

            lblSuu.Text = Common.NullToEmpty(Context.Items["suu"]);
            lblSuu2.Text = Common.NullToEmpty(Context.Items["suu"]);

            ViewState["make_no"] = Context.Items["make_no"];
            ViewState["code"] = Context.Items["code"];
            ViewState["line_id"] = Context.Items["line_id"];
            ViewState["user_cd"] = Context.Items["user_cd"];
            ViewState["chk_no"] = Context.Items["chk_no"];
            ViewState["user_name"] = Context.Items["user_name"];
            ViewState["chk_date"] = Context.Items["chk_date"];
            ViewState["isAllLine"] = Context.Items["isAllLine"];

            this.lblMake_no.Text = ViewState["make_no"].ToString();
            this.lblCode.Text = ViewState["code"].ToString();
            this.lblUser.Text = ViewState["user_cd"].ToString();
            this.lblLine_id.Text = ViewState["line_id"].ToString() + "  ";
            this.lblUserName.Text = ViewState["user_name"].ToString();
            this.hidLineIdKey.Text = ViewState["line_id"].ToString().Trim();
            this.hidChkNo.Text = ViewState["chk_no"].ToString();
            this.hidLineId.Text = ViewState["line_id"].ToString();
            this.hidInsUser.Text = ViewState["user_cd"].ToString();


            if (System.Configuration.ConfigurationManager.AppSettings.Get("autoColor_lines").ToString().IndexOf(this.hidLineId.Text) > 0)
            {
                autoColor_flg.Value = "1";
            }
            else
            {
                autoColor_flg.Value = "0";
            }

            if (System.Configuration.ConfigurationManager.AppSettings.Get("camera").ToString().IndexOf(ViewState["line_id"].ToString()) >= 0)
            {
                ViewState["camera_flg"] = "1";
            }
            else
            {
                ViewState["camera_flg"] = "0";
            }
            //报工系统是否打开
            BaoGongDA BaoGongDA = new BaoGongDA();
            hidBaogong.Text = "0";
            if (System.Configuration.ConfigurationManager.AppSettings.Get("baogong_lines").ToString().IndexOf(ViewState["line_id"].ToString()) > 0)
            {
                if (BaoGongDA.IsBaogongSysOn() == true)
                {
                    hidBaogong.Text = "1";
                }
            }

            if (System.Configuration.ConfigurationManager.AppSettings.Get("tuopan_lines").ToString().IndexOf(ViewState["line_id"].ToString()) > 0)
            {
                hidTuopanLines.Text = "1";
            }
            else
            {
                hidTuopanLines.Text = "0";
            }

            camera_flg.Value = ViewState["camera_flg"].ToString();

            //'固定項目設定
            KoteiInit();


            TCheckMsBC BC = new TCheckMsBC();
            DataTable dt = BC.SelTCheckMs(ViewState["chk_no"].ToString(), ViewState["line_id"].ToString());

            int pjIdx = 0;
            int i;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (!dicPjnms.ContainsKey(dt.Rows[i]["project_name"].ToString()))
                {
                    dicPjnms.Add(dt.Rows[i]["project_name"].ToString(), pjIdx.ToString());
                    pjIdx++;
                }
            }

            ViewState["dicPjnms"] = dicPjnms;

            InitLinkbuttons();

            ClickKindNameLink(PanelLinks.Controls[0]);

            string project_name = ((LinkButton)PanelLinks.Controls[0]).Text.Trim();

            ViewState["project_name"] = project_name;

            MsInit();
            //'明細項目設定
            //MsInit();

        }
        else
        {
            InitLinkbuttons();
        }

        Context.Items["menu_line_id"] = ViewState["menu_line_id"];
        Context.Items["menu_user_cd"] = ViewState["menu_user_cd"];
        Context.Items["menu_user_name"] = ViewState["menu_user_name"];
    }

    public void InitLinkbuttons()
    {
        if (System.Configuration.ConfigurationManager.AppSettings.Get("Pre_allOK_chked_lines").ToString().IndexOf(ViewState["line_id"].ToString()) > 0)
        {
            TCheckMsBC BC = new TCheckMsBC();
            DataTable dt = BC.SelTCheckMs(ViewState["chk_no"].ToString(), ViewState["line_id"].ToString());
            string preItemKey = "";
            Dictionary<string, string> dicPjnms = (Dictionary<string, string>)ViewState["dicPjnms"];
            foreach (var item in dicPjnms)
            {
                // If project_name <> dt.Rows(i).Item("project_name").ToString Then
                LinkButton lnk = new LinkButton();
                lnk.Text = item.Key;
                lnk.CssClass = "link_btn";
                lnk.Click += Link_Click;

                if (Convert.ToInt32(item.Value) > 0)
                {
                    if (dt.Select("project_name='" + preItemKey + "' and chk_result='OK'").Length == dt.Select("project_name='" + preItemKey + "'").Length)
                    {

                    }
                    else
                    {
                        //'lnk.Enabled = false;

                        lnk.Attributes["onclick"] = "event.preventDefault();return false;";

                        //lnk.Attributes["readonly"] = "readonly";
                        //lnk.Attributes["disabled"] = "true";
                    }
                    preItemKey = item.Key;

                }
                else
                {
                    preItemKey = item.Key;
                }

                PanelLinks.Controls.Add(lnk);
            }
        }
        else
        {
            Dictionary<string, string> dicPjnms = (Dictionary<string, string>)ViewState["dicPjnms"];
            foreach (var item in dicPjnms)
            {
                LinkButton lnk = new LinkButton();
                lnk.Text = item.Key;
                lnk.CssClass = "link_btn";
                lnk.Click += Link_Click;
                PanelLinks.Controls.Add(lnk);
            }

        }

    }

    // 種類Click
    public void Link_Click(object sender, System.EventArgs e)
    {
        string project_name = ((LinkButton)sender).Text.Trim();
        ClickKindNameLink(sender);

        ViewState["project_name"] = project_name;
        //System.Data.DataTable dt = DA.GetCheckMs(CLoginInfo, CLoginInfo.ck_id, kind_name);
        MsInit();
        //PageCom.SetLinkKindName(CLoginInfo.user_cd, kind_name);
    }

    public void ClickKindNameLink(object sender)
    {
        TCheckMsBC BC = new TCheckMsBC();
        DataTable dt = BC.SelTCheckMs(ViewState["chk_no"].ToString(), ViewState["line_id"].ToString());

        for (int i = 0; i <= PanelLinks.Controls.Count - 1; i++)
        {
            ((LinkButton)PanelLinks.Controls[i]).Font.Bold = false;
            //((LinkButton)PanelLinks.Controls[i]).ForeColor = PageCom.GetLinkColor(dt, (LinkButton)PanelLinks.Controls(i).Text.Trim.Trim.Split(":")(1));
            ((LinkButton)PanelLinks.Controls[i]).Font.Size = 30;

            ((LinkButton)PanelLinks.Controls[i]).CssClass = "link_btn";

            if (dt.Select("project_name='" + ((LinkButton)PanelLinks.Controls[i]).Text + "' and chk_result='NG'").Length > 0)
            {
                ((LinkButton)PanelLinks.Controls[i]).ForeColor = System.Drawing.Color.Red;
            }
            else if (dt.Select("project_name='" + ((LinkButton)PanelLinks.Controls[i]).Text + "' and chk_result='OK'").Length == dt.Select("project_name='" + ((LinkButton)PanelLinks.Controls[i]).Text + "'").Length)
            {
                ((LinkButton)PanelLinks.Controls[i]).ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ((LinkButton)PanelLinks.Controls[i]).ForeColor = System.Drawing.Color.Black;
            }

        }


        ((LinkButton)sender).Font.Bold = true;
        ((LinkButton)sender).Font.Size = 35;

        ((LinkButton)sender).CssClass = "link_btn sel_link_btn";
        hidYXLD_START_TIME.Value = "";
        //if (((LinkButton)sender).Text.Contains("捆包") && hidPlanLineId.Text== "SRM1532B")
        if (((LinkButton)sender).Text.Contains("全检") && hidPlanLineId.Text == "SRM1532B")
        {

            //this.lblMake_no.Text = ViewState["make_no"].ToString();
            //this.lblCode.Text = ViewState["code"].ToString();
            //this.lblUser.Text = ViewState["user_cd"].ToString();
            btnZB.Style["display"] = "";

            if (Common.YXLD_INFOS.ContainsKey(ViewState["user_cd"].ToString()))
            {

                if (Common.YXLD_INFOS[ViewState["user_cd"].ToString()] ==
                    ViewState["make_no"].ToString() + "|" + ViewState["code"].ToString() + "|" + hidPlanLineId.Text)
                {

                    //TCheckMsBC BC2 = new TCheckMsBC();
                    hidYXLD_START_TIME.Value = BC.GetDbDate();
                }
            }
        }
        else
        {

            btnZB.Style["display"] = "none";
        }

    }



    public void KoteiInit()
    {

    }

    public void MsInit()
    {
        Int32 i;
        DataTable dtIn = GetMsData();

        DataTable dt = new DataTable();
        dt = dtIn.Copy();
        dt.Rows.Clear();

        string color_nm = "";
        string midcode = "";
        string wenlu = "";
        string dh = "";
        string dw = "";
        string sw = "";
        string dingfansize = "";

        if (dtIn.Rows.Count > 0)
        {

            color_nm = dtIn.Rows[0]["color_nm"].ToString();
            midcode = dtIn.Rows[0]["midcode"].ToString();
            wenlu = dtIn.Rows[0]["wenlu"].ToString();
            dh = dtIn.Rows[0]["dh"].ToString();
            dw = dtIn.Rows[0]["dw"].ToString();
            sw = dtIn.Rows[0]["sw"].ToString();
            dingfansize = dtIn.Rows[0]["dingfansize"].ToString();
            hidPlanLineId.Text = dtIn.Rows[0]["line_id"].ToString();
        }





        for (i = 0; i <= dtIn.Rows.Count - 1; i++)
        {
            if (ViewState["project_name"].ToString() == dtIn.Rows[i]["project_name"].ToString())
            {
                dt.ImportRow(dtIn.Rows[i]);
            }

        }


        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {

            //dt.Rows[i]["kj_explain_Expr"] = dt.Rows[i]["kj_explain_Expr"].ToString().Replace("1", "a").Replace("2", "b").Replace("3", "c").Replace("4", "d").Replace("5", "e").Replace("6", "f").Replace("7", "g").Replace("8", "h").Replace("9", "i").Replace("0", "j");

            if (dt.Rows[i]["chk_name"].ToString() == ("卡尺(基準値±工差以内)")
                || dt.Rows[i]["chk_name"].ToString() == ("卷尺(基準値±工差以内)")
                || dt.Rows[i]["chk_name"].ToString() == ("卡尺")
                || dt.Rows[i]["chk_name"].ToString() == ("卷尺"))
            {
                dt.Rows[i]["kj_explain_Expr"] = "";
            }
            else
            {



            }
            dt.Rows[i]["kj_explain_Expr"] = dt.Rows[i]["kj_explain_Expr"].ToString()
            .Replace("{midcode}", midcode)
            .Replace("{wenlu}", wenlu)
            .Replace("{dh}", dh)
            .Replace("{dw}", dw)
            .Replace("{sw}", sw)
            .Replace("{dingfansize}", dingfansize);

            dt.Rows[i]["kj_0"] = dt.Rows[i]["kj_0"].ToString()
                .Replace("{midcode}", midcode)
                .Replace("{wenlu}", wenlu)
                .Replace("{dh}", dh)
                .Replace("{dw}", dw)
                .Replace("{sw}", sw)
                .Replace("{dingfansize}", dingfansize);


            dt.Rows[i]["kj_0_Expr"] = dt.Rows[i]["kj_0_Expr"].ToString()
                .Replace("{midcode}", midcode)
                .Replace("{wenlu}", wenlu)
                .Replace("{dh}", dh)
                .Replace("{dw}", dw)
                .Replace("{sw}", sw)
                .Replace("{dingfansize}", dingfansize);

            dt.Rows[i]["chk_formula"] = dt.Rows[i]["chk_formula"].ToString()
                .Replace("{midcode}", midcode)
                .Replace("{wenlu}", wenlu)
                .Replace("{dh}", dh)
                .Replace("{dw}", dw)
                .Replace("{sw}", sw)
                .Replace("{dingfansize}", dingfansize);

        }



        this.gvMs.DataSource = dt;
        this.gvMs.DataBind();


        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            this.gvMs.Rows[i].Attributes.Add("chk_id", dt.Rows[i]["chk_id"].ToString());
            this.gvMs.Rows[i].Attributes.Add("kj_0", dt.Rows[i]["kj_0_Expr"].ToString());
            this.gvMs.Rows[i].Attributes.Add("kj_1", dt.Rows[i]["kj_1_Expr"].ToString());
            this.gvMs.Rows[i].Attributes.Add("kj_2", dt.Rows[i]["kj_2_Expr"].ToString());
            this.gvMs.Rows[i].Attributes.Add("chk_method_id", dt.Rows[i]["chk_method_id"].ToString());
            this.gvMs.Rows[i].Attributes.Add("chk_method", dt.Rows[i]["chk_method"].ToString());
            this.gvMs.Rows[i].Attributes.Add("chk_formula", dt.Rows[i]["chk_formula"].ToString());
            this.gvMs.Rows[i].Attributes.Add("pic_id", dt.Rows[i]["pic_id"].ToString());

            this.gvMs.Rows[i].Attributes.Add("color_nm", color_nm);
            this.gvMs.Rows[i].Attributes.Add("midcode", midcode);
            this.gvMs.Rows[i].Attributes.Add("wenlu", wenlu);
            this.gvMs.Rows[i].Attributes.Add("dh", dh);
            this.gvMs.Rows[i].Attributes.Add("dw", dw);
            this.gvMs.Rows[i].Attributes.Add("sw", sw);
            this.gvMs.Rows[i].Attributes.Add("dingfansize", dingfansize);

            this.gvMs.Rows[i].Attributes.Add("chk_km_name", dt.Rows[i]["chk_km_name"].ToString());


            //string midcode;
            //string wenlu;
            //string dh;
            //string dw;
            //string sw;
            //string dingfansize;

            if (dt.Rows[i]["chk_method"].ToString() == "1")
            {
                ((TextBox)this.gvMs.Rows[i].FindControl("tbxIn1")).Attributes["typ"] = "scan";

            }

            //chk_method


        }

        MergeGridViewCell.MergeRow(this.gvMs, 0, 1);

    }

    DataTable GetMsData()
    {
        TCheckMsBC BC = new TCheckMsBC();
        return BC.SelTCheckMs(ViewState["chk_no"].ToString(), ViewState["line_id"].ToString());
    }

    private string cleanString(string newStr)
    {
        string tempStr = newStr.Replace((char)13, (char)0);
        return tempStr.Replace((char)10, (char)0);
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        //更新明细数据
        TCheckMsBC BC = new TCheckMsBC();
        BC.UpdTCheckResultMS(ViewState["chk_no"].ToString(), ViewState["line_id"].ToString(), ViewState["user_cd"].ToString());

        Context.Items["make_no"] = ViewState["make_no"];
        Context.Items["code"] = ViewState["code"];
        Context.Items["line_id"] = ViewState["line_id"];
        Context.Items["user_cd"] = ViewState["user_cd"];
        Context.Items["chk_no"] = ViewState["chk_no"];
        Context.Items["user_name"] = ViewState["user_name"];
        Context.Items["chk_date"] = ViewState["chk_date"];
        Server.Transfer("CheckItiran.aspx");
    }
    protected void btnModoru_Click(object sender, EventArgs e)
    {
        Context.Items["make_no"] = ViewState["make_no"];
        Context.Items["code"] = ViewState["code"];
        Context.Items["line_id"] = ViewState["line_id"];
        Context.Items["user_cd"] = ViewState["user_cd"];
        Context.Items["chk_no"] = ViewState["chk_no"];
        Context.Items["user_name"] = ViewState["user_name"];
        Context.Items["chk_date"] = ViewState["chk_date"];
        Server.Transfer("CheckItiran.aspx");
    }

    protected void btnSinki_Click(object sender, EventArgs e)
    {


        string make_no = this.tbxMakeNo_key.Value;
        string code = this.tbxCode_key.Value;
        //string suu = this.hidSuu.Text;
        string line_id = ViewState["line_id"].ToString();
        string loginLine_id = line_id;
        Context.Items["chk_date"] = ViewState["chk_date"];
        Context.Items["make_no"] = make_no;
        Context.Items["code"] = code;
        Context.Items["line_id"] = line_id;
        Context.Items["user_cd"] = this.ViewState["user_cd"];
        Context.Items["user_name"] = this.lblUserName.Text;


        TCheckResultBC BC = new TCheckResultBC();
        System.Data.DataTable dt = BC.SelTCheckResult(line_id, "", "", make_no, code, Convert.ToBoolean(ViewState["isAllLine"]));



        Int32 i, mxChkTimes, chkTimes, idx;
        mxChkTimes = 0;
        idx = 0;

        string yotei_chk_date;
        DataRow[] drs;

        string tmp_chk_no;

        ///sp = chr("_");

        if (dt.Rows.Count > 0)
        {
            yotei_chk_date = dt.Rows[0]["yotei_chk_date"].ToString();
            drs = dt.Select("yotei_chk_date='" + yotei_chk_date + "'");
            for (i = 0; i <= drs.Length - 1; i++)
            {
                chkTimes = Convert.ToInt32(dt.Rows[i]["chk_times"].ToString());
                if (mxChkTimes < chkTimes || chkTimes == 0)
                {
                    mxChkTimes = chkTimes;
                    idx = i;
                }
            }

            if (drs[idx]["temp_id"].ToString().Trim() == "")
            {
                Common.ShowMsg(this.Page, "检查模板不存在");
                return;
            }

            string[] sArray = drs[idx]["chk_no"].ToString().Split('_');

            tmp_chk_no = sArray[0] + "_" + sArray[1] + "_" + (mxChkTimes + 1).ToString();

            BC.InsTCheckResult(tmp_chk_no
                             , System.DateTime.Now.Year.ToString()
                             , (mxChkTimes + 1).ToString()
                             , drs[idx]["plan_no"].ToString()
                             , drs[idx]["line_id"].ToString()
                             , loginLine_id
                             , drs[idx]["make_no"].ToString()
                             , drs[idx]["code"].ToString()
                             , drs[idx]["suu"].ToString()
                             , drs[idx]["temp_id"].ToString()
                             , drs[idx]["chk_result"].ToString()
                             , this.ViewState["user_cd"].ToString()
                             , drs[idx]["yotei_chk_date"].ToString()
                             , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                             , ""
                             , ""
                             , "0"
                             , this.ViewState["user_cd"].ToString()
                             , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), Convert.ToBoolean(ViewState["isAllLine"])
                             );

            Context.Items["chk_no"] = tmp_chk_no;

            Server.Transfer("t_check_ms.aspx");

        }
        else
        {
            Common.ShowMsg(this.Page, "检查计划数据不存在;");
            return;
        }
    }
}




/// <summary>
/// 合并GridView单元格
/// </summary>
public class MergeGridViewCell
{

    #region Public

    /// <summary>
    /// GridView合并行
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="startCol">开始列（索引从0开始）</param>
    /// <param name="endCol">结束列</param>
    public static void MergeRow(GridView gv, int startCol, int endCol)
    {
        if (startCol < 0)
            throw new ArgumentOutOfRangeException("startCol", "开始列不能小于0");
        if (endCol < 0)
            throw new ArgumentOutOfRangeException("endCol", "结束列不能小于0");
        if (startCol > endCol)
            throw new ArgumentException("开始列不能小于结束列");

        var init = new RowArg()
        {
            StartRowIndex = 0,
            EndRowIndex = gv.Rows.Count - 2
        };
        for (int i = startCol; i < endCol + 1; i++)
        {
            if (i > 0)
            {
                var list = new List<RowArg>();
                //从第二列开始就要遍历前一列
                IteratePrevCol(gv, i - 1, list);
                foreach (var item in list)
                {
                    MergeRow(gv, i, item.StartRowIndex, item.EndRowIndex);
                }
            }
            //合并开始列的行
            else
            {
                MergeRow(gv, i, init.StartRowIndex, init.EndRowIndex);
            }
        }
    }

    /// <summary>
    /// 合并GridView单元格
    /// </summary>
    /// <param name="gv">要合并的GridView</param>
    /// <param name="cols">制定的列</param>
    public static void MergeRow(GridView gv, params int[] cols)
    {
        if (cols.Any(t => t < 0))
        {
            throw new ArgumentOutOfRangeException("参数中不能包含小于0列");
        }
        var init = new RowArg()
        {
            StartRowIndex = 0,
            EndRowIndex = gv.Rows.Count - 2
        };

        for (int i = 0; i < cols.Length; i++)
        {
            if (i > 0)
            {
                var list = new List<RowArg>();
                //从第二列开始就要遍历前一列
                IteratePrevCol(gv, cols[i - 1], list);
                foreach (var item in list)
                {
                    MergeRow(gv, cols[i], item.StartRowIndex, item.EndRowIndex);
                }
            }
            //合并开始列的行
            else
            {
                MergeRow(gv, i, init.StartRowIndex, init.EndRowIndex);
            }
        }
    }

    /// <summary>
    /// 和并列
    /// </summary>
    /// <param name="gv">要合并的GridView</param>
    /// <param name="startCol">开始列的索引</param>
    /// <param name="endCol">结束列的索引</param>
    /// <param name="containHeader">是否合并表头，默认不合并</param>
    public static void MergeColumn(GridView gv, int startCol, int endCol, bool containHeader)
    {
        if (containHeader)
        {
            IterateRowCells(gv.HeaderRow, startCol, endCol);
        }
        foreach (GridViewRow row in gv.Rows)
        {
            IterateRowCells(row, startCol, endCol);
        }
    }

    #endregion


    #region Private

    /// <summary>
    /// 合并单列的行
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="currentCol">当前列</param>
    /// <param name="startRow">开始合并的行索引</param>
    /// <param name="endRow">结束合并的行索引</param>
    private static void MergeRow(GridView gv, int currentCol, int startRow, int endRow)
    {
        for (int rowIndex = endRow; rowIndex >= startRow; rowIndex--)
        {
            GridViewRow currentRow = gv.Rows[rowIndex];
            GridViewRow prevRow = gv.Rows[rowIndex + 1];
            if (currentRow.Cells[currentCol].Text != "" && currentRow.Cells[currentCol].Text != " ")
            {
                if (currentRow.Cells[currentCol].Text == prevRow.Cells[currentCol].Text)
                {
                    currentRow.Cells[currentCol].RowSpan = prevRow.Cells[currentCol].RowSpan < 1 ? 2 : prevRow.Cells[currentCol].RowSpan + 1;
                    prevRow.Cells[currentCol].Visible = false;
                }
            }
        }
    }

    /// <summary>
    /// 遍历GridViewRow中的单元格
    /// </summary>
    /// <param name="row">要遍历的行</param>
    /// <param name="start">开始索引</param>
    /// <param name="end">结束索引</param>
    private static void IterateRowCells(GridViewRow row, int start, int end)
    {
        //从开始索引的下一列开始
        for (int i = start + 1; i <= end; i++)
        {
            //当前单元格
            TableCell currCell = row.Cells[i];
            //前一个单元格
            TableCell prevCell = row.Cells[i - 1];
            if (!string.IsNullOrEmpty(currCell.Text) && !string.IsNullOrEmpty(prevCell.Text))
            {
                if (currCell.Text == prevCell.Text)
                {
                    currCell.ColumnSpan = prevCell.ColumnSpan < 1 ? 2 : prevCell.ColumnSpan + 1;
                    prevCell.Visible = false;
                }
            }
        }
    }

    /// <summary>
    /// 遍历前一列
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="prevCol">当前列的前一列</param>
    /// <param name="list"></param>
    private static void IteratePrevCol(GridView gv, int prevCol, List<RowArg> list)
    {
        if (list == null)
        {
            list = new List<RowArg>();
        }
        foreach (GridViewRow row in gv.Rows)
        {
            if (!row.Cells[prevCol].Visible)
                continue;
            list.Add(new RowArg
            {
                StartRowIndex = row.RowIndex,
                EndRowIndex = row.RowIndex + row.Cells[prevCol].RowSpan - 2
            });
        }
    }

    class RowArg
    {
        public int StartRowIndex { get; set; }
        public int EndRowIndex { get; set; }
    }

    #endregion
}
