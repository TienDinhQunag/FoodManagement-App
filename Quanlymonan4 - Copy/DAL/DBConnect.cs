using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBConnect
    {
        protected SqlConnection conn = new SqlConnection(@"Data Source=THAILE\MYLOM;Initial Catalog=QLYBANHANG;Integrated Security=True");

    }
}
