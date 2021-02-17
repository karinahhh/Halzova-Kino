using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino_Halzova
{
    public partial class Start_app : Form
    {
        SqlCommand command;
        SqlDataAdapter Saalide_adapter;
        public int i = 0, j = 0;
        ListBox saalide_list;
        public int[] read_list;
        public int[] kohad_list;
        PictureBox lttl;
        public Start_app()
        {
            this.Text = "Kino";
            this.Size = new Size(560, 400);

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\Halzova\Kino-Halzova\Kino-Halzova\Films.mdf;Integrated Security=True");
            con.Open();

            Saalide_adapter = new SqlDataAdapter("SELECT * FROM saalid_tabel", con);
            DataTable saalid_tabel = new DataTable();
            Saalide_adapter.Fill(saalid_tabel);
            saalide_list = new ListBox();
            saalide_list.Location = new Point(10, 10);
            saalide_list.Size = new Size(150, 60);

            foreach (DataRow row in saalid_tabel.Rows)
            {
                saalide_list.Items.Add(row["Saalinimetus"]);
            }

            read_list = new int[saalid_tabel.Rows.Count];
            kohad_list = new int[saalid_tabel.Rows.Count];
            int a = 0;
            foreach (DataRow row in saalid_tabel.Rows)
            {
                read_list[a] = (int)row["Read"];
                kohad_list[a] = (int)row["Kohad"];
                a = a + 1;
            }
            con.Close();


            lttl = new PictureBox()
            {
                Image = Image.FromFile(@"C:\Users\opilane\source\repos\Halzova\Kino-Halzova\Kino-Halzova\images\djavol.jpg"),
                Size = new Size(100, 150),
                Location = new Point(400, 50)
            };
            this.Controls.Add(saalide_list);
            this.Controls.Add(lttl);
            saalide_list.SelectedIndexChanged += Saalide_list_SelectedIndexChanged1;

        }

        private void Saalide_list_SelectedIndexChanged1(object sender, EventArgs e)
        {
            i = read_list[saalide_list.SelectedIndex];
            j = kohad_list[saalide_list.SelectedIndex];
            Saalid saalid = new Saalid(i, j);
            saalid.Show();
        }
    }
}
