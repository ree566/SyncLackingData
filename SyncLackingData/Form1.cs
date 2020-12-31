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

        private ATMCEntities1 atmcDb = new ATMCEntities1();
        private TWM3Entities twm3Db = new TWM3Entities();
        private lackingEntities1 lackingDb = new lackingEntities1();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.SyncTwm3Data();
            this.SyncLackingData();
            //this.TestLacking();

            Application.Exit();
        }

        private void SyncTwm3Data()
        {
            /*
                select TOP (100) PERCENT 物料,缺料數量,UnitsInStock
                from TWM3.dbo.備料明細 JOIN TWM3.dbo.prepare_Schedule on 訂單=PO 
                                           JOIN TWM3.dbo.prepare_state on prepare_Schedule.prepare_stateID=prepare_state.prepare_stateID 
                where 缺料數量 >'0' and prepare_state.prepare_stateID<>'-1' 
                GROUP BY 備料明細.id,CLASS, 生產儲位,訂單,需求溯源,物料,採購群組,需求數量,
                         缺料數量,PC_Schedule,prepare_state,物管,回覆日期,UnitsInStock 
                ORDER BY 訂單 ASC
             */

            //Remove all data first
            var allData = from a in atmcDb.temp1 select a;
            atmcDb.temp1.RemoveRange(allData);

            Console.WriteLine("Query Twm3 table...");
            var query = from a in twm3Db.備料明細
                        join b in twm3Db.prepare_Schedule
                        on a.訂單 equals b.PO
                        where a.缺料數量 > 0 && !b.prepare_stateID.Equals(-1)
                        orderby a.訂單
                        select new temp1
                        {
                            po = b.PO,
                            modelName = b.Model_name,
                            material = a.物料,
                            unitsInStock = a.UnitsInStock,
                            owner = a.物管,
                            shortage = a.缺料數量,
                            createDate = DateTime.Now
                        };

            Console.WriteLine(query.Count());

            foreach (temp1 t in query)
            {
                atmcDb.temp1.Add(t);

                temp1_Log t_log = new temp1_Log()
                {
                    po = t.po,
                    modelName = t.modelName,
                    material = t.material,
                    unitsInStock = t.unitsInStock,
                    owner = t.owner,
                    shortage = t.shortage,
                    createDate = DateTime.Now
                };
                atmcDb.temp1_Log.Add(t_log);
            }

            Console.WriteLine("Save temp1 table...");
            atmcDb.SaveChanges();


            //Console.WriteLine("Select data...");
            //Console.WriteLine(query1.Count());

            //var items = query1.Take(1).ToList();
            //Console.WriteLine(items[0]);
        }

        private void SyncLackingData()
        {
            /*
            SELECT
                orders.*,
	            items.*,
	            orders.id AS id,
	            users.name AS user_name,
	            teams.name AS team_name,
	            order_types.name AS type_name
            FROM

                orders, items, users, order_types, teams
            WHERE

                orders.id = items.order_id &&
                orders.user_id = users.id &&
                orders.type = order_types.id &&
                orders.team_id = teams.id &&
                orders.time_close = 0
           */

            //Remove all data first
            var allData = from a in atmcDb.temp2 select a;
            atmcDb.temp2.RemoveRange(allData);

            Console.WriteLine("Query Lacking table...");
            var query = from o in lackingDb.orders
                        join i in lackingDb.items
                        on o.id equals i.order_id
                        join u in lackingDb.users
                        on o.user_id equals u.id
                        join r in lackingDb.replies
                        on o.id equals r.order_id into gj
                        from sub_r in gj.DefaultIfEmpty()
                        where DateTime.Compare(o.time_close, DateTime.MinValue) <= 0
                        select new temp2
                        {
                            po = i.label_1,
                            modelName = i.label_2,
                            material = i.label_3,
                            quantity = o.number,
                            remark = sub_r == null ? String.Empty : sub_r.comment,
                            createDate = DateTime.Now
                        };

            Console.WriteLine(query.Count());

            foreach (temp2 t in query)
            {
                atmcDb.temp2.Add(t);

                temp2_Log t_Log = new temp2_Log()
                {
                    po = t.po,
                    modelName = t.modelName,
                    material = t.material,
                    quantity = t.quantity,
                    remark = t.remark,
                    createDate = DateTime.Now
                };

                atmcDb.temp2_Log.Add(t_Log);
            }

            Console.WriteLine("Save temp2 table...");
            atmcDb.SaveChanges();

        }

        private void TestLacking()
        {
            var query = from o in lackingDb.orders
                        join i in lackingDb.items
                        on o.id equals i.order_id
                        join u in lackingDb.users
                        on o.user_id equals u.id
                        join r in lackingDb.replies
                        on o.id equals r.order_id into gj
                        from sub_r in gj.DefaultIfEmpty()
                        where DateTime.Compare(o.time_close, DateTime.MinValue) <= 0 && i.label_1.Equals("TPK000532ZA")
                        select new temp2
                        {
                            po = i.label_1,
                            modelName = i.label_2,
                            material = i.label_3,
                            quantity = o.number,
                            remark = sub_r == null ? String.Empty : sub_r.comment,
                            createDate = DateTime.Now
                        };

            Console.WriteLine(query.Count());

            //Console.WriteLine(DateTime.MinValue);
            //Console.WriteLine(query.First().time_close);
            //Console.WriteLine(DateTime.Compare(query.First().time_open, DateTime.MinValue));
        }
    }
}
