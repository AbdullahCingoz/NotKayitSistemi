using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        public string numara;

        SqlConnection baglanti=new SqlConnection(@"Data Source=LAPTOP-G0BE8F7A\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True ");
        
        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            lbl_Numara.Text = numara;
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT*FROM TblDers WHERE OgrNo=@p1",baglanti);
            cmd.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lbl_AdSoyad.Text=dr[2].ToString()+" "+dr[3].ToString();
                lbl_S1.Text=dr[4].ToString();
                lbl_S2.Text = dr[5].ToString();
                lbl_S3.Text = dr[6].ToString();
                lbl_Ortalama.Text= dr[7].ToString();
                lbl_Durum.Text = dr[8].ToString();
            }
            baglanti.Close();
        }
    }
}
