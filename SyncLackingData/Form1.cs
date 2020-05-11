using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncLackingData
{

    public partial class Form1 : Form
    {

        private ATMCEntities atmcDb = new ATMCEntities();
        private TWM3Entities twm3Db = new TWM3Entities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SyncTwm3Data();
            Application.Exit();
        }

        private void SyncTwm3Data()
        {
            Console.WriteLine("Query table...");
            var query1 = from a in twm3Db.備料明細
                         join b in twm3Db.prepare_Schedule
                         on a.訂單 equals b.PO
                         where a.缺料數量 > 0 && !b.prepare_stateID.Equals(-1)
                         orderby a.訂單
                         select new temp1
                         {
                             PO = b.PO,
                             Model_name = b.Model_name,
                             物料 = a.物料,
                             UnitsInStock = a.UnitsInStock,
                             物管 = a.物管,
                             缺料數量 = a.缺料數量,
                             createDate = DateTime.Now
                         };

            foreach (temp1 t in query1)
            {
                atmcDb.temp1.Add(t);
            }

            atmcDb.SaveChanges();
            //

            //Console.WriteLine("Select data...");
            //Console.WriteLine(query1.Count());

            //var items = query1.Take(1).ToList();
            //Console.WriteLine(items[0]);
        }

        private void SyncLackingData()
        {

        }
    }
}
