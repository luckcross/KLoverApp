using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLoversApp.Models
{
    public class Token
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string AcessToken { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime ExpireDate { get; set; }
        public double ExpireIn { get; set; }

        public Token() { }
    }
}
