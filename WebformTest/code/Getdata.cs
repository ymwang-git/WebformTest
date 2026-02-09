using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Npgsql;
using NpgsqlTypes;

namespace Firstbankvirusreport.code
{
    public class Getdata
    {
        Classall classall = new Classall();
        NpgsqlConnection npgcn;
        NpgsqlCommand cmd;
        NpgsqlDataReader dr;
        //NpgsqlDataAdapter da;
        public DataTable GetScanResult(string dataid)
        {
            DataTable dt = new DataTable();
            // Implementation for fetching scan results goes here
            string sqlstr = "";
            //sqlstr = "CALL scan.proc_GetScanResult(@dataid,NULL)";

            sqlstr = @"SELECT request_id, NULL::bigint AS parent_request_id FROM scan.request WHERE data_id = @dataid";

            using (npgcn = new NpgsqlConnection(classall.DbConn()))
            {
                npgcn.Open();
                using (cmd = new NpgsqlCommand(sqlstr, npgcn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@dataid", dataid);
                    //cmd.Parameters.Add("@setkey", NpgsqlTypes.NpgsqlDbType.Text).Value = "掃描類型";

                    using (dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }
                    }
                }
            }
            return dt;
        }

    }

}