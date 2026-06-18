using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;

public partial class CheckItiran : System.Web.UI.Page
{
    private string G_line_id
    {
        get { return this.ddl_lines.SelectedValue; }
        set { this.ddl_lines.SelectedValue = value; this.tbxLineId_key.Text = value; }
    }
    /// <summary>
    /// PAGE LOAD
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["isAllLine"] = false;


            //2024-02-12
            System.Data.DataTable dtLines = (new MUserBC()).SelLineIds();
            ddl_lines.Items.Clear();
            ddl_lines.Items.Add("");
            for (int i = 0; i <= dtLines.Rows.Count - 1; i++)
            {
                ddl_lines.Items.Add(dtLines.Rows[i][0].ToString());
            }

            ViewState["menu_line_id"] = Common.NullToEmpty(Context.Items["menu_line_id"]);
            ViewState["menu_user_cd"] = Common.NullToEmpty(Context.Items["menu_user_cd"]);
            ViewState["menu_user_name"] = Common.NullToEmpty(Context.Items["menu_user_name"]);
            ViewState["menu_kengen"] = Common.NullToEmpty(Context.Items["menu_kengen"]);

            ddl_lines.Enabled = ("1" == ViewState["menu_kengen"].ToString());
            hidOldChkNo.Text = Common.NullToEmpty(Context.Items["chk_no"]);
            ViewState["chk_no"] = "";

            if (ViewState["menu_user_cd"] == "")
            {
                Server.Transfer("Default.aspx");
            }
            else
            {
                this.tbxCheckUser.Text = ViewState["menu_user_cd"].ToString();
                G_line_id = ViewState["menu_line_id"].ToString();
                //有权限管理所有
                if (System.Configuration.ConfigurationManager.AppSettings.Get("Chk_AllLines_lines").ToString().IndexOf(G_line_id) >= 0)
                {
                    ViewState["isAllLine"] = true;
                }

                this.tbxLineId_key.Text = ViewState["menu_line_id"].ToString();
                this.lblUserName.Text = ViewState["menu_user_name"].ToString();
                this.tbxMakeNo_key.Focus();
            }

            this.tbxDate_key.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            this.tbxDate_key.Attributes["itType"] = "date";
            this.tbxDate_key.Attributes["itLength"] = "20";
            this.tbxDate_key.Attributes["itName"] = "登録日";

            if (Context.Items["line_id"] != null)
            {
                G_line_id = Context.Items["line_id"].ToString();
                this.tbxLineId_key.Text = Context.Items["line_id"].ToString();
                this.tbxCheckUser.Text = Context.Items["user_cd"].ToString();
                this.lblUserName.Text = Context.Items["user_name"].ToString();
                this.tbxDate_key.Text = Context.Items["chk_date"].ToString();

                ViewState["chk_no"] = Context.Items["chk_no"].ToString();
                //有权限管理所有
                if (System.Configuration.ConfigurationManager.AppSettings.Get("Chk_AllLines_lines").ToString().IndexOf(G_line_id) >= 0)
                {
                    ViewState["isAllLine"] = true;
                }

                MsInit(1, "", "", true);
                this.tbxMakeNo_key.Focus();
            }
            else if (Request.QueryString["line_id"] != null)
            {
                this.tbxLineId_key.Text = Request.QueryString["line_id"].ToString();

                if (Request.QueryString["user_id"] != null)
                {
                    this.tbxCheckUser.Text = Request.QueryString["user_id"].ToString();

                    if (this.tbxCheckUser.Text != "")
                    {
                        MUserBC BC2 = new MUserBC();
                        DataTable dt = BC2.SelMUser(this.tbxCheckUser.Text, "ajax");

                        if (dt.Rows.Count > 0)
                        {
                            this.lblUserName.Text = dt.Rows[0]["user_name"].ToString();
                        }
                    }
                    this.tbxMakeNo_key.Focus();
                }


            }
            else
            {
                MsInit(1, "", "", true);
            }


