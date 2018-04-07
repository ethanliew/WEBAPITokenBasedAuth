using System;
using System.Security.Claims;
using System.Web.Http;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WEBAPITokenBasedAuth.Controllers
{
    public class DataController : ApiController
    {
        public string Connection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public DataSet GetAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.Name == "RaviRanjanKr")
            {
                DataSet dsRecord = new DataSet();
                try
                {
                    using (SqlConnection myConnection = new SqlConnection(Connection))
                    {
                        using (SqlCommand myCommand = new SqlCommand("procFixFD", myConnection))
                        {
                            myCommand.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter da = new SqlDataAdapter(myCommand))
                            {
                                da.Fill(dsRecord);
                            }
                        }
                    }
                }
                catch (Exception)
                { }
                if (dsRecord.Tables[0].Rows.Count > 0)
                {
                    return dsRecord;
                }
                else
                {
                    return null; // instead of returning null, you can customize code as per your need.
                }
            }
            else
            {
                return null;
            }
        }
    }
}
