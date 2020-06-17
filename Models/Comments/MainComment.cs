using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdaia.Models.Comments
{
    public class MainComment:Commnet
    {

        public List<SubComment> SubComments { get; set; }



    }
}