            //开启了强制完了和报工
            if (G_line_id.Trim() != ""
                && System.Configuration.ConfigurationManager.AppSettings.Get("baogong_lines").ToString().IndexOf(G_line_id) >= 0
                && System.Configuration.ConfigurationManager.AppSettings.Get("qiangzhi_baogong_lines").ToString().IndexOf(G_line_id) >= 0
                )
            {
                hidIsQiangzhi_baogong_lines.Text = "1";
            }
            else
            {
                hidIsQiangzhi_baogong_lines.Text = "0";
            }



        }


        Context.Items["menu_line_id"] = ViewState["menu_line_id"];
        Context.Items["menu_user_cd"] = ViewState["menu_user_cd"];
        Context.Items["menu_user_name"] = ViewState["menu_user_name"];
        Context.Items["menu_kengen"] = ViewState["menu_kengen"];
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Server.Transfer("Menu.aspx");
    }

    /// <summary>
    /// 明显装载
    /// </summary>
    public void MsInit(bool userDateFlg)
    {
        DataTable dt = GetMsData("", "", userDateFlg);
        gvMs.DataSource = dt;
        gvMs.DataBind();

    }

    /// <summary>
    /// 明显装载
    /// </summary>
    /// <returns></returns>
    private System.Data.DataTable GetMsData(string make_no, string code, bool userDateFlg)
    {

        TCheckResultBC BC = new TCheckResultBC();
        string startTime;
        string endTime;
        System.DateTime currentTime = new System.DateTime();

        if (userDateFlg)
        {
            if (this.tbxDate_key.Text == "")
            {
                currentTime = System.DateTime.Now;
                startTime = currentTime.AddDays(-7).ToString("yyyy/MM/dd");
                endTime = currentTime.ToString("yyyy/MM/dd");
            }
            else
            {
                startTime = Convert.ToDateTime(this.tbxDate_key.Text).ToString("yyyy/MM/dd");
                endTime = startTime;
            }

        }
        else
        {
            currentTime = System.DateTime.Now;
            startTime = currentTime.AddDays(-1000).ToString("yyyy/MM/dd");
            endTime = currentTime.AddDays(1000).ToString("yyyy/MM/dd");
        }


        return BC.SelTCheckResult(G_line_id, startTime, endTime, make_no, code, Convert.ToBoolean(ViewState["isAllLine"]));

    }
    /// <summary>
    /// 明显装载
    /// </summary>
    /// <returns></returns>
    public void MsInit(int pageIdx, string make_no, string code, bool userDateFlg)
    {
        System.Data.DataTable dt = new DataTable();
        System.Data.DataTable dtMs = new DataTable();
        System.Data.DataTable dtPageIdx = new DataTable();
        dt = GetMsData(make_no, code, userDateFlg);
        GetPageData(dt, pageIdx);
    }

    /// <summary>
    /// 检索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {

        hidScroll.Text = "";

        hidScanFlg.Text = "0";
        if (G_line_id == "")
        {
            Common.ShowMsg(this.Page, "请输入生产线");
            return;
        }

        MsInit(1, "", "", true);
    }

    //扫描生产明细书
    protected void btnSelect2_Click(object sender, EventArgs e)
    {
        hidScroll.Text = "";

        hidScanFlg.Text = "1";
        if (G_line_id == "")
        {
            Common.ShowMsg(this.Page, "请输入生产线");
            return;
        }

        MsInit(1, this.tbxMakeNo_key.Text, this.tbxCode_key.Text, false);
    }
    public string IsVis(string kbn)
    {
        if (kbn != "-1")
        {
            return "visible";
        }
        else
        {
            return "hidden";
        }
    }

    public int CalculateTotalPages(DataTable dataTable, int pageSize)
    {
        if (dataTable == null) return 0;
        if (pageSize <= 0) return 0;

        int totalRows = dataTable.Rows.Count;
        if (totalRows == 0) return 0;

        // 整数计算，避免浮点精度问题
        return (totalRows + pageSize - 1) / pageSize;
    }

    //获得数据 页
    public void GetPageData(System.Data.DataTable inDt, int pageIdx)
    {
        int onePageRowCnt = 100;
        int mxPageIdx;
        //mxPageIdx = (int)Math.Ceiling((double)inDt.Rows.Count / (double)onePageRowCnt);
        mxPageIdx = CalculateTotalPages(inDt, onePageRowCnt);


        if (pageIdx > mxPageIdx && mxPageIdx > 0)
        {
            pageIdx--;
        }

        int i;

        int itNo;
        string chk_no;
        string old_chk_no;

        chk_no = "";
        old_chk_no = "";
        itNo = 0;
        if (!inDt.Columns.Contains("No"))
        {
            inDt.Columns.Add("No");
            for (i = 0; i <= inDt.Rows.Count - 1; i++)
            {
                chk_no = inDt.Rows[i]["make_no"].ToString();

                if (chk_no != old_chk_no)
                {
                    itNo++;
                    old_chk_no = chk_no;
                }
                else
                {
                    inDt.Rows[i]["qianpin_suu"] = -1;
                }
                inDt.Rows[i]["No"] = itNo.ToString();
            }

        }


        System.Data.DataTable dt = inDt.Clone();

        for (i = (pageIdx - 1) * onePageRowCnt; i <= (pageIdx) * onePageRowCnt - 1; i++)
        {
            if (i < inDt.Rows.Count)
                dt.Rows.Add(inDt.Rows[i].ItemArray);
        }

        if (ViewState["chk_no"].ToString() == "")
        {

        }
        else
        {
            int tmPageIdx = pageIdx;
            if (inDt.Select("chk_no='" + ViewState["chk_no"].ToString() + "'").Length > 0)
            {
                for (tmPageIdx = 1; tmPageIdx <= mxPageIdx; tmPageIdx++)
                {
                    dt.Clear();
                    for (i = (tmPageIdx - 1) * onePageRowCnt; i <= (tmPageIdx) * onePageRowCnt - 1; i++)
                    {
                        if (i < inDt.Rows.Count)
                            dt.Rows.Add(inDt.Rows[i].ItemArray);
                    }

                    if (dt.Select("chk_no='" + ViewState["chk_no"].ToString() + "'").Length > 0)
                    {
                        break;
                    }
                }

            }

        }




        System.Data.DataTable dt2 = new System.Data.DataTable();
        dt2.Columns.Add("idx");
        for (i = 1; i <= mxPageIdx; i++)
        {
            System.Data.DataRow dr = dt2.NewRow();
            dr[0] = i.ToString();
            //dr.Item[0] = i.ToString();
            dt2.Rows.Add(dr);
        }

        gvMs.DataSource = dt;
        gvMs.DataBind();

        chk_no = "";
        old_chk_no = "";
        System.Drawing.Color tmpColor = System.Drawing.Color.Blue;

        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            string[] sArray = dt.Rows[i]["chk_no"].ToString().Split('_');
            chk_no = sArray[0] + "_" + sArray[1];

            if (chk_no != old_chk_no)
            {
                if (tmpColor == System.Drawing.Color.Black)
                {
                    tmpColor = System.Drawing.Color.Blue;
                }
                else
                {
                    tmpColor = System.Drawing.Color.Black;
                }

                old_chk_no = chk_no;

                //gvMs.Rows[i].Cells[0].Font.Bold = true;
            }

            gvMs.Rows[i].Attributes["chk_no"] = dt.Rows[i]["chk_no"].ToString();
            if (hidOldChkNo.Text == dt.Rows[i]["chk_no"].ToString())
            {
                gvMs.Rows[i].TabIndex = -1;
            }
            //gvMs.Rows[i].Cells[0].Text = sArray[2];

            gvMs.Rows[i].ForeColor = tmpColor;
            //gvMs.Rows[i].Cells[0].ForeColor  = tmpColor;
            //gvMs.Rows[i].Cells[1].ForeColor = tmpColor;
            //gvMs.Rows[i].Cells[2].ForeColor = tmpColor; 

        }

        ddlPageIdx.DataValueField = "idx";
        ddlPageIdx.DataTextField = "idx";
        ddlPageIdx.DataSource = dt2;
        ddlPageIdx.DataBind();

        lblAllPageText.Text = ddlPageIdx.Items.Count.ToString();

        if (ddlPageIdx.Items.Count >= pageIdx)
            ddlPageIdx.SelectedIndex = pageIdx - 1;

    }

    //翻页
    protected void ddlPageIdx_SelectedIndexChanged(object sender, EventArgs e)
    {
        MsInit(Convert.ToInt32(ddlPageIdx.SelectedValue), "", "", true);
    }

    /// <summary>
    /// 新规检查
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        //string chk_no = this.hidChkNo.Text;
        //string make_no = this.hidMakeNo.Text;
        //string code = this.hidCode.Text;
        //string suu = this.hidSuu.Text;
        string make_no = this.tbxMakeNo_key.Text;
        string code = this.tbxCode_key.Text;
        string suu = this.hidSuu.Text;
        string line_id = G_line_id;
        string loginLine_id = line_id;
        string tmpRowLineId = "";

        Context.Items["suu"] = suu;
        Context.Items["chk_date"] = this.tbxDate_key.Text.Trim();
        Context.Items["make_no"] = make_no;
        Context.Items["code"] = code;
        Context.Items["line_id"] = line_id;
        Context.Items["user_cd"] = this.tbxCheckUser.Text.Trim();
        Context.Items["user_name"] = this.lblUserName.Text;

        TCheckResultBC BC = new TCheckResultBC();

        string lineId_key = this.tbxLineId_key.Text;
        System.Data.DataTable dt = BC.SelTCheckResult(G_line_id, "", "", make_no, code, Convert.ToBoolean(ViewState["isAllLine"]));


        //System.Data.DataTable dt = BC.SelTCheckResult(lineId_key, "", "", make_no, code);

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

            tmpRowLineId = drs[0]["line_id"].ToString();
            string[] sArray = drs[idx]["chk_no"].ToString().Split('_');

            tmp_chk_no = sArray[0] + "_" + sArray[1] + "_" + (mxChkTimes + 1).ToString();

            BC.InsTCheckResult(tmp_chk_no
                             , System.DateTime.Now.Year.ToString()
                             , (mxChkTimes + 1).ToString()
                             , drs[idx]["plan_no"].ToString()
                             , tmpRowLineId
                             , loginLine_id
                             , drs[idx]["make_no"].ToString()
                             , drs[idx]["code"].ToString()
                             , drs[idx]["suu"].ToString()
                             , drs[idx]["temp_id"].ToString()
                             , drs[idx]["chk_result"].ToString()
                             , this.tbxCheckUser.Text.Trim()
                             , drs[idx]["yotei_chk_date"].ToString()
                             , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                             , ""
                             , ""
                             , "0"
                             , this.tbxCheckUser.Text.Trim()
                             , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),Convert.ToBoolean(ViewState["isAllLine"])
                             );

            //第一次新规 做两条记录
            System.Data.DataTable dtRltOnly = BC.SelTCheckResult("", "", lineId_key, make_no);
            if (dtRltOnly.Rows.Count == 1)
            {
                mxChkTimes++;
                tmp_chk_no = sArray[0] + "_" + sArray[1] + "_" + (mxChkTimes + 1).ToString();
                BC.InsTCheckResult(tmp_chk_no
                                 , System.DateTime.Now.Year.ToString()
                                 , (mxChkTimes + 1).ToString()
                                 , drs[idx]["plan_no"].ToString()
                                 , tmpRowLineId
                                 , loginLine_id
                                 , drs[idx]["make_no"].ToString()
                                 , drs[idx]["code"].ToString()
                                 , drs[idx]["suu"].ToString()
                                 , drs[idx]["temp_id"].ToString()
                                 , drs[idx]["chk_result"].ToString()
                                 , this.tbxCheckUser.Text.Trim()
                                 , drs[idx]["yotei_chk_date"].ToString()
                                 , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                                 , ""
                                 , ""
                                 , "0"
                                 , this.tbxCheckUser.Text.Trim()
                                 , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), Convert.ToBoolean(ViewState["isAllLine"])
                                 );
            }

            Context.Items["chk_no"] = tmp_chk_no;
            Context.Items["isAllLine"] = ViewState["isAllLine"];
            Server.Transfer("t_check_ms.aspx");

        }
        else
        {
            Common.ShowMsg(this.Page, "检查计划数据不存在;");
            return;
        }
    }
    //删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string chk_no = hidChkNo.Text.Trim();
        string line_id = this.tbxLineId_key.Text;
        string user = this.tbxCheckUser.Text.Trim();

        TCheckResultBC BC = new TCheckResultBC();
        BC.DeleteCheckResult(chk_no, line_id, user);

        ViewState["chk_no"] = "";
        MsInit((this.ddlPageIdx.SelectedIndex + 1), "", "", true);


    }
    //更新
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        string make_no = this.tbxMakeNo_key.Text;
        string code = this.tbxCode_key.Text;
        string suu = this.hidSuu.Text;
        string line_id = G_line_id;

        Context.Items["suu"] = suu;

        Context.Items["chk_date"] = this.tbxDate_key.Text.Trim();

        Context.Items["chk_no"] = hidChkNo.Text.Trim();

        Context.Items["user_name"] = this.lblUserName.Text;

        Context.Items["make_no"] = make_no;
        Context.Items["code"] = code;
        Context.Items["line_id"] = line_id;
        Context.Items["user_cd"] = this.tbxCheckUser.Text.Trim();
        Context.Items["isAllLine"] = ViewState["isAllLine"];
        Server.Transfer("t_check_ms.aspx");
    }

    //完了
    protected void btnComlete_Click(object sender, EventArgs e)
    {

        string chk_no = hidChkNo.Text.Trim();
        string make_no = this.tbxMakeNo_key.Text;
        string code = this.tbxCode_key.Text;
        string suu = this.hidSuu.Text;
        string line_id = G_line_id;
        string loginLine_id = line_id;

        ViewState["code"] = code;
        ViewState["make_no"] = make_no;
        ViewState["user_cd"] = this.tbxCheckUser.Text.Trim();
        ViewState["line_id"] = line_id;


        TCheckMsBC BC_TCheckMsBC = new TCheckMsBC();
        //t_check_result 获得本次检查结果 用 chk_no
        DataTable dtRlt = BC_TCheckMsBC.GetResultByChkNo(chk_no, line_id);

        //如果有检查记录
        if (dtRlt.Rows.Count > 0)
        {
            TCheckMsBC BC = new TCheckMsBC();
            BC.UpdTCheckResultMS(hidChkNo.Text.Trim(), line_id, this.tbxCheckUser.Text.Trim());
            MsInit((this.ddlPageIdx.SelectedIndex + 1), "", "", true);

            ViewState["chk_no"] = hidChkNo.Text.Trim();

        }
        else
        {
            //如果没有检查记录
            //那么新规 一个检查

            //寻找这个工单是否有检查
            TCheckResultBC BC = new TCheckResultBC();
            System.Data.DataTable dt = BC.SelTCheckResult(G_line_id, "", "", make_no, code, Convert.ToBoolean(ViewState["isAllLine"]));

            Int32 i, mxChkTimes, chkTimes, idx;
            mxChkTimes = 0;
            idx = 0;

            string yotei_chk_date;
            DataRow[] drs;

            string tmp_chk_no;
            string tmpRowLineId;

            ///sp = chr("_");
            //如果这个工单是否有检查
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

                tmpRowLineId = drs[0]["line_id"].ToString();
                string[] sArray = drs[idx]["chk_no"].ToString().Split('_');

                tmp_chk_no = sArray[0] + "_" + sArray[1] + "_" + (mxChkTimes + 1).ToString();

                BC.InsTCheckResult(tmp_chk_no
                                 , System.DateTime.Now.Year.ToString()
                                 , (mxChkTimes + 1).ToString()
                                 , drs[idx]["plan_no"].ToString()
                                 , tmpRowLineId
                                 , loginLine_id
                                 , drs[idx]["make_no"].ToString()
                                 , drs[idx]["code"].ToString()
                                 , drs[idx]["suu"].ToString()
                                 , drs[idx]["temp_id"].ToString()
                                 , drs[idx]["chk_result"].ToString()
                                 , this.tbxCheckUser.Text.Trim()
                                 , drs[idx]["yotei_chk_date"].ToString()
                                 , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                                 , ""
                                 , ""
                                 , "0"
                                 , this.tbxCheckUser.Text.Trim()
                                 , System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), Convert.ToBoolean(ViewState["isAllLine"])
                                 );

                TCheckMsBC BC2 = new TCheckMsBC();
                BC2.UpdTCheckResultOK(tmp_chk_no.Trim(), line_id, this.tbxCheckUser.Text.Trim());

                MsInit((this.ddlPageIdx.SelectedIndex + 1), "", "", true);

                ViewState["chk_no"] = tmp_chk_no.Trim();
                //Context.Items["chk_no"] = tmp_chk_no;
                //Server.Transfer("t_check_ms.aspx");

            }

        }

        try
        {
            //如果是可以报工的生产线
            if (System.Configuration.ConfigurationManager.AppSettings.Get("baogong_lines").ToString().IndexOf(ViewState["line_id"].ToString()) >= 0
                && System.Configuration.ConfigurationManager.AppSettings.Get("qiangzhi_baogong_lines").ToString().IndexOf(ViewState["line_id"].ToString()) >= 0
                )
            {

                //v_bg_list_new
                TCheckMsBC BC = new TCheckMsBC();
                TCheckResultBC BC_CK = new TCheckResultBC();
                DataTable dt = (new GKL_BgDA()).SelTCheckResultOkSuu(ViewState["make_no"].ToString(), ViewState["code"].ToString(), ViewState["line_id"].ToString());

                //t_check_result 获得本次检查结果 用 chk_no
                DataTable dtRlt2 = BC.GetResultByChkNo(ViewState["chk_no"].ToString());

                string txt = "";

                try
                {
                    txt = "1.准备报工,v_bg_list_new:" + dt.Rows.Count.ToString() + ",t_check_result:" + dtRlt2.Rows.Count.ToString();
                    if (dt.Rows.Count > 0 && dtRlt2.Rows.Count > 0)
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


                if (dt.Rows.Count > 0 && dtRlt2.Rows.Count > 0)
                {
                    //OK 数 bg_result
                    if (Convert.ToInt32(dt.Rows[0]["ok_suu"].ToString()) >= 1 && (dt.Rows[0]["bg_result"] == "" || dt.Rows[0]["bg_result"] == "NG"))
                    {
                        //如果本次检查结果也是OK
                        if (Common.NullToEmpty(dtRlt2.Rows[0]["chk_result"].ToString()) == "1")
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

        //TCheckMsBC BC = new TCheckMsBC();
        //BC.UpdTCheckResultMS(hidChkNo.Text.Trim(), line_id, this.tbxCheckUser.Text.Trim());
        //MsInit((this.ddlPageIdx.SelectedIndex + 1), "", "", true);
    }

    //前一日
    protected void btnPreDay_Click(object sender, EventArgs e)
    {
        if (this.tbxDate_key.Text == "")
        {
            this.tbxDate_key.Text = System.DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
        }
        else
        {
            this.tbxDate_key.Text = Convert.ToDateTime(this.tbxDate_key.Text).AddDays(-1).ToString("yyyy/MM/dd");
        }
        ViewState["chk_no"] = "";
        MsInit(1, "", "", true);
    }
    //下一天
    protected void btnNextDay_Click(object sender, EventArgs e)
    {
        if (this.tbxDate_key.Text == "")
        {
            this.tbxDate_key.Text = System.DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
        }
        else
        {
            this.tbxDate_key.Text = Convert.ToDateTime(this.tbxDate_key.Text).AddDays(1).ToString("yyyy/MM/dd");
        }
        ViewState["chk_no"] = "";
        MsInit(1, "", "", true);
    }


    protected void ddl_lines_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidScanFlg.Text = "0";
        if (G_line_id == "")
        {
            Common.ShowMsg(this.Page, "请输入生产线");
            return;
        }

        MsInit(1, "", "", true);

        if (G_line_id == ViewState["menu_line_id"].ToString())
        {
            hidIsMyLine.Text = "1";
            btnInsert.Enabled = true;
            btnInsert.Visible = true;
        }
        else
        {
            hidIsMyLine.Text = "0";
            btnInsert.Enabled = false;
            btnInsert.Visible = false;
        }

    }
}