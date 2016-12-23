using LMT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMT_MVC.Controllers.webapi
{
    public class leaveTypeController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            DCDataContext DbLMS = new DCDataContext();
            var LeaveType = DbLMS.Leave_Management_Leave_Types;
            List<Leave_Management_Leave_Type> objLeaveType = new List<Leave_Management_Leave_Type>();
            foreach (var x in LeaveType)
            {
                Leave_Management_Leave_Type ObjLeave_Management_Leave_Type = new Leave_Management_Leave_Type();
                ObjLeave_Management_Leave_Type.pk_leave_type_id = x.pk_leave_type_id;
                ObjLeave_Management_Leave_Type.leave_type = x.leave_type;
                objLeaveType.Add(ObjLeave_Management_Leave_Type);
            }
            return objLeaveType;
        }//user_firstname user_lastname user_picture user_email user_isadmin user_isdeleted pk_user_id

        // GET api/<controller>/5
       

        // POST api/<controller>
        public bool Post(Leave_Management_Leave_Type newType)
        {
            try
            {
                DCDataContext DbLMS = new DCDataContext();

                DbLMS.Leave_Management_Leave_Types.InsertOnSubmit(newType);
                DbLMS.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return true;
            }
            
        }

        // PUT api/<controller>/5
        public bool Post(int id, Leave_Management_Leave_Type newType)
        {
            try
            {
                DCDataContext DbLMS = new DCDataContext();
                var SelectedRow = DbLMS.Leave_Management_Leave_Types.FirstOrDefault(x => x.pk_leave_type_id == id);
                SelectedRow.leave_type = newType.leave_type;
                
                DbLMS.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}