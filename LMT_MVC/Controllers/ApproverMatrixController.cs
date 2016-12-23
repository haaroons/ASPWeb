﻿using LMT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LMT_MVC.Controllers
{
    public class ApproverMatrixController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            DCDataContext DbLMS = new DCDataContext();
            var ApproverMatrix = DbLMS.Leave_Management_Approval_Matrixes.Select(x => new {x.pk_approval_id, Approver = x.Innovation_Management_User1.user_firstname, ApproverId = x.Innovation_Management_User1.pk_user_id, Requester = x.Innovation_Management_User.user_firstname, RequesterId = x.Innovation_Management_User.pk_user_id });

            return ApproverMatrix;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public bool Post(int id, [FromBody]int[] Requesters)
        {
            try
            {
                DCDataContext DbLMS = new DCDataContext();
                List<Leave_Management_Approval_Matrix> Requestermap = new List<Leave_Management_Approval_Matrix>();
                foreach (int Requester in Requesters)
                {
                    Leave_Management_Approval_Matrix requestmap = new Leave_Management_Approval_Matrix();
                    requestmap.fk_approver_id = id;
                    requestmap.fk_requestor_id = Requester;
                    Requestermap.Add(requestmap);
                }
                var PreviousMap = DbLMS.Leave_Management_Approval_Matrixes.Where(x => x.fk_approver_id == id);
                if (PreviousMap.Count() > 0)
                {
                    DbLMS.Leave_Management_Approval_Matrixes.DeleteAllOnSubmit(PreviousMap);
                }

                DbLMS.Leave_Management_Approval_Matrixes.InsertAllOnSubmit(Requestermap);
                DbLMS.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            try
            {
                DCDataContext DbLMS = new DCDataContext();
                var ApproverMapDel = DbLMS.Leave_Management_Approval_Matrixes.FirstOrDefault(x => x.pk_approval_id == id);
                DbLMS.Leave_Management_Approval_Matrixes.DeleteOnSubmit(ApproverMapDel);
                DbLMS.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}