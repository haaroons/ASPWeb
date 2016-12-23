using LMT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace LMT_MVC.Controllers.webapi
{
    public class LeavesTakenController : ApiController
    {
        public List<LeaveApplied> Get()
        {
            List<LeaveApplied> L = new List<LeaveApplied>();

            DCDataContext DC = new DCDataContext();
            
            var la = (from l in DC.Leave_Management_Leave_Takens where l.fk_user_id == CommonMethods.CurrentUserId orderby l.leave_days descending select new { l, l.Leave_Management_Leave_Type.leave_type, l.Innovation_Management_User1.user_firstname });//.ToList();
            foreach (var i in la)
            {
                LeaveApplied l = new LeaveApplied();
                l.ID = i.l.pk_leave_taken_id;
                l.D = i.l.leave_days.Value.ToString("yyyy-MM-dd");
                l.T = i.leave_type;
                //l.A = i.user_firstname;
                string H = "";
                if (i.l.is_halfday.Value)
                {
                    H = "Morning";
                    if (i.l.is_afternoon.Value)
                    {
                        H = "Afternoon";
                    }
                }
                l.H = H;
                switch (i.l.leave_status)
                {
                    case 0:
                        l.A = "Applied";
                        break;
                    case 1:
                        l.A = "Approved (" + i.user_firstname + ")";
                        break;
                    case 2:
                        l.A = "Rejected";
                        break;
                }
                L.Add(l);
            }
            return L;
        }

        // POST api/<controller>
        public void Post(LeaveApplied l)
        {
            if (l.D == null)
            {
                returnToAjax("please choose a date");
            }

            try
            {
                DateTime d = Convert.ToDateTime(l.D);
                //if (d < DateTime.Now)
                //{
                //    throw new Exception("Please select a future date");
                //}

                int t = Convert.ToInt32(l.T);
                int dt = Convert.ToInt32(l.H);

                Leave_Management_Leave_Taken lt = new Leave_Management_Leave_Taken();
                lt.fk_leave_type = t; lt.leave_days = d;
                lt.fk_user_id = CommonMethods.CurrentUserId;
                lt.leave_status = 0;
                switch (dt)
                {
                    case 0:
                        lt.is_halfday = false;
                        break;
                    case 1:
                        lt.is_halfday = true;
                        lt.is_afternoon = false;
                        break;
                    case 2:
                        lt.is_halfday = true;
                        lt.is_afternoon = true;
                        break;
                }
                DCDataContext DC = new DCDataContext();

                #region check if already applied
                var alreadyadded = DC.Leave_Management_Leave_Takens.Where(x => x.fk_user_id == CommonMethods.CurrentUserId && x.leave_days == d);
                if (alreadyadded.Count() > 0)
                {
                    throw new Exception("Already applied for this date");
                }
                #endregion

                DC.Leave_Management_Leave_Takens.InsertOnSubmit(lt);
                DC.SubmitChanges();
                var approvers = DC.Leave_Management_Approval_Matrixes.Select(x => new { x.fk_approver_id, x.fk_requestor_id, ApproverEmail = x.Innovation_Management_User1.user_email, Requester = x.Innovation_Management_User.user_firstname }).Where(x => x.fk_requestor_id == CommonMethods.CurrentUserId).ToList();
                if (approvers.Count() > 0)
                {
                    Thread email = new System.Threading.Thread(delegate()
                    {
                        foreach (var item in approvers)
                        {
                            CommonMethods.SendMail(item.ApproverEmail, "info@globibo.com", "Globibo Leave Request - " + item.Requester, "", false);
                        }
                    });
                    email.IsBackground = true;
                    email.Start();
                }
            }
            catch (Exception ex)
            {
                returnToAjax(ex.Message);
            }
        }

        public void Delete(int id)
        {
            using (DCDataContext DC = new DCDataContext())
            {
                var def = (from l in DC.Leave_Management_Leave_Takens where l.pk_leave_taken_id == id select l).FirstOrDefault();
                if (def.leave_status != 0)
                {
                    returnToAjax("You cannot delete this");
                }
                else
                {
                    DC.Leave_Management_Leave_Takens.DeleteOnSubmit(def);
                    DC.SubmitChanges();
                }
            }
        }

        private static void returnToAjax(string Message)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = Message
            };
            throw new HttpResponseException(resp);
        }
    }

    public class LeaveApplied
    {
        public int ID { get; set; }
        public string D { get; set; }//date
        public string T { get; set; }//Type
        public string A { get; set; }//approved by
        public string H { get; set; }//which part of the day
    }
}