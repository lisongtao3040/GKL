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

            this.lblMake_no.Text = ViewState["make_no"].ToString();
            this.lblCode.Text = ViewState["code"].ToString();
            this.lblUser.Text = ViewState["user_cd"].ToString();
            this.lblLine_id.Text = ViewState["line_id"].ToString() + "  ";
            this.lblUserName.Text = ViewState["user_name"].ToString();
            this.hidLineIdKey.Text = ViewState["line_id"].ToString().Trim();
            this.hidChkNo.Text = ViewState["chk_no"].ToString();
            this.hidLineId.Text = ViewState["line_id"].ToString();
            this.hidInsUser.Text = ViewState["user_cd"].ToString();


            if (System.Configuration.ConfigurationManager.AppSettings.Get("print_lines986").ToString().IndexOf(this.hidLineId.Text) > 0)
            {
                this.btnMakeQR.Visible = true;
                btnMakeQR.Attributes["onclick"] = "window.open('printLinesCodeRelation.html?chk_no=" + this.hidChkNo.Text + "');return false;";
            }
            else
            {
                this.btnMakeQR.Visible = false;
            }

            
            if (System.Configuration.ConfigurationManager.AppSettings.Get("HuJiao_lines").ToString().IndexOf(this.hidLineId.Text) > 0)
            {
                this.btnHujiao.Visible = true;
            }
            else
            {
                this.btnHujiao.Visible = false;
            }

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


            if (System.Configuration.ConfigurationManager.AppSettings.Get("YXLD_lines").ToString().IndexOf(ViewState["line_id"].ToString()) >= 0)
            {
                this.btnYXLD.Visible = true;

                TCheckResultBC tmpBc = new TCheckResultBC();

                if (tmpBc.GetYXLDResult(ViewState["make_no"].ToString().Trim()) == true)
                {
                    this.btnYXLD.BackColor = System.Drawing.Color.LightGreen;
                }
            }
            else
            {
                this.btnYXLD.Visible = false;
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

            //'明細項目設定
            MsInit();
        }

        Context.Items["menu_line_id"] = ViewState["menu_line_id"];
        Context.Items["menu_user_cd"] = ViewState["menu_user_cd"];
        Context.Items["menu_user_name"] = ViewState["menu_user_name"];
    }


    public void KoteiInit()
    {

    }

    public void MsInit()
    {

        DataTable dt = GetMsData();

        Int32 i;
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
            this.gvMs.Rows[i].Attributes.Add("DW", dt.Rows[i]["DW"].ToString());
            this.gvMs.Rows[i].Attributes.Add("DH", dt.Rows[i]["DH"].ToString());

            if (dt.Rows[i]["chk_method"].ToString() == "1")
            {
                ((TextBox)this.gvMs.Rows[i].FindControl("tbxIn1")).Attributes["typ"] = "scan";

            }

            //影像联动 左右区分
            if (dt.Rows[i]["chk_name"].ToString().Contains("左"))
            {
                gvMs.Rows[i].Attributes["lr"] = "1";
            }
            else if (dt.Rows[i]["chk_name"].ToString().Contains("右"))
            {
                gvMs.Rows[i].Attributes["lr"] = "2";
            }


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

        try
        {
            //①如果是可以报工的生产线
            //②如果不是关联托盘生产线
            //那么直接报工

            if (System.Configuration.ConfigurationManager.AppSettings.Get("baogong_lines").ToString().IndexOf(ViewState["line_id"].ToString()) >= 0
             && System.Configuration.ConfigurationManager.AppSettings.Get("tuopan_lines").ToString().IndexOf(ViewState["line_id"].ToString()) < 0
                )
            {

                //v_bg_list_new
                TCheckResultBC BC_CK = new TCheckResultBC();
                DataTable dt = (new GKL_BgDA()).SelTCheckResultOkSuu(ViewState["make_no"].ToString(), ViewState["code"].ToString(), ViewState["line_id"].ToString());

                //t_check_result 获得本次检查结果 用 chk_no
                DataTable dtRlt = BC.GetResultByChkNo(ViewState["chk_no"].ToString());

                string txt = "";

                try
                {
                    txt = "1.准备报工,v_bg_list_new:" + dt.Rows.Count.ToString() + ",t_check_result:" + dtRlt.Rows.Count.ToString();
                    if (dt.Rows.Count > 0 && dtRlt.Rows.Count > 0)
                    {
                        txt = txt + "--ok_suu:" + dt.Rows[0]["ok_suu"].ToString();
                        txt = txt + "--bg_result:" + dt.Rows[0]["bg_result"].ToString();
                    }
                }
                catch (Exception e3)
                {
                    txt = "文本准备出错" + e3.Message.Replace((char)13, (char)0).Replace((char)10, (char)0).Replace(",", "").Substring(0, 450);
                }


                try
                {
                    BC_CK.InsBaogongRireki(ViewState["chk_no"].ToString(), ViewState["make_no"].ToString(), ViewState["code"].ToString(), ViewState["line_id"].ToString(), txt);
                }
                catch (Exception e2)
                {

                }


                if (dt.Rows.Count > 0 && dtRlt.Rows.Count > 0)
                {
                    //OK 数 bg_result
                    if (Convert.ToInt32(dt.Rows[0]["ok_suu"].ToString()) >= 2 && (dt.Rows[0]["bg_result"] == "" || dt.Rows[0]["bg_result"] == "NG"))
                    {
                        //如果本次检查结果也是OK
                        if (Common.NullToEmpty(dtRlt.Rows[0]["chk_result"].ToString()) == "1")
                        {
                            BGAcion BGAcion = new BGAcion();
                            BGAcion.Pub_cd = ViewState["code"].ToString();
                            BGAcion.Pub_no = ViewState["make_no"].ToString();
                            BGAcion.Pub_User = ViewState["user_cd"].ToString();
                            BGAcion.Pub_Line = ViewState["line_id"].ToString();
                            Thread t = new Thread(new ThreadStart(BGAcion.RunBGAll));
                            t.Start();
                        }
                    }
                }
            }
        }
        catch (Exception e1)
        {

        }
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
        System.Data.DataTable dt = BC.SelTCheckResult(line_id, "", "", make_no, code);



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
                             , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
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
