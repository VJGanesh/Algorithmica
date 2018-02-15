using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlTransactions
{
    class Program
    {
        private static object _lock = new object();
        static void Main(string[] args)
        {
            Program p = new Program();
            p.ConcurrentBag();
            
        }

        SqlConnection conn;
        DataTable dt = new DataTable();
        private SqlDataAdapter da;

        public void ConcurrentBag() 
        {
            
            ConcurrentBag<Task<bool>> tasks=new ConcurrentBag<Task<bool>>();


            Task<bool> T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);


            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);


            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);

            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);

            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);


            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);


            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);


            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);


            T = Task.Factory.StartNew(() => update1());
            tasks.Add(T);

            Task.WaitAll(tasks.ToArray());

            conn.Close();
        }

        public bool update1()
        {
            //SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Admin_T; Integrated Security=true");
            conn = GetConnection();
            Load();
            modify();
            save();
            //conn.Close();
            return true;
        }

        public bool Load()
        {
            try
            {
              
            lock (_lock)
            {

                da = new SqlDataAdapter("select * from product", conn);
                dt = new DataTable();
                da.Fill(dt);

            }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void save()
        {
            lock (_lock)
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.InsertCommand = builder.GetInsertCommand();
                da.UpdateCommand = builder.GetUpdateCommand();
                da.DeleteCommand = builder.GetDeleteCommand();
                builder.ConflictOption = ConflictOption.OverwriteChanges;
                da.Update(dt);
                //da.Fill(dt);

            }
        }

        public void modify()
        {
            DataRow dr = dt.NewRow();
            dr["Product"] = "Vijay" + System.Threading.Thread.CurrentThread.ManagedThreadId;
            dt.Rows.Add(dr);
        }

       

        public SqlConnection GetConnection()
        {
            lock (_lock)
            {
                conn = new SqlConnection("Data Source=localhost;Initial Catalog=Algorithmica; Integrated Security=true");
               
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return conn;
            }
            
            
        }
    }
}
