using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using LiveSearch.Data;
using System.Web.Mvc;

namespace LiveSearch.Models
{
    public class CustomerModel
    {
        public int CustId { get; set; }
        public string CustName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string SaveCustomer(CustomerModel model)
        {
            string msg = "";
            LiveSearchEntities Db = new LiveSearchEntities();
          
            var CustomerSave = new tbl_Customer()
            {
                //CustId = model.CustId,
                CustName = model.CustName,
                Address = model.Address,
                Mobile = model.Mobile,
                Email = model.Email,
            };
            Db.tbl_Customer.Add(CustomerSave);
            Db.SaveChanges();
            msg = "Customer Added Successfully";
            return msg;


        }
        public List<CustomerModel> SearchCustomer(string Prefix)
        {
            try
            {
                List<CustomerModel> model = new List<CustomerModel>();
                LiveSearchEntities db = new LiveSearchEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCustomerSearch";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter LID = cmd.CreateParameter();
                        LID.ParameterName = "SearchString";
                        LID.Value = Prefix;
                        cmd.Parameters.Add(LID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["NotesDate"].ToString());
                            }
                            catch
                            {

                            }
                            model.Add(new CustomerModel()
                            {
                                CustId = Convert.ToInt32(dr["CustId"].ToString()),
                                CustName = dr["CustName"].ToString(),
                                Address = dr["Address"].ToString(),
                                // SalePrice = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                Mobile = dr["Mobile"].ToString(),
                                Email = dr["Email"].ToString(),
                            });
                        }


                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}