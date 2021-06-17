using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            DataBaseController con = DataBaseController.init();

            con.start();

            







        }
    }
}
